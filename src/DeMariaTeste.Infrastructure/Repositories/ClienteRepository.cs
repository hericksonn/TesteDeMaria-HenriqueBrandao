using System.Collections.Generic;
using System.Text;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Infrastructure.Data;
using Npgsql;
using NpgsqlTypes;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public PaginacaoResultado<Cliente> Listar(string filtroNome, string filtroDocumento, bool? ativo, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 20;

            var resultado = new PaginacaoResultado<Cliente>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina
            };

            var where = new StringBuilder("WHERE 1=1 ");
            using (var conn = ConnectionFactory.Criar())
            {
                using (var cmdCount = conn.CreateCommand())
                using (var cmdData = conn.CreateCommand())
                {
                    if (!string.IsNullOrWhiteSpace(filtroNome))
                    {
                        where.Append("AND LOWER(nome) LIKE @nome ");
                        cmdCount.Parameters.AddWithValue("@nome", "%" + filtroNome.ToLower() + "%");
                        cmdData.Parameters.AddWithValue("@nome", "%" + filtroNome.ToLower() + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(filtroDocumento))
                    {
                        where.Append("AND documento LIKE @doc ");
                        cmdCount.Parameters.AddWithValue("@doc", "%" + filtroDocumento + "%");
                        cmdData.Parameters.AddWithValue("@doc", "%" + filtroDocumento + "%");
                    }

                    if (ativo.HasValue)
                    {
                        where.Append("AND ativo = @ativo ");
                        cmdCount.Parameters.AddWithValue("@ativo", ativo.Value);
                        cmdData.Parameters.AddWithValue("@ativo", ativo.Value);
                    }

                    cmdCount.CommandText = "SELECT COUNT(*) FROM clientes " + where;
                    resultado.TotalRegistros = System.Convert.ToInt32(cmdCount.ExecuteScalar());

                    cmdData.CommandText =
                        "SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo " +
                        "FROM clientes " + where +
                        "ORDER BY nome " +
                        "LIMIT @limit OFFSET @offset";

                    cmdData.Parameters.AddWithValue("@limit", tamanhoPagina);
                    cmdData.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                    using (var dr = cmdData.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            resultado.Itens.Add(MapearCliente(dr));
                        }
                    }
                }
            }

            return resultado;
        }

        public Cliente ObterPorId(int id, IUnitOfWork uow = null)
        {
            return ObterPorWhere("id = @id", new[] { new NpgsqlParameter("@id", id) }, uow);
        }

        public Cliente ObterPorDocumento(string documento, IUnitOfWork uow = null)
        {
            return ObterPorWhere("documento = @doc", new[] { new NpgsqlParameter("@doc", documento) }, uow);
        }

        public int Inserir(Cliente cliente, IUnitOfWork uow)
        {
            const string sql = @"
                INSERT INTO clientes (nome, documento, tipo, email, telefone, data_cadastro, ativo)
                VALUES (@nome, @doc, @tipo, @email, @tel, @dt, @ativo)
                RETURNING id";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@nome",  NpgsqlDbType.Varchar,   cliente.Nome);
                DbHelper.AddParam(cmd, "@doc",   NpgsqlDbType.Varchar,   cliente.Documento);
                DbHelper.AddParam(cmd, "@tipo",  NpgsqlDbType.Char,      cliente.Tipo == TipoCliente.Fisica ? "F" : "J");
                DbHelper.AddParam(cmd, "@email", NpgsqlDbType.Varchar,   (object)cliente.Email);
                DbHelper.AddParam(cmd, "@tel",   NpgsqlDbType.Varchar,   (object)cliente.Telefone);
                DbHelper.AddParam(cmd, "@dt",    NpgsqlDbType.Timestamp, cliente.DataCadastro);
                DbHelper.AddParam(cmd, "@ativo", NpgsqlDbType.Boolean,   cliente.Ativo);

                var id = (int)cmd.ExecuteScalar();
                cliente.Id = id;
                return id;
            }
        }

        public void Atualizar(Cliente cliente, IUnitOfWork uow)
        {
            const string sql = @"
                UPDATE clientes
                   SET nome = @nome,
                       documento = @doc,
                       tipo = @tipo,
                       email = @email,
                       telefone = @tel,
                       ativo = @ativo
                 WHERE id = @id";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@nome",  NpgsqlDbType.Varchar, cliente.Nome);
                DbHelper.AddParam(cmd, "@doc",   NpgsqlDbType.Varchar, cliente.Documento);
                DbHelper.AddParam(cmd, "@tipo",  NpgsqlDbType.Char,    cliente.Tipo == TipoCliente.Fisica ? "F" : "J");
                DbHelper.AddParam(cmd, "@email", NpgsqlDbType.Varchar, (object)cliente.Email);
                DbHelper.AddParam(cmd, "@tel",   NpgsqlDbType.Varchar, (object)cliente.Telefone);
                DbHelper.AddParam(cmd, "@ativo", NpgsqlDbType.Boolean, cliente.Ativo);
                DbHelper.AddParam(cmd, "@id",    NpgsqlDbType.Integer, cliente.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int id, IUnitOfWork uow)
        {
            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, "DELETE FROM clientes WHERE id = @id"))
            {
                DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, id);
                cmd.ExecuteNonQuery();
            }
        }

        public bool PossuiOrdemServicoVinculada(int clienteId, IUnitOfWork uow = null)
        {
            const string sql = "SELECT 1 FROM ordens_servico WHERE cliente_id = @id LIMIT 1";

            if (uow != null)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, clienteId);
                    return cmd.ExecuteScalar() != null;
                }
            }

            using (var conn = ConnectionFactory.Criar())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", clienteId);
                return cmd.ExecuteScalar() != null;
            }
        }

        private Cliente ObterPorWhere(string filtro, NpgsqlParameter[] parametros, IUnitOfWork uow)
        {
            string sql =
                "SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo " +
                "FROM clientes WHERE " + filtro + " LIMIT 1";

            if (uow != null)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    foreach (var p in parametros) cmd.Parameters.Add(p);
                    using (var dr = cmd.ExecuteReader())
                    {
                        return dr.Read() ? MapearCliente(dr) : null;
                    }
                }
            }

            using (var conn = ConnectionFactory.Criar())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                foreach (var p in parametros) cmd.Parameters.Add(p);
                using (var dr = cmd.ExecuteReader())
                {
                    return dr.Read() ? MapearCliente(dr) : null;
                }
            }
        }

        private Cliente MapearCliente(System.Data.IDataReader dr)
        {
            string tipo = DbHelper.GetString(dr, "tipo");
            return new Cliente
            {
                Id = DbHelper.GetInt(dr, "id"),
                Nome = DbHelper.GetString(dr, "nome"),
                Documento = DbHelper.GetString(dr, "documento"),
                Tipo = tipo == "J" ? TipoCliente.Juridica : TipoCliente.Fisica,
                Email = DbHelper.GetString(dr, "email"),
                Telefone = DbHelper.GetString(dr, "telefone"),
                DataCadastro = DbHelper.GetDateTime(dr, "data_cadastro"),
                Ativo = DbHelper.GetBool(dr, "ativo")
            };
        }
    }
}
