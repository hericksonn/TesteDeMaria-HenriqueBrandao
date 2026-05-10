using System;
using System.Configuration;
using Npgsql;

namespace DeMariaTeste.Infrastructure.Data
{
    public static class ConnectionFactory
    {
        private const string NomeConexao = "DeMariaDb";

        public static NpgsqlConnection Criar()
        {
            var settings = ConfigurationManager.ConnectionStrings[NomeConexao];

            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new InvalidOperationException(
                    "Connection string '" + NomeConexao + "' nao foi encontrada no App.config.");

            var conn = new NpgsqlConnection(settings.ConnectionString);

            // Aceita o certificado do servidor sem precisar da CA na maquina
            // cliente. No Npgsql 3.2 nao da pra usar "Trust Server Certificate"
            // direto na connection string.
            conn.UserCertificateValidationCallback = (sender, cert, chain, errors) => true;

            conn.Open();
            return conn;
        }
    }
}
