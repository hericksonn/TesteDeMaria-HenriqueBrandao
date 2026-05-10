namespace DeMariaTeste.UI.Forms.OrdensServico
{
    partial class FormOrdemServicoCadastro
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.TextBox txtVersao;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblAbertura;
        private System.Windows.Forms.DateTimePicker dtAbertura;
        private System.Windows.Forms.Label lblObs;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.GroupBox gbItens;
        private System.Windows.Forms.Label lblServico;
        private System.Windows.Forms.ComboBox cboServico;
        private System.Windows.Forms.Label lblQtd;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label lblVU;
        private System.Windows.Forms.TextBox txtValorUnit;
        private System.Windows.Forms.Button btnAdicionarItem;
        private System.Windows.Forms.Button btnRemoverItem;
        private System.Windows.Forms.DataGridView gridItens;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItServico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItQtd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItImp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblBloqueada;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelarForm;

        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblVersao = new System.Windows.Forms.Label();
            this.txtVersao = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblAbertura = new System.Windows.Forms.Label();
            this.dtAbertura = new System.Windows.Forms.DateTimePicker();
            this.lblObs = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.gbItens = new System.Windows.Forms.GroupBox();
            this.lblServico = new System.Windows.Forms.Label();
            this.cboServico = new System.Windows.Forms.ComboBox();
            this.lblQtd = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.lblVU = new System.Windows.Forms.Label();
            this.txtValorUnit = new System.Windows.Forms.TextBox();
            this.btnAdicionarItem = new System.Windows.Forms.Button();
            this.btnRemoverItem = new System.Windows.Forms.Button();
            this.gridItens = new System.Windows.Forms.DataGridView();
            this.colItServico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItQtd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItImp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblBloqueada = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelarForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).BeginInit();
            this.gbItens.SuspendLayout();
            this.SuspendLayout();

            this.lblId.Text = "OS:"; this.lblId.Location = new System.Drawing.Point(15, 18); this.lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(60, 15); this.txtId.Size = new System.Drawing.Size(80, 22); this.txtId.ReadOnly = true;

            this.lblVersao.Text = "Versao:"; this.lblVersao.Location = new System.Drawing.Point(160, 18); this.lblVersao.AutoSize = true;
            this.txtVersao.Location = new System.Drawing.Point(220, 15); this.txtVersao.Size = new System.Drawing.Size(60, 22); this.txtVersao.ReadOnly = true;

            this.lblCliente.Text = "Cliente:"; this.lblCliente.Location = new System.Drawing.Point(15, 50); this.lblCliente.AutoSize = true;
            this.cboCliente.Location = new System.Drawing.Point(80, 47); this.cboCliente.Size = new System.Drawing.Size(380, 22);
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblStatus.Text = "Status:"; this.lblStatus.Location = new System.Drawing.Point(480, 50); this.lblStatus.AutoSize = true;
            this.cboStatus.Location = new System.Drawing.Point(540, 47); this.cboStatus.Size = new System.Drawing.Size(150, 22);
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblAbertura.Text = "Abertura:"; this.lblAbertura.Location = new System.Drawing.Point(710, 50); this.lblAbertura.AutoSize = true;
            this.dtAbertura.Location = new System.Drawing.Point(780, 47); this.dtAbertura.Size = new System.Drawing.Size(180, 22);
            this.dtAbertura.Format = System.Windows.Forms.DateTimePickerFormat.Custom; this.dtAbertura.CustomFormat = "dd/MM/yyyy HH:mm";

            this.lblObs.Text = "Observacao:"; this.lblObs.Location = new System.Drawing.Point(15, 80); this.lblObs.AutoSize = true;
            this.txtObservacao.Location = new System.Drawing.Point(15, 100); this.txtObservacao.Size = new System.Drawing.Size(945, 60);
            this.txtObservacao.Multiline = true;

            this.gbItens.Text = "Itens"; this.gbItens.Location = new System.Drawing.Point(12, 175); this.gbItens.Size = new System.Drawing.Size(950, 360);
            this.gbItens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;

            this.lblServico.Text = "Servico:"; this.lblServico.Location = new System.Drawing.Point(15, 25); this.lblServico.AutoSize = true;
            this.cboServico.Location = new System.Drawing.Point(80, 22); this.cboServico.Size = new System.Drawing.Size(360, 22);
            this.cboServico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServico.SelectedIndexChanged += new System.EventHandler(this.cboServico_SelectedIndexChanged);

            this.lblQtd.Text = "Qtd:"; this.lblQtd.Location = new System.Drawing.Point(460, 25); this.lblQtd.AutoSize = true;
            this.txtQuantidade.Location = new System.Drawing.Point(495, 22); this.txtQuantidade.Size = new System.Drawing.Size(80, 22); this.txtQuantidade.Text = "1";

            this.lblVU.Text = "Vlr Unit:"; this.lblVU.Location = new System.Drawing.Point(595, 25); this.lblVU.AutoSize = true;
            this.txtValorUnit.Location = new System.Drawing.Point(655, 22); this.txtValorUnit.Size = new System.Drawing.Size(120, 22);

            this.btnAdicionarItem.Text = "Adicionar"; this.btnAdicionarItem.Location = new System.Drawing.Point(790, 20); this.btnAdicionarItem.Size = new System.Drawing.Size(100, 26);
            this.btnAdicionarItem.Click += new System.EventHandler(this.btnAdicionarItem_Click);

            this.gridItens.AllowUserToAddRows = false;
            this.gridItens.Location = new System.Drawing.Point(15, 60); this.gridItens.Size = new System.Drawing.Size(920, 240);
            this.gridItens.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.gridItens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItServico, this.colItQtd, this.colItVu, this.colItImp, this.colItTotal });
            this.gridItens.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridItens_CellEndEdit);

            this.colItServico.HeaderText = "Servico"; this.colItServico.DataPropertyName = "ServicoNome"; this.colItServico.Width = 360; this.colItServico.ReadOnly = true;
            this.colItQtd.HeaderText = "Qtd"; this.colItQtd.DataPropertyName = "Quantidade"; this.colItQtd.Width = 100; this.colItQtd.DefaultCellStyle.Format = "N3";
            this.colItVu.HeaderText = "Vlr Unit"; this.colItVu.DataPropertyName = "ValorUnitario"; this.colItVu.Width = 120; this.colItVu.DefaultCellStyle.Format = "N2";
            this.colItImp.HeaderText = "% Imposto"; this.colItImp.DataPropertyName = "PercentualImpostoAplicado"; this.colItImp.Width = 110; this.colItImp.DefaultCellStyle.Format = "N2"; this.colItImp.ReadOnly = true;
            this.colItTotal.HeaderText = "Total"; this.colItTotal.DataPropertyName = "ValorTotalItem"; this.colItTotal.Width = 130; this.colItTotal.DefaultCellStyle.Format = "N2"; this.colItTotal.ReadOnly = true;

            this.btnRemoverItem.Text = "Remover item"; this.btnRemoverItem.Location = new System.Drawing.Point(15, 310); this.btnRemoverItem.Size = new System.Drawing.Size(120, 26);
            this.btnRemoverItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);

            this.gbItens.Controls.Add(this.lblServico); this.gbItens.Controls.Add(this.cboServico);
            this.gbItens.Controls.Add(this.lblQtd); this.gbItens.Controls.Add(this.txtQuantidade);
            this.gbItens.Controls.Add(this.lblVU); this.gbItens.Controls.Add(this.txtValorUnit);
            this.gbItens.Controls.Add(this.btnAdicionarItem);
            this.gbItens.Controls.Add(this.gridItens);
            this.gbItens.Controls.Add(this.btnRemoverItem);

            this.lblTotal.Text = "Total da OS: 0,00";
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(15, 545);
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;

            this.lblBloqueada.Text = "OS finalizada / cancelada (somente leitura)";
            this.lblBloqueada.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBloqueada.AutoSize = true;
            this.lblBloqueada.Visible = false;
            this.lblBloqueada.Location = new System.Drawing.Point(300, 547);
            this.lblBloqueada.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;

            this.btnSalvar.Text = "Salvar"; this.btnSalvar.Location = new System.Drawing.Point(740, 545); this.btnSalvar.Size = new System.Drawing.Size(105, 30);
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            this.btnCancelarForm.Text = "Fechar"; this.btnCancelarForm.Location = new System.Drawing.Point(855, 545); this.btnCancelarForm.Size = new System.Drawing.Size(105, 30);
            this.btnCancelarForm.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelarForm.Click += new System.EventHandler(this.btnCancelarForm_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 590);
            this.Controls.Add(this.lblId); this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblVersao); this.Controls.Add(this.txtVersao);
            this.Controls.Add(this.lblCliente); this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblStatus); this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.lblAbertura); this.Controls.Add(this.dtAbertura);
            this.Controls.Add(this.lblObs); this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.gbItens);
            this.Controls.Add(this.lblTotal); this.Controls.Add(this.lblBloqueada);
            this.Controls.Add(this.btnSalvar); this.Controls.Add(this.btnCancelarForm);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FormOrdemServicoCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OS";
            ((System.ComponentModel.ISupportInitialize)(this.gridItens)).EndInit();
            this.gbItens.ResumeLayout(false);
            this.gbItens.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
