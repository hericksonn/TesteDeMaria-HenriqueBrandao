using System;
using System.Collections.Generic;
using System.Text;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Domain.Exceptions;
using DeMariaTeste.Infrastructure.Data;
using Npgsql;
using NpgsqlTypes;

namespace DeMariaTeste.Infrastructure.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        // A grid principal nao carrega itens; eles vem ao abrir a OS.
        public PaginacaoResultado<OrdemServico> Listar(int? clienteId, StatusOrdemServico? status,
            DateTime? de, DateTime? ate, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina <= 0) tamanhoPagina = 20;

            var resultado = new PaginacaoResultado<OrdemServico>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina
            };

            var where = new StringBuilder("WHERE 1=1 ");
            using (var conn = ConnectionFactory.Criar())
            using (var cmdCount = conn.CreateCommand())
            using (var cmdData = conn.CreateCommand())
            {
                if (clienteId.HasValue)
                {
                    where.Append("AND os.cliente_id = @cli ");
                    cmdCount.Parameters.AddWithValue("@cli", clienteId.Value);
                    cmdData.Parameters.AddWithValue("@cli", clienteId.Value);
                }
                if (status.HasValue)
                {
                    where.Append("AND os.status = @st ");
                    cmdCount.Parameters.AddWithValue("@st", status.Value.ToString());
                    cmdData.Parameters.AddWithValue("@st", status.Value.ToString());
                }
                if (de.HasValue)
                {
                    where.Append("AND os.data_abertura >= @de ");
                    cmdCount.Parameters.AddWithValue("@de", de.Value);
                    cmdData.Parameters.AddWithValue("@de", de.Value);
                }
                if (ate.HasValue)
                {
                    where.Append("AND os.data_abertura <= @ate ");
                    cmdCount.Parameters.AddWithValue("@ate", ate.Value);
                    cmdData.Parameters.AddWithValue("@ate", ate.Value);
                }

                cmdCount.CommandText = "SELECT COUNT(*) FROM ordens_servico os " + where;
                resultado.TotalRegistros = Convert.ToInt32(cmdCount.ExecuteScalar());

                cmdData.CommandText =
                    "SELECT os.id, os.cliente_id, c.nome AS cliente_nome, os.data_abertura, os.data_conclusao, " +
                    "       os.status, os.observacao, os.valor_total, os.versao " +
                    "FROM ordens_servico os " +
                    "INNER JOIN clientes c ON c.id = os.cliente_id " +
                    where +
                    "ORDER BY os.data_abertura DESC, os.id DESC " +
                    "LIMIT @limit OFFSET @offset";

                cmdData.Parameters.AddWithValue("@limit", tamanhoPagina);
                cmdData.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                using (var dr = cmdData.ExecuteReader())
                {
                    while (dr.Read()) resultado.Itens.Add(Mapear(dr, true));
                }
            }

            return resultado;
        }

        public IList<OrdemServico> ListarParaRelatorio(int? clienteId, StatusOrdemServico? status, DateTime? de, DateTime? ate)
        {
            var lista = new List<OrdemServico>();
            var where = new StringBuilder("WHERE 1=1 ");

            using (var conn = ConnectionFactory.Criar())
            using (var cmd = conn.CreateCommand())
            {
                if (clienteId.HasValue) { where.Append("AND os.cliente_id = @cli "); cmd.Parameters.AddWithValue("@cli", clienteId.Value); }
                if (status.HasValue)    { where.Append("AND os.status = @st ");      cmd.Parameters.AddWithValue("@st", status.Value.ToString()); }
                if (de.HasValue)        { where.Append("AND os.data_abertura >= @de "); cmd.Parameters.AddWithValue("@de", de.Value); }
                if (ate.HasValue)       { where.Append("AND os.data_abertura <= @ate "); cmd.Parameters.AddWithValue("@ate", ate.Value); }

                cmd.CommandText =
                    "SELECT os.id, os.cliente_id, c.nome AS cliente_nome, os.data_abertura, os.data_conclusao, " +
                    "       os.status, os.observacao, os.valor_total, os.versao " +
                    "FROM ordens_servico os " +
                    "INNER JOIN clientes c ON c.id = os.cliente_id " +
                    where +
                    "ORDER BY c.nome, os.data_abertura";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(Mapear(dr, true));
                }
            }

            // Carrega os itens em consulta separada para nao bagunçar a
            // cardinalidade do GROUP BY no relatorio.
            foreach (var os in lista)
            {
                os.Itens = CarregarItens(os.Id, null);
            }

            return lista;
        }

        public OrdemServico ObterPorId(int id, bool incluirItens, IUnitOfWork uow = null)
        {
            const string sql =
                "SELECT os.id, os.cliente_id, c.nome AS cliente_nome, os.data_abertura, os.data_conclusao, " +
                "       os.status, os.observacao, os.valor_total, os.versao " +
                "FROM ordens_servico os " +
                "INNER JOIN clientes c ON c.id = os.cliente_id " +
                "WHERE os.id = @id";

            OrdemServico os;
            if (uow != null)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        os = dr.Read() ? Mapear(dr, true) : null;
                    }
                }
            }
            else
            {
                using (var conn = ConnectionFactory.Criar())
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        os = dr.Read() ? Mapear(dr, true) : null;
                    }
                }
            }

            if (os != null && incluirItens)
                os.Itens = CarregarItens(os.Id, uow);

            return os;
        }

        public int Inserir(OrdemServico os, IUnitOfWork uow)
        {
            const string sql = @"
                INSERT INTO ordens_servico
                    (cliente_id, data_abertura, data_conclusao, status, observacao, valor_total, versao)
                VALUES
                    (@cli, @abertura, @concl, @st, @obs, @vl, @versao)
                RETURNING id";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@cli",     NpgsqlDbType.Integer,   os.ClienteId);
                DbHelper.AddParam(cmd, "@abertura",NpgsqlDbType.Timestamp, os.DataAbertura);
                DbHelper.AddParam(cmd, "@concl",   NpgsqlDbType.Timestamp, (object)os.DataConclusao);
                DbHelper.AddParam(cmd, "@st",      NpgsqlDbType.Varchar,   os.Status.ToString());
                DbHelper.AddParam(cmd, "@obs",     NpgsqlDbType.Text,      (object)os.Observacao);
                DbHelper.AddParam(cmd, "@vl",      NpgsqlDbType.Numeric,   os.ValorTotal);
                DbHelper.AddParam(cmd, "@versao",  NpgsqlDbType.Integer,   os.Versao);

                int id = (int)cmd.ExecuteScalar();
                os.Id = id;
                return id;
            }
        }

        // Concorrencia otimista: o WHERE inclui a versao atual e o SET
        // incrementa. Se outra sessao alterou a OS, RETURNING vem nulo.
        public void Atualizar(OrdemServico os, IUnitOfWork uow)
        {
            const string sql = @"
                UPDATE ordens_servico
                   SET cliente_id     = @cli,
                       data_conclusao = @concl,
                       status         = @st,
                       observacao     = @obs,
                       valor_total    = @vl,
                       versao         = versao + 1
                 WHERE id = @id AND versao = @versao
                 RETURNING versao";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@cli",    NpgsqlDbType.Integer,   os.ClienteId);
                DbHelper.AddParam(cmd, "@concl",  NpgsqlDbType.Timestamp, (object)os.DataConclusao);
                DbHelper.AddParam(cmd, "@st",     NpgsqlDbType.Varchar,   os.Status.ToString());
                DbHelper.AddParam(cmd, "@obs",    NpgsqlDbType.Text,      (object)os.Observacao);
                DbHelper.AddParam(cmd, "@vl",     NpgsqlDbType.Numeric,   os.ValorTotal);
                DbHelper.AddParam(cmd, "@id",     NpgsqlDbType.Integer,   os.Id);
                DbHelper.AddParam(cmd, "@versao", NpgsqlDbType.Integer,   os.Versao);

                object retorno = cmd.ExecuteScalar();
                if (retorno == null)
                {
                    throw new ConcorrenciaException(
                        "Esta OS foi alterada por outro usuario enquanto voce editava. " +
                        "Recarregue a tela e tente novamente.");
                }

                os.Versao = Convert.ToInt32(retorno);
            }
        }

        public void RemoverItens(int ordemServicoId, IUnitOfWork uow)
        {
            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao,
                "DELETE FROM itens_ordem_servico WHERE ordem_servico_id = @id"))
            {
                DbHelper.AddParam(cmd, "@id", NpgsqlDbType.Integer, ordemServicoId);
                cmd.ExecuteNonQuery();
            }
        }

        public void InserirItens(OrdemServico os, IUnitOfWork uow)
        {
            const string sql = @"
                INSERT INTO itens_ordem_servico
                    (ordem_servico_id, servico_id, quantidade, valor_unitario,
                     percentual_imposto_aplicado, valor_total_item)
                VALUES
                    (@os, @serv, @qtd, @vu, @imp, @tot)
                RETURNING id";

            foreach (var item in os.Itens)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    DbHelper.AddParam(cmd, "@os",   NpgsqlDbType.Integer, os.Id);
                    DbHelper.AddParam(cmd, "@serv", NpgsqlDbType.Integer, item.ServicoId);
                    DbHelper.AddParam(cmd, "@qtd",  NpgsqlDbType.Numeric, item.Quantidade);
                    DbHelper.AddParam(cmd, "@vu",   NpgsqlDbType.Numeric, item.ValorUnitario);
                    DbHelper.AddParam(cmd, "@imp",  NpgsqlDbType.Numeric, item.PercentualImpostoAplicado);
                    DbHelper.AddParam(cmd, "@tot",  NpgsqlDbType.Numeric, item.ValorTotalItem);
                    item.Id = (int)cmd.ExecuteScalar();
                    item.OrdemServicoId = os.Id;
                }
            }
        }

        public void RegistrarHistoricoStatus(HistoricoStatusOS hist, IUnitOfWork uow)
        {
            const string sql = @"
                INSERT INTO historico_status_os (ordem_servico_id, status_anterior, status_novo, data_hora, usuario)
                VALUES (@os, @ant, @novo, @dt, @user)";

            using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
            {
                DbHelper.AddParam(cmd, "@os",   NpgsqlDbType.Integer, hist.OrdemServicoId);
                DbHelper.AddParam(cmd, "@ant",  NpgsqlDbType.Varchar, hist.StatusAnterior.HasValue ? (object)hist.StatusAnterior.Value.ToString() : null);
                DbHelper.AddParam(cmd, "@novo", NpgsqlDbType.Varchar, hist.StatusNovo.ToString());
                DbHelper.AddParam(cmd, "@dt",   NpgsqlDbType.Timestamp, hist.DataHora);
                DbHelper.AddParam(cmd, "@user", NpgsqlDbType.Varchar, hist.Usuario ?? "sistema");
                cmd.ExecuteNonQuery();
            }
        }

        private List<ItemOrdemServico> CarregarItens(int ordemId, IUnitOfWork uow)
        {
            var lista = new List<ItemOrdemServico>();
            const string sql =
                "SELECT i.id, i.ordem_servico_id, i.servico_id, s.nome AS servico_nome, " +
                "       i.quantidade, i.valor_unitario, i.percentual_imposto_aplicado, i.valor_total_item " +
                "FROM itens_ordem_servico i " +
                "INNER JOIN servicos s ON s.id = i.servico_id " +
                "WHERE i.ordem_servico_id = @os ORDER BY i.id";

            if (uow != null)
            {
                using (var cmd = DbHelper.Comando(uow.Conexao, uow.Transacao, sql))
                {
                    DbHelper.AddParam(cmd, "@os", NpgsqlDbType.Integer, ordemId);
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read()) lista.Add(MapearItem(dr));
                }
                return lista;
            }

            using (var conn = ConnectionFactory.Criar())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@os", ordemId);
                using (var dr = cmd.ExecuteReader())
                    while (dr.Read()) lista.Add(MapearItem(dr));
            }
            return lista;
        }

        private OrdemServico Mapear(System.Data.IDataReader dr, bool comCliente)
        {
            string st = DbHelper.GetString(dr, "status");
            StatusOrdemServico parsed;
            if (!Enum.TryParse(st, true, out parsed))
                parsed = StatusOrdemServico.Aberta;

            var os = new OrdemServico
            {
                Id = DbHelper.GetInt(dr, "id"),
                ClienteId = DbHelper.GetInt(dr, "cliente_id"),
                DataAbertura = DbHelper.GetDateTime(dr, "data_abertura"),
                DataConclusao = DbHelper.GetDateTimeNullable(dr, "data_conclusao"),
                Status = parsed,
                Observacao = DbHelper.GetString(dr, "observacao"),
                ValorTotal = DbHelper.GetDecimal(dr, "valor_total"),
                Versao = DbHelper.GetInt(dr, "versao")
            };

            if (comCliente)
                os.ClienteNome = DbHelper.GetString(dr, "cliente_nome");

            return os;
        }

        private ItemOrdemServico MapearItem(System.Data.IDataReader dr)
        {
            return new ItemOrdemServico
            {
                Id = DbHelper.GetInt(dr, "id"),
                OrdemServicoId = DbHelper.GetInt(dr, "ordem_servico_id"),
                ServicoId = DbHelper.GetInt(dr, "servico_id"),
                ServicoNome = DbHelper.GetString(dr, "servico_nome"),
                Quantidade = DbHelper.GetDecimal(dr, "quantidade"),
                ValorUnitario = DbHelper.GetDecimal(dr, "valor_unitario"),
                PercentualImpostoAplicado = DbHelper.GetDecimal(dr, "percentual_imposto_aplicado"),
                ValorTotalItem = DbHelper.GetDecimal(dr, "valor_total_item")
            };
        }
    }
}
