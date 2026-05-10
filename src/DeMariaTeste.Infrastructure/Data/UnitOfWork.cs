using System;
using Npgsql;

namespace DeMariaTeste.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlConnection _conn;
        private NpgsqlTransaction _tran;
        private bool _finalizada;

        public UnitOfWork()
        {
            _conn = ConnectionFactory.Criar();
            _tran = _conn.BeginTransaction();
        }

        public NpgsqlConnection Conexao
        {
            get { return _conn; }
        }

        public NpgsqlTransaction Transacao
        {
            get { return _tran; }
        }

        public void Commit()
        {
            if (_finalizada) return;
            _tran.Commit();
            _finalizada = true;
        }

        public void Rollback()
        {
            if (_finalizada) return;
            try
            {
                _tran.Rollback();
            }
            finally
            {
                _finalizada = true;
            }
        }

        public void Dispose()
        {
            try
            {
                // Garante rollback se a transacao nao foi finalizada.
                if (!_finalizada && _tran != null)
                {
                    try { _tran.Rollback(); } catch { }
                }
            }
            finally
            {
                if (_tran != null) _tran.Dispose();
                if (_conn != null) _conn.Dispose();
                _tran = null;
                _conn = null;
            }
        }
    }
}
