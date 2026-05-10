using System;
using System.Globalization;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms.Servicos
{
    public partial class FormServicoCadastro : Form
    {
        private readonly ServicoService _service;
        private Servico _servico;

        public FormServicoCadastro() : this(0) { }

        public FormServicoCadastro(int idEdicao)
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            _service = ServiceLocator.CriarServicoService();

            if (idEdicao > 0)
            {
                _servico = _service.ObterPorId(idEdicao);
                if (_servico == null)
                {
                    Mensagens.Aviso("Servico nao encontrado.");
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                this.Text = "Editar servico";
            }
            else
            {
                _servico = new Servico();
                this.Text = "Novo servico";
            }

            CarregarTela();
        }

        private void CarregarTela()
        {
            txtId.Text = _servico.Id == 0 ? "(novo)" : _servico.Id.ToString();
            txtNome.Text = _servico.Nome;
            txtValor.Text = _servico.ValorBase.ToString("N2", CultureInfo.CurrentCulture);
            txtImposto.Text = _servico.PercentualImposto.ToString("N2", CultureInfo.CurrentCulture);
            chkAtivo.Checked = _servico.Ativo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal valor;
                decimal imposto;

                if (!decimal.TryParse(txtValor.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out valor))
                {
                    Mensagens.Aviso("Valor base invalido.");
                    return;
                }
                if (!decimal.TryParse(txtImposto.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out imposto))
                {
                    Mensagens.Aviso("Percentual de imposto invalido.");
                    return;
                }

                _servico.Nome = txtNome.Text.Trim();
                _servico.ValorBase = valor;
                _servico.PercentualImposto = imposto;
                _servico.Ativo = chkAtivo.Checked;

                _service.Salvar(_servico);
                Mensagens.Info("Servico salvo com sucesso.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Salvar servico");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
