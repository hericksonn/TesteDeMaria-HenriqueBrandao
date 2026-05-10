using System;
using System.IO;

namespace DeMariaTeste.Infrastructure.Logging
{
    // Escreve em logs/app-yyyyMMdd.log na pasta do executavel.
    public class FileLogger : ILogger
    {
        private static readonly object _trava = new object();
        private readonly string _pastaLog;

        public FileLogger()
        {
            _pastaLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(_pastaLog))
                Directory.CreateDirectory(_pastaLog);
        }

        public void Info(string mensagem)
        {
            Escrever("INFO", mensagem, null);
        }

        public void Aviso(string mensagem)
        {
            Escrever("AVISO", mensagem, null);
        }

        public void Erro(string mensagem, Exception ex = null)
        {
            Escrever("ERRO", mensagem, ex);
        }

        private void Escrever(string nivel, string mensagem, Exception ex)
        {
            try
            {
                string arquivo = Path.Combine(_pastaLog, "app-" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                string linha = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}",
                    DateTime.Now, nivel, mensagem);

                if (ex != null)
                {
                    var atual = ex;
                    var nivelEx = 0;
                    while (atual != null)
                    {
                        linha += Environment.NewLine +
                                 string.Format("  -> ({0}) {1}: {2}", nivelEx, atual.GetType().Name, atual.Message);

                        if (!string.IsNullOrWhiteSpace(atual.StackTrace))
                            linha += Environment.NewLine + atual.StackTrace;

                        atual = atual.InnerException;
                        nivelEx++;
                    }
                }

                lock (_trava)
                {
                    File.AppendAllText(arquivo, linha + Environment.NewLine);
                }
            }
            catch
            {
                // Falha de log nao deve derrubar a aplicacao.
            }
        }
    }
}
