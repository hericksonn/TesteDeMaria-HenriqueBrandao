using System;
using System.Linq;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms.OrdensServico
{
    public partial class FormOrdensLista : Form
    {
        private readonly OrdemServicoService _service;
        private readonly ClienteService _serviceCliente;
        private int _paginaAtual = 1;
        private const int TamanhoPagina = 20;

        public FormOrdensLista()
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            RemoverLogosDecorativas();
            _service = ServiceLocator.CriarOrdemServicoService();
            _serviceCliente = ServiceLocator.CriarClienteService();

            cboStatus.Items.Add("Todos");
            foreach (var st in Enum.GetValues(typeof(StatusOrdemServico)))
                cboStatus.Items.Add(st.ToString());
            cboStatus.SelectedIndex = 0;

            this.Load += (s, e) => Pesquisar();
        }

        private void RemoverLogosDecorativas()
        {
            var logos = this.Controls.OfType<PictureBox>().ToList();
            foreach (var logo in logos)
            {
                this.Controls.Remove(logo);
                logo.Dispose();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            _paginaAtual = 1;
            Pesquisar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var f = new FormOrdemServicoCadastro())
                if (f.ShowDialog() == DialogResult.OK) Pesquisar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var os = ObterSelecionado();
            if (os == null) return;
            using (var f = new FormOrdemServicoCadastro(os.Id))
                if (f.ShowDialog() == DialogResult.OK) Pesquisar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var os = ObterSelecionado();
            if (os == null) return;
            if (!Mensagens.Confirmar("Cancelar a OS #" + os.Id + "?")) return;

            string motivo;
            using (var dlg = new InputDialog("Informe o motivo do cancelamento:", "Cancelar OS"))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                motivo = dlg.Valor;
            }

            try
            {
                _service.Cancelar(os.Id, motivo);
                Pesquisar();
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Cancelar OS");
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaAtual <= 1) return;
            _paginaAtual--;
            Pesquisar();
        }

        private void btnProxima_Click(object sender, EventArgs e)
        {
            _paginaAtual++;
            Pesquisar();
        }

        private void Pesquisar()
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

                DateTime? de = chkDe.Checked ? (DateTime?)dtDe.Value.Date : null;
                DateTime? ate = chkAte.Checked ? (DateTime?)dtAte.Value.Date.AddDays(1).AddSeconds(-1) : null;

                var resultado = _service.Listar(null, status, de, ate, _paginaAtual, TamanhoPagina);
                grid.AutoGenerateColumns = false;
                grid.DataSource = resultado.Itens;
                lblPaginacao.Text = string.Format("Pagina {0} de {1} ({2} registros)",
                    resultado.Pagina,
                    resultado.TotalPaginas == 0 ? 1 : resultado.TotalPaginas,
                    resultado.TotalRegistros);
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Listar OS");
            }
        }

        private OrdemServico ObterSelecionado()
        {
            if (grid.CurrentRow == null) { Mensagens.Aviso("Selecione uma OS."); return null; }
            return grid.CurrentRow.DataBoundItem as OrdemServico;
        }
    }
}
