using System;
using Npgsql;

namespace DeMariaTeste.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        NpgsqlConnection Conexao { get; }
        NpgsqlTransaction Transacao { get; }
        void Commit();
        void Rollback();
    }
}
