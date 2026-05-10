using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Infrastructure.Data;

namespace DeMariaTeste.Infrastructure.Repositories
{
    // Os metodos aceitam IUnitOfWork opcional. Se null, o repositorio
    // abre uma conexao propria; se informado, participa da transacao.
    public interface IClienteRepository
    {
        PaginacaoResultado<Cliente> Listar(string filtroNome, string filtroDocumento, bool? ativo, int pagina, int tamanhoPagina);
        Cliente ObterPorId(int id, IUnitOfWork uow = null);
        Cliente ObterPorDocumento(string documento, IUnitOfWork uow = null);
        int Inserir(Cliente cliente, IUnitOfWork uow);
        void Atualizar(Cliente cliente, IUnitOfWork uow);
        void Excluir(int id, IUnitOfWork uow);
        bool PossuiOrdemServicoVinculada(int clienteId, IUnitOfWork uow = null);
    }
}
