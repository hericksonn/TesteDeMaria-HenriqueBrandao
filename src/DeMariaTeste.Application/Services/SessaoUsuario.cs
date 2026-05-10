namespace DeMariaTeste.Application.Services
{
    // Usuario logado para preencher auditoria e historico de status.
    public static class SessaoUsuario
    {
        public static string UsuarioLogado { get; set; }

        public static string ObterUsuarioOuPadrao()
        {
            return string.IsNullOrWhiteSpace(UsuarioLogado) ? "sistema" : UsuarioLogado;
        }
    }
}
