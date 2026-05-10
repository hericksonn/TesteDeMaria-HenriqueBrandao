using System;

namespace DeMariaTeste.Domain.Exceptions
{
    // Base das excecoes de regra de negocio. O TratadorErro mostra a
    // mensagem para o usuario sem detalhes tecnicos.
    public class DominioException : Exception
    {
        public DominioException(string mensagem) : base(mensagem) { }
        public DominioException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }
}
