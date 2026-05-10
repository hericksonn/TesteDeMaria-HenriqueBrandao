using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Infrastructure.Data;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public interface IAuditoriaRepository
    {
        void Registrar(Auditoria a, IUnitOfWork uow);
    }
}
