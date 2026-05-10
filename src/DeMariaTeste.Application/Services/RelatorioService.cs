using System;
using System.Collections.Generic;
using DeMariaTeste.Application.Reports;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.Infrastructure.Repositories;

namespace DeMariaTeste.Application.Services
{
    // Monta a projecao usada pelo ReportViewer.
    public class RelatorioService
    {
        private readonly IOrdemServicoRepository _repoOs;
        private readonly IClienteRepository _repoCliente;

        public RelatorioService(IOrdemServicoRepository repoOs, IClienteRepository repoCliente)
        {
            _repoOs = repoOs;
            _repoCliente = repoCliente;
        }

        public IList<LinhaRelatorioOS> GerarLinhas(int? clienteId, StatusOrdemServico? status, DateTime? de, DateTime? ate)
        {
            var linhas = new List<LinhaRelatorioOS>();
            var lista = _repoOs.ListarParaRelatorio(clienteId, status, de, ate);

            foreach (var os in lista)
            {
                decimal imposto = 0m;
                if (os.Itens != null)
                {
                    foreach (var item in os.Itens)
                    {
                        decimal subtotal = item.Quantidade * item.ValorUnitario;
                        imposto += subtotal * (item.PercentualImpostoAplicado / 100m);
                    }
                }

                // Para volume maior valeria fazer JOIN no ListarParaRelatorio.
                Cliente cli = _repoCliente.ObterPorId(os.ClienteId);

                linhas.Add(new LinhaRelatorioOS
                {
                    OrdemServicoId = os.Id,
                    Cliente = os.ClienteNome ?? (cli != null ? cli.Nome : ""),
                    Documento = cli != null ? cli.Documento : "",
                    DataAbertura = os.DataAbertura,
                    DataConclusao = os.DataConclusao,
                    Status = os.Status.ToString(),
                    ValorTotal = os.ValorTotal,
                    ValorImposto = imposto
                });
            }

            return linhas;
        }

        public TotaisRelatorio CalcularTotais(IList<LinhaRelatorioOS> linhas)
        {
            var t = new TotaisRelatorio();
            if (linhas == null) return t;

            t.QuantidadeOS = linhas.Count;
            foreach (var l in linhas)
            {
                t.TotalGeral += l.ValorTotal;
                t.TotalImposto += l.ValorImposto;
            }
            return t;
        }
    }
}
