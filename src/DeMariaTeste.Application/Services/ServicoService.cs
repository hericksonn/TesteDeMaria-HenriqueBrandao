using System.Collections.Generic;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;
using DeMariaTeste.Infrastructure.Data;
using DeMariaTeste.Infrastructure.Logging;
using DeMariaTeste.Infrastructure.Repositories;
using Npgsql;

namespace DeMariaTeste.Application.Services
{
    public class ServicoService
    {
        private readonly IServicoRepository _repo;
        private readonly IAuditoriaRepository _audit;
        private readonly ILogger _logger;

        public ServicoService(IServicoRepository repo, IAuditoriaRepository audit, ILogger logger)
        {
            _repo = repo;
            _audit = audit;
            _logger = logger;
        }

        public PaginacaoResultado<Servico> Listar(string nome, bool? ativo, int pagina, int tamanho)
        {
            return _repo.Listar(nome, ativo, pagina, tamanho);
        }

        public IList<Servico> ListarAtivos()
        {
            return _repo.ListarAtivos();
        }

        public Servico ObterPorId(int id)
        {
            return _repo.ObterPorId(id);
        }

        public int Salvar(Servico servico)
        {
            servico.Validar();

            using (var uow = new UnitOfWork())
            {
                try
                {
                    Servico antes = null;
                    // Alterar valor_base aqui nao afeta itens de OS ja
                    // existentes; cada item guarda o seu proprio valor_unitario.
                    if (servico.Id == 0)
                    {
                        _repo.Inserir(servico, uow);
                    }
                    else
                    {
                        antes = _repo.ObterPorId(servico.Id, uow);
                        _repo.Atualizar(servico, uow);
                    }

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "Servico",
                        IdRegistro = servico.Id.ToString(),
                        Operacao = antes == null ? OperacaoAuditoria.Insert : OperacaoAuditoria.Update,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao(),
                        SnapshotAntes = AuditoriaSerializer.ToJson(antes),
                        SnapshotDepois = AuditoriaSerializer.ToJson(servico)
                    }, uow);

                    uow.Commit();
                    return servico.Id;
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
                        throw new DominioException("Servico nao encontrado.");

                    _repo.Excluir(id, uow);

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "Servico",
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
                    _logger.Erro("Falha ao excluir servico", pex);
                    if (pex.SqlState == "23503")
                        throw new DominioException(
                            "Nao e possivel excluir, existem itens de OS usando esse servico.");
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
