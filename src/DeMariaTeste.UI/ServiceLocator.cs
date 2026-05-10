using DeMariaTeste.Application.Services;
using DeMariaTeste.Infrastructure.Logging;
using DeMariaTeste.Infrastructure.Repositories;

namespace DeMariaTeste.UI
{
    public static class ServiceLocator
    {
        private static readonly ILogger _logger = new FileLogger();

        private static readonly IClienteRepository _repoCliente = new ClienteRepository();
        private static readonly IServicoRepository _repoServico = new ServicoRepository();
        private static readonly IOrdemServicoRepository _repoOs = new OrdemServicoRepository();
        private static readonly IAuditoriaRepository _repoAudit = new AuditoriaRepository();

        public static ILogger Logger { get { return _logger; } }

        public static ClienteService CriarClienteService()
        {
            return new ClienteService(_repoCliente, _repoAudit, _logger);
        }

        public static ServicoService CriarServicoService()
        {
            return new ServicoService(_repoServico, _repoAudit, _logger);
        }

        public static OrdemServicoService CriarOrdemServicoService()
        {
            return new OrdemServicoService(_repoOs, _repoAudit, _logger);
        }

        public static RelatorioService CriarRelatorioService()
        {
            return new RelatorioService(_repoOs, _repoCliente);
        }
    }
}
