using System;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms.Clientes
{
    public partial class FormClientesLista : Form
    {
        private readonly ClienteService _service;
        private int _paginaAtual = 1;
        private const int TamanhoPagina = 20;

        public FormClientesLista()
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            _service = ServiceLocator.CriarClienteService();
            this.Load += (s, e) => Pesquisar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            _paginaAtual = 1;
            Pesquisar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (var f = new FormClienteCadastro())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var cli = ObterSelecionado();
            if (cli == null) return;
            using (var f = new FormClienteCadastro(cli.Id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    Pesquisar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var cli = ObterSelecionado();
            if (cli == null) return;
            if (!Mensagens.Confirmar("Confirma a exclusao do cliente '" + cli.Nome + "'?")) return;
            try
            {
                _service.Excluir(cli.Id);
                Pesquisar();
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Excluir cliente");
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
                bool? ativo = null;
                if (cboAtivo.SelectedIndex == 1) ativo = true;
                else if (cboAtivo.SelectedIndex == 2) ativo = false;

                var resultado = _service.Listar(
                    txtNome.Text, txtDocumento.Text, ativo, _paginaAtual, TamanhoPagina);

                grid.AutoGenerateColumns = false;
                grid.DataSource = resultado.Itens;
                lblPaginacao.Text = string.Format("Pagina {0} de {1} ({2} registros)",
                    resultado.Pagina,
                    resultado.TotalPaginas == 0 ? 1 : resultado.TotalPaginas,
                    resultado.TotalRegistros);
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Listar clientes");
            }
        }

        private Cliente ObterSelecionado()
        {
            if (grid.CurrentRow == null) { Mensagens.Aviso("Selecione um cliente."); return null; }
            return grid.CurrentRow.DataBoundItem as Cliente;
        }
    }
}
