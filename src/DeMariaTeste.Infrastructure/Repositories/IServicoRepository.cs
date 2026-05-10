using System.Collections.Generic;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Infrastructure.Data;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public interface IServicoRepository
    {
        PaginacaoResultado<Servico> Listar(string filtroNome, bool? ativo, int pagina, int tamanhoPagina);
        IList<Servico> ListarAtivos();
        Servico ObterPorId(int id, IUnitOfWork uow = null);
        int Inserir(Servico servico, IUnitOfWork uow);
        void Atualizar(Servico servico, IUnitOfWork uow);
        void Excluir(int id, IUnitOfWork uow);
    }
}
