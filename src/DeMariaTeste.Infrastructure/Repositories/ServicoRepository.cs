using System.Collections.Generic;
using System.Text;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Infrastructure.Data;
using Npgsql;
using NpgsqlTypes;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        public PaginacaoResultado<Servico> Listar(string filtroNome, bool? ativo, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 20;

            var resultado = new PaginacaoResultado<Servico>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina
            };

            var where = new StringBuilder("WHERE 1=1 ");
            using (var conn = ConnectionFactory.Criar())
            using (var cmdCount = conn.CreateCommand())
            using (var cmdData = conn.CreateCommand())
            {
                if (!string.IsNullOrWhiteSpace(filtroNome))
                {
                    where.Append("AND LOWER(nome) LIKE @nome ");
                    cmdCount.Parameters.AddWithValue("@nome", "%" + filtroNome.ToLower() + "%");
                    cmdData.Parameters.AddWithValue("@nome", "%" + filtroNome.ToLower() + "%");
                }

                if (ativo.HasValue)
                {
                    where.Append("AND ativo = @ativo ");
                    cmdCount.Parameters.AddWithValue("@ativo", ativo.Value);
                    cmdData.Parameters.AddWithValue("@ativo", ativo.Value);
                }

                cmdCount.CommandText = "SELECT COUNT(*) FROM servicos " + where;
                resultado.TotalRegistros = System.Convert.ToInt32(cmdCount.ExecuteScalar());

                cmdData.CommandText =
                    "SELECT id, nome, valor_base, percentual_imposto, ativo " +
                    "FROM servicos " + where +
                    "ORDER BY nome " +
                    "LIMIT @limit OFFSET @offset";

                cmdData.Parameters.AddWithValue("@limit", tamanhoPagina);
                cmdData.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                using (var dr = cmdData.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultado.Itens.Add(Mapear(dr));
                    }
                }
            }

            return resultado;
        }

        public IList<Servico> ListarAtivos()
        {
            var lista = new List<Servico>();
            using (var conn = ConnectionFactory.Criar())
            using (var cmd = new NpgsqlCommand(
                "SELECT id, nome, valor_base, percentual_imposto, ativo FROM servicos WHERE ativo = TRUE ORDER BY nome", conn))
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read()) lista.Add(Mapear(dr));
            }
            return lista;
        }

        public Servico ObterPorId(int id, IUnitOfWork uow = null)
        {
            const string sql = "SELECT id, nome, valor_base, percentual_imposto, ativo FROM servicos WHERE id = @id";

            if (uow != null)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, id);
                    using (var dr = cmd.ExecuteReader())
                        return dr.Read() ? Mapear(dr) : null;
                }
            }

            using (var conn = ConnectionFactory.Criar())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                    return dr.Read() ? Mapear(dr) : null;
            }
        }

        public int Inserir(Servico servico, IUnitOfWork uow)
        {
            const string sql = @"
                INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo)
                VALUES (@nome, @valor, @imp, @ativo)
                RETURNING id";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@nome",  NpgsqlDbType.Varchar, servico.Nome);
                DbHelper.AddParam(cmd, "@valor", NpgsqlDbType.Numeric, servico.ValorBase);
                DbHelper.AddParam(cmd, "@imp",   NpgsqlDbType.Numeric, servico.PercentualImposto);
                DbHelper.AddParam(cmd, "@ativo", NpgsqlDbType.Boolean, servico.Ativo);

                int id = (int)cmd.ExecuteScalar();
                servico.Id = id;
                return id;
            }
        }

        public void Atualizar(Servico servico, IUnitOfWork uow)
        {
            const string sql = @"
                UPDATE servicos
                   SET nome = @nome,
                       valor_base = @valor,
                       percentual_imposto = @imp,
                       ativo = @ativo
                 WHERE id = @id";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@nome",  NpgsqlDbType.Varchar, servico.Nome);
                DbHelper.AddParam(cmd, "@valor", NpgsqlDbType.Numeric, servico.ValorBase);
                DbHelper.AddParam(cmd, "@imp",   NpgsqlDbType.Numeric, servico.PercentualImposto);
                DbHelper.AddParam(cmd, "@ativo", NpgsqlDbType.Boolean, servico.Ativo);
                DbHelper.AddParam(cmd, "@id",    NpgsqlDbType.Integer, servico.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int id, IUnitOfWork uow)
        {
            // ON DELETE RESTRICT do banco trata o caso de servico em uso por OS.
            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, "DELETE FROM servicos WHERE id = @id"))
            {
                DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, id);
                cmd.ExecuteNonQuery();
            }
        }

        private Servico Mapear(System.Data.IDataReader dr)
        {
            return new Servico
            {
                Id = DbHelper.GetInt(dr, "id"),
                Nome = DbHelper.GetString(dr, "nome"),
                ValorBase = DbHelper.GetDecimal(dr, "valor_base"),
                PercentualImposto = DbHelper.GetDecimal(dr, "percentual_imposto"),
                Ativo = DbHelper.GetBool(dr, "ativo")
            };
        }
    }
}
