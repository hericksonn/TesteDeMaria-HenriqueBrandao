using System;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;
using DeMariaTeste.Infrastructure.Data;
using DeMariaTeste.Infrastructure.Logging;
using DeMariaTeste.Infrastructure.Repositories;
using Npgsql;

namespace DeMariaTeste.Application.Services
{
    public class OrdemServicoService
    {
        private readonly IOrdemServicoRepository _repoOs;
        private readonly IAuditoriaRepository _audit;
        private readonly ILogger _logger;

        public OrdemServicoService(IOrdemServicoRepository repoOs, IAuditoriaRepository audit, ILogger logger)
        {
            _repoOs = repoOs;
            _audit = audit;
            _logger = logger;
        }

        public PaginacaoResultado<OrdemServico> Listar(int? clienteId, StatusOrdemServico? status,
            DateTime? de, DateTime? ate, int pagina, int tamanho)
        {
            return _repoOs.Listar(clienteId, status, de, ate, pagina, tamanho);
        }

        public OrdemServico ObterPorId(int id)
        {
            return _repoOs.ObterPorId(id, true);
        }

        // Insert ou update da OS inteira em transacao unica.
        public int Salvar(OrdemServico os)
        {
            // Recalcula antes de validar; o usuario pode ter editado
            // quantidade/valor direto na grid sem disparar evento.
            os.RecalcularValorTotal();
            os.Validar();

            using (var uow = new UnitOfWork())
            {
                try
                {
                    OrdemServico antes = null;
                    bool eInsert = os.Id == 0;

                    if (eInsert)
                    {
                        os.Versao = 1;
                        _repoOs.Inserir(os, uow);

                        _repoOs.RegistrarHistoricoStatus(new HistoricoStatusOS
                        {
                            OrdemServicoId = os.Id,
                            StatusAnterior = null,
                            StatusNovo = os.Status,
                            Usuario = SessaoUsuario.ObterUsuarioOuPadrao()
                        }, uow);
                    }
                    else
                    {
                        antes = _repoOs.ObterPorId(os.Id, true, uow);
                        if (antes == null)
                            throw new DominioException("Ordem de Servico nao encontrada.");

                        if (!antes.PodeEditar())
                            throw new DominioException("Esta OS esta finalizada e nao pode mais ser alterada.");

                        if (os.Status == StatusOrdemServico.Concluida)
                            os.GarantirConclusaoValida();

                        if (antes.Status != os.Status)
                        {
                            _repoOs.RegistrarHistoricoStatus(new HistoricoStatusOS
                            {
                                OrdemServicoId = os.Id,
                                StatusAnterior = antes.Status,
                                StatusNovo = os.Status,
                                Usuario = SessaoUsuario.ObterUsuarioOuPadrao()
                            }, uow);
                        }

                        _repoOs.Atualizar(os, uow);
                    }

                    // Apaga e reinsere os itens na mesma transacao para
                    // evitar diff item a item.
                    _repoOs.RemoverItens(os.Id, uow);
                    _repoOs.InserirItens(os, uow);

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "OrdemServico",
                        IdRegistro = os.Id.ToString(),
                        Operacao = eInsert ? OperacaoAuditoria.Insert : OperacaoAuditoria.Update,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao(),
                        SnapshotAntes = AuditoriaSerializer.ToJson(antes),
                        SnapshotDepois = AuditoriaSerializer.ToJson(os)
                    }, uow);

                    uow.Commit();
                    return os.Id;
                }
                catch (ConcorrenciaException)
                {
                    uow.Rollback();
                    throw;
                }
                catch (PostgresException pex)
                {
                    uow.Rollback();
                    _logger.Erro("Falha ao gravar OS", pex);

                    if (pex.SqlState == "23503")
                        throw new DominioException("Cliente ou servico inexistente nessa OS.");

                    if (pex.SqlState == "23505")
                        throw new DominioException("Registro duplicado ao salvar a OS.");

                    throw;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public void Cancelar(int id, string motivo)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    var os = _repoOs.ObterPorId(id, true, uow);
                    if (os == null) throw new DominioException("OS nao encontrada.");
                    if (!os.PodeEditar()) throw new DominioException("OS ja esta finalizada.");

                    var statusAntigo = os.Status;
                    os.Status = StatusOrdemServico.Cancelada;
                    os.Observacao = string.IsNullOrWhiteSpace(motivo)
                        ? os.Observacao
                        : (os.Observacao ?? "") + Environment.NewLine + "[Cancelamento] " + motivo;

                    _repoOs.Atualizar(os, uow);
                    _repoOs.RegistrarHistoricoStatus(new HistoricoStatusOS
                    {
                        OrdemServicoId = os.Id,
                        StatusAnterior = statusAntigo,
                        StatusNovo = os.Status,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao()
                    }, uow);

                    _audit.Registrar(new Auditoria
                    {
                        Entidade = "OrdemServico",
                        IdRegistro = os.Id.ToString(),
                        Operacao = OperacaoAuditoria.Update,
                        Usuario = SessaoUsuario.ObterUsuarioOuPadrao(),
                        SnapshotAntes = AuditoriaSerializer.ToJson(new { Status = statusAntigo }),
                        SnapshotDepois = AuditoriaSerializer.ToJson(new { Status = os.Status, Motivo = motivo })
                    }, uow);

                    uow.Commit();
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
