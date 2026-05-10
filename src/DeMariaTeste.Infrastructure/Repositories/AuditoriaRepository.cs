using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Infrastructure.Data;
using NpgsqlTypes;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        public void Registrar(Auditoria a, IUnitOfWork uow)
        {
            // NpgsqlDbType.Jsonb faz o driver enviar o texto ja com o cast.
            const string sql = @"
                INSERT INTO auditoria
                    (entidade, id_registro, operacao, data_hora, usuario, snapshot_antes, snapshot_depois)
                VALUES
                    (@ent, @idreg, @op, @dt, @user, @antes, @depois)";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@ent",    NpgsqlDbType.Varchar,   a.Entidade);
                DbHelper.AddParam(cmd, "@idreg",  NpgsqlDbType.Varchar,   a.IdRegistro);
                DbHelper.AddParam(cmd, "@op",     NpgsqlDbType.Varchar,   a.Operacao.ToString().ToUpper());
                DbHelper.AddParam(cmd, "@dt",     NpgsqlDbType.Timestamp, a.DataHora);
                DbHelper.AddParam(cmd, "@user",   NpgsqlDbType.Varchar,   a.Usuario ?? "sistema");
                DbHelper.AddParam(cmd, "@antes",  NpgsqlDbType.Jsonb,     (object)a.SnapshotAntes);
                DbHelper.AddParam(cmd, "@depois", NpgsqlDbType.Jsonb,     (object)a.SnapshotDepois);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
