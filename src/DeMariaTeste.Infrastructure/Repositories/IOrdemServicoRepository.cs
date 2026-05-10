using System;
using System.Collections.Generic;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Infrastructure.Data;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public interface IOrdemServicoRepository
    {
        PaginacaoResultado<OrdemServico> Listar(int? clienteId, StatusOrdemServico? status,
            DateTime? de, DateTime? ate, int pagina, int tamanhoPagina);

        // Listagem usada pelo relatorio: sem paginacao, com itens.
        IList<OrdemServico> ListarParaRelatorio(int? clienteId, StatusOrdemServico? status,
            DateTime? de, DateTime? ate);

        OrdemServico ObterPorId(int id, bool incluirItens, IUnitOfWork uow = null);

        int Inserir(OrdemServico os, IUnitOfWork uow);
        // Lanca ConcorrenciaException quando a versao da OS no banco mudou.
        void Atualizar(OrdemServico os, IUnitOfWork uow);

        void RemoverItens(int ordemServicoId, IUnitOfWork uow);
        void InserirItens(OrdemServico os, IUnitOfWork uow);

        void RegistrarHistoricoStatus(HistoricoStatusOS hist, IUnitOfWork uow);
    }
}
