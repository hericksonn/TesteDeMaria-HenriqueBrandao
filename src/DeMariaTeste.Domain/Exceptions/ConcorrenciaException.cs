namespace DeMariaTeste.Domain.Exceptions
{
    // UPDATE da OS afetou zero linhas porque a versao mudou.
    public class ConcorrenciaException : DominioException
    {
        public ConcorrenciaException(string mensagem) : base(mensagem) { }
    }
}
