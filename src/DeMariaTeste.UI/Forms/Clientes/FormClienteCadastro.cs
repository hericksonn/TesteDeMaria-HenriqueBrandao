using System;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms.Clientes
{
    public partial class FormClienteCadastro : Form
    {
        private readonly ClienteService _service;
        private Cliente _cliente;

        public FormClienteCadastro() : this(0) { }

        public FormClienteCadastro(int idEdicao)
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            _service = ServiceLocator.CriarClienteService();

            if (idEdicao > 0)
            {
                _cliente = _service.ObterPorId(idEdicao);
                if (_cliente == null)
                {
                    Mensagens.Aviso("Cliente nao encontrado.");
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                this.Text = "Editar cliente";
            }
            else
            {
                _cliente = new Cliente();
                this.Text = "Novo cliente";
            }

            cboTipo.Items.AddRange(new object[] { "Fisica", "Juridica" });
            CarregarTela();
        }

        private void CarregarTela()
        {
            txtId.Text = _cliente.Id == 0 ? "(novo)" : _cliente.Id.ToString();
            txtNome.Text = _cliente.Nome;
            txtDocumento.Text = _cliente.Documento;
            cboTipo.SelectedIndex = (int)_cliente.Tipo;
            txtEmail.Text = _cliente.Email;
            txtTelefone.Text = _cliente.Telefone;
            chkAtivo.Checked = _cliente.Ativo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _cliente.Nome = txtNome.Text.Trim();
                _cliente.Documento = txtDocumento.Text.Trim();
                _cliente.Tipo = cboTipo.SelectedIndex == 1 ? TipoCliente.Juridica : TipoCliente.Fisica;
                _cliente.Email = txtEmail.Text.Trim();
                _cliente.Telefone = txtTelefone.Text.Trim();
                _cliente.Ativo = chkAtivo.Checked;

                _service.Salvar(_cliente);
                Mensagens.Info("Cliente salvo com sucesso.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Salvar cliente");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
