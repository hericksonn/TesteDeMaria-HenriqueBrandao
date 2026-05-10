using System;
using DeMariaTeste.Domain.Exceptions;
using Npgsql;

namespace DeMariaTeste.UI.Forms.Common
{
    internal static class TratadorErro
    {
        public static void Tratar(Exception ex, string contexto = null)
        {
            if (ex == null) return;

            if (ex is DominioException)
            {
                Mensagens.Aviso(ex.Message);
                ServiceLocator.Logger.Aviso(
                    (contexto ?? "Regra de negocio") + ": " + ex.Message);
                return;
            }

            var pgex = ex as PostgresException;
            if (pgex != null)
            {
                ServiceLocator.Logger.Erro(
                    (contexto ?? "Erro PostgreSQL") + " - SqlState " + pgex.SqlState, pgex);

                if (pgex.SqlState == "23505")
                {
                    Mensagens.Aviso("Registro duplicado. Verifique os dados informados.");
                    return;
                }
                if (pgex.SqlState == "23503")
                {
                    Mensagens.Aviso("Existem registros dependentes desta informacao.");
                    return;
                }

                Mensagens.Erro("Falha ao acessar o banco de dados. Detalhes no log.");
                return;
            }

            ServiceLocator.Logger.Erro(contexto ?? "Erro inesperado", ex);
            Mensagens.Erro("Ocorreu um erro: " + ex.Message);
        }
    }
}
