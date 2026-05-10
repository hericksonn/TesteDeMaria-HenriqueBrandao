using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DeMariaTeste.Application.Reports;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.UI.Forms.Common;
using Microsoft.Reporting.WinForms;

namespace DeMariaTeste.UI.Forms.Relatorios
{
    public partial class FormRelatorioOS : Form
    {
        private readonly RelatorioService _service;
        private readonly ClienteService _serviceCliente;
        private IList<LinhaRelatorioOS> _ultimoResultado;

        public FormRelatorioOS()
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            _service = ServiceLocator.CriarRelatorioService();
            _serviceCliente = ServiceLocator.CriarClienteService();

            cboStatus.Items.Add("Todos");
            foreach (var st in Enum.GetValues(typeof(StatusOrdemServico)))
                cboStatus.Items.Add(st.ToString());
            cboStatus.SelectedIndex = 0;

            var clientes = _serviceCliente.Listar(null, null, true, 1, 1000).Itens;
            cboCliente.DataSource = clientes;
            cboCliente.DisplayMember = "Nome";
            cboCliente.ValueMember = "Id";
            cboCliente.SelectedIndex = -1;

            this.Load += (s, e) =>
            {
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource = "DeMariaTeste.UI.Reports.RelatorioOS.rdlc";
            };
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                StatusOrdemServico? status = null;
                if (cboStatus.SelectedIndex > 0)
                {
                    StatusOrdemServico tmp;
                    if (Enum.TryParse(cboStatus.SelectedItem.ToString(), out tmp))
                        status = tmp;
                }

                int? clienteId = null;
                if (cboCliente.SelectedValue is int) clienteId = (int)cboCliente.SelectedValue;

                DateTime? de = chkDe.Checked ? (DateTime?)dtDe.Value.Date : null;
                DateTime? ate = chkAte.Checked ? (DateTime?)dtAte.Value.Date.AddDays(1).AddSeconds(-1) : null;

                _ultimoResultado = _service.GerarLinhas(clienteId, status, de, ate);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("LinhasOS", _ultimoResultado));
                reportViewer.RefreshReport();

                lblResumo.Text = string.Format("OS: {0}  |  Total geral: {1:N2}",
                    _ultimoResultado.Count,
                    SomaTotal(_ultimoResultado));
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Gerar relatorio");
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ultimoResultado == null || _ultimoResultado.Count == 0)
                {
                    Mensagens.Aviso("Gere o relatorio antes de exportar.");
                    return;
                }

                using (var dlg = new SaveFileDialog())
                {
                    dlg.FileName = "Relatorio_OS_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";
                    dlg.Filter = "PDF (*.pdf)|*.pdf";
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType, encoding, fileExt;
                    byte[] bytes = reportViewer.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out fileExt, out streamIds, out warnings);

                    File.WriteAllBytes(dlg.FileName, bytes);
                    Mensagens.Info("Relatorio exportado com sucesso:" + Environment.NewLine + dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Exportar PDF");
            }
        }

        private static decimal SomaTotal(IList<LinhaRelatorioOS> linhas)
        {
            decimal s = 0m;
            foreach (var l in linhas) s += l.ValorTotal;
            return s;
        }
    }
}
