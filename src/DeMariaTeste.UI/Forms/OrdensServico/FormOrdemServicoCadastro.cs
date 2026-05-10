using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.Domain.Entities;
using DeMariaTeste.Domain.Enums;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms.OrdensServico
{
    public partial class FormOrdemServicoCadastro : Form
    {
        private readonly OrdemServicoService _service;
        private readonly ClienteService _serviceCliente;
        private readonly ServicoService _serviceServico;

        private OrdemServico _os;
        private BindingList<ItemOrdemServico> _itens;

        public FormOrdemServicoCadastro() : this(0) { }

        public FormOrdemServicoCadastro(int idEdicao)
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            _service = ServiceLocator.CriarOrdemServicoService();
            _serviceCliente = ServiceLocator.CriarClienteService();
            _serviceServico = ServiceLocator.CriarServicoService();

            CarregarCombos();

            if (idEdicao > 0)
            {
                _os = _service.ObterPorId(idEdicao);
                if (_os == null)
                {
                    Mensagens.Aviso("OS nao encontrada.");
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                this.Text = "Editar OS #" + _os.Id;
            }
            else
            {
                _os = new OrdemServico();
                this.Text = "Nova OS";
            }

            CarregarTela();
        }

        private void CarregarCombos()
        {
            var clientes = _serviceCliente.Listar(null, null, true, 1, 1000).Itens;
            cboCliente.DataSource = clientes;
            cboCliente.DisplayMember = "Nome";
            cboCliente.ValueMember = "Id";
            cboCliente.SelectedIndex = -1;

            cboStatus.Items.Clear();
            foreach (var st in Enum.GetValues(typeof(StatusOrdemServico)))
                cboStatus.Items.Add(st.ToString());

            var servicos = _serviceServico.ListarAtivos();
            cboServico.DataSource = servicos;
            cboServico.DisplayMember = "Nome";
            cboServico.ValueMember = "Id";
            cboServico.SelectedIndex = -1;
        }

        private void CarregarTela()
        {
            txtId.Text = _os.Id == 0 ? "(nova)" : _os.Id.ToString();
            txtVersao.Text = _os.Versao.ToString();

            if (_os.ClienteId > 0)
                cboCliente.SelectedValue = _os.ClienteId;

            cboStatus.SelectedItem = _os.Status.ToString();
            dtAbertura.Value = _os.DataAbertura == DateTime.MinValue ? DateTime.Now : _os.DataAbertura;
            txtObservacao.Text = _os.Observacao;

            _itens = new BindingList<ItemOrdemServico>(new List<ItemOrdemServico>(_os.Itens ?? new List<ItemOrdemServico>()));
            gridItens.AutoGenerateColumns = false;
            gridItens.DataSource = _itens;

            AtualizarTotal();

            // OS finalizada/cancelada fica read-only na tela (a service
            // tambem rejeita salvar nesses status).
            if (!_os.PodeEditar())
            {
                cboCliente.Enabled = false;
                cboStatus.Enabled = false;
                dtAbertura.Enabled = false;
                txtObservacao.ReadOnly = true;
                cboServico.Enabled = false;
                txtQuantidade.ReadOnly = true;
                txtValorUnit.ReadOnly = true;
                btnAdicionarItem.Enabled = false;
                btnRemoverItem.Enabled = false;
                btnSalvar.Enabled = false;
                gridItens.ReadOnly = true;
                lblBloqueada.Visible = true;
            }
        }

        private void AtualizarTotal()
        {
            decimal total = 0m;
            foreach (var i in _itens)
            {
                i.RecalcularTotal();
                total += i.ValorTotalItem;
            }
            lblTotal.Text = "Total da OS: " + total.ToString("N2", CultureInfo.CurrentCulture);
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            try
            {
                var servSelecionado = cboServico.SelectedItem as Servico;
                if (servSelecionado == null) { Mensagens.Aviso("Selecione um servico."); return; }

                decimal qtd, vu;
                if (!decimal.TryParse(txtQuantidade.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out qtd) || qtd <= 0)
                {
                    Mensagens.Aviso("Quantidade invalida.");
                    return;
                }
                if (!decimal.TryParse(txtValorUnit.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out vu) || vu <= 0)
                {
                    Mensagens.Aviso("Valor unitario invalido.");
                    return;
                }

                // O percentual e o valor unitario sao gravados a partir do
                // servico no momento da inclusao do item.
                var item = new ItemOrdemServico
                {
                    ServicoId = servSelecionado.Id,
                    ServicoNome = servSelecionado.Nome,
                    Quantidade = qtd,
                    ValorUnitario = vu,
                    PercentualImpostoAplicado = servSelecionado.PercentualImposto
                };
                item.RecalcularTotal();

                _itens.Add(item);
                AtualizarTotal();

                txtQuantidade.Text = "1";
                txtValorUnit.Text = servSelecionado.ValorBase.ToString("N2", CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Adicionar item");
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (gridItens.CurrentRow == null) return;
            var item = gridItens.CurrentRow.DataBoundItem as ItemOrdemServico;
            if (item == null) return;
            _itens.Remove(item);
            AtualizarTotal();
        }

        private void cboServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = cboServico.SelectedItem as Servico;
            if (s == null) return;
            txtValorUnit.Text = s.ValorBase.ToString("N2", CultureInfo.CurrentCulture);
        }

        private void gridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            AtualizarTotal();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCliente.SelectedValue == null)
                {
                    Mensagens.Aviso("Selecione um cliente.");
                    return;
                }

                _os.ClienteId = Convert.ToInt32(cboCliente.SelectedValue);
                StatusOrdemServico statusSelecionado;
                if (Enum.TryParse(cboStatus.SelectedItem != null ? cboStatus.SelectedItem.ToString() : "", out statusSelecionado))
                    _os.Status = statusSelecionado;

                if (_os.Status == StatusOrdemServico.Concluida)
                    _os.DataConclusao = DateTime.Now;
                else if (_os.Status != StatusOrdemServico.Cancelada)
                    _os.DataConclusao = null;

                _os.DataAbertura = dtAbertura.Value;
                _os.Observacao = txtObservacao.Text;
                _os.Itens = new List<ItemOrdemServico>(_itens);

                _service.Salvar(_os);
                Mensagens.Info("OS salva com sucesso.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                TratadorErro.Tratar(ex, "Salvar OS");
            }
        }

        private void btnCancelarForm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
