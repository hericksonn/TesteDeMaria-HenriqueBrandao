using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;
using DeMariaTeste.Infrastructure.Data;
using DeMariaTeste.Infrastructure.Logging;
using DeMariaTeste.Infrastructure.Repositories;
using Npgsql;

namespace DeMariaTeste.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly IAuditoriaRepository _audit;
        private readonly ILogger _logger;

        public ClienteService(IClienteRepository repo, IAuditoriaRepository audit, ILogger logger)
        {
            _repo = repo;
            _audit = audit;
            _logger = logger;
        }

        public PaginacaoResultado<Cliente> Listar(string nome, string documento, bool? ativo, int pagina, int tamanho)
        {
            return _repo.Listar(nome, documento, ativo, pagina, tamanho);
        }

        public Cliente ObterPorId(int id)
        {
            return _repo.ObterPorId(id);
        }

        public int Salvar(Cliente cliente)
        {
            cliente.Validar();

            using (var uow = new UnitOfWork())
            {
                try
                {
                    // Checa unicidade antes do INSERT para dar mensagem
                    // melhor do que a violacao de constraint do banco.
                    var existente = _repo.ObterPorDocumento(cliente.Documento, uow);
                    if (existente != null && existente.Id != cliente.Id)
                        throw new RegistroDuplicadoException(
                            "Ja existe outro cliente com o documento informado.");

                    Cliente antes = null;
                    if (cliente.Id == 0)
                    {
                        _repo.Inserir(cliente, uow);
                    }
                    else
                    {
                        antes = _repo.ObterPorId(cliente.Id, uow);
                        _repo.Atualizar(cliente, uow);
                    }

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "Cliente",
                        IdRegistro = cliente.Id.ToString(),
                        Operacao = antes == null ? OperacaoAuditoria.Insert : OperacaoAuditoria.Update,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao(),
                        SnapshotAntes = AuditoriaSerializer.ToJson(antes),
                        SnapshotDepois = AuditoriaSerializer.ToJson(cliente)
                    }, uow);

                    uow.Commit();
                    return cliente.Id;
                }
                catch (PostgresException pex)
                {
                    uow.Rollback();
                    _logger.Erro("Falha ao salvar cliente", pex);

                    if (pex.SqlState == "23505")
                        throw new RegistroDuplicadoException("Documento ja cadastrado em outro cliente.");

                    throw;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public void Excluir(int id)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    var antes = _repo.ObterPorId(id, uow);
                    if (antes == null)
                        throw new DominioException("Cliente nao encontrado.");

                    if (_repo.PossuiOrdemServicoVinculada(id, uow))
                        throw new DominioException(
                            "Nao e possivel excluir o cliente pois ele possui Ordens de Servico vinculadas.");

                    _repo.Excluir(id, uow);

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "Cliente",
                        IdRegistro = id.ToString(),
                        Operacao = OperacaoAuditoria.Delete,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao(),
                        SnapshotAntes = AuditoriaSerializer.ToJson(antes),
                        SnapshotDepois = null
                    }, uow);

                    uow.Commit();
                }
                catch (PostgresException pex)
                {
                    uow.Rollback();
                    _logger.Erro("Falha ao excluir cliente", pex);
                    if (pex.SqlState == "23503")
                        throw new DominioException(
                            "Nao foi possivel excluir, existem registros dependentes desse cliente.");
                    throw;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }
    }
}
