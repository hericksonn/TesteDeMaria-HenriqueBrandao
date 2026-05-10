namespace DeMariaTeste.Domain.Exceptions
{
    // Violacao de unique (ex.: documento duplicado).
    public class RegistroDuplicadoException : DominioException
    {
        public RegistroDuplicadoException(string mensagem) : base(mensagem) { }
    }
}
