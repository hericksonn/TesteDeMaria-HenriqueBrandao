using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace DeMariaTeste.Infrastructure.Data
{
    internal static class DbHelper
    {
        public static NpgsqlCommand Comando(NpgsqlConnection conexao, NpgsqlTransaction tran, string sql)
        {
            var cmd = new NpgsqlCommand(sql, conexao);
            if (tran != null) cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public static void AddParam(NpgsqlCommand cmd, string nome, NpgsqlDbType tipo, object valor)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = nome;
            p.NpgsqlDbType = tipo;
            p.Value = valor ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }

        public static string GetString(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.IsDBNull(idx) ? null : dr.GetString(idx);
        }

        public static int GetInt(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.GetInt32(idx);
        }

        public static int? GetIntNullable(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.IsDBNull(idx) ? (int?)null : dr.GetInt32(idx);
        }

        public static long GetLong(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.GetInt64(idx);
        }

        public static decimal GetDecimal(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.GetDecimal(idx);
        }

        public static bool GetBool(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.GetBoolean(idx);
        }

        public static DateTime GetDateTime(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.GetDateTime(idx);
        }

        public static DateTime? GetDateTimeNullable(IDataReader dr, string col)
        {
            int idx = dr.GetOrdinal(col);
            return dr.IsDBNull(idx) ? (DateTime?)null : dr.GetDateTime(idx);
        }
    }
}
