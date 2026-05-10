using System;

namespace DeMariaTeste.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string mensagem);
        void Aviso(string mensagem);
        void Erro(string mensagem, Exception ex = null);
    }
}
