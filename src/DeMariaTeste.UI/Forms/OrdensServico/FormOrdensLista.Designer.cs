namespace DeMariaTeste.UI.Forms.OrdensServico
{
    partial class FormOrdensLista
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.CheckBox chkDe;
        private System.Windows.Forms.DateTimePicker dtDe;
        private System.Windows.Forms.CheckBox chkAte;
        private System.Windows.Forms.DateTimePicker dtAte;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblPaginacao;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProxima;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConclusao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;

        private void InitializeComponent()
        {
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.chkDe = new System.Windows.Forms.CheckBox();
            this.dtDe = new System.Windows.Forms.DateTimePicker();
            this.chkAte = new System.Windows.Forms.CheckBox();
            this.dtAte = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.lblPaginacao = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnProxima = new System.Windows.Forms.Button();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConclusao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();

            this.lblStatus.Text = "Status:"; this.lblStatus.Location = new System.Drawing.Point(12, 15); this.lblStatus.AutoSize = true;
            this.cboStatus.Location = new System.Drawing.Point(70, 12); this.cboStatus.Size = new System.Drawing.Size(140, 22);
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.chkDe.Text = "De:"; this.chkDe.Location = new System.Drawing.Point(230, 14); this.chkDe.AutoSize = true;
            this.dtDe.Location = new System.Drawing.Point(280, 12); this.dtDe.Size = new System.Drawing.Size(140, 22); this.dtDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.chkAte.Text = "Ate:"; this.chkAte.Location = new System.Drawing.Point(440, 14); this.chkAte.AutoSize = true;
            this.dtAte.Location = new System.Drawing.Point(490, 12); this.dtAte.Size = new System.Drawing.Size(140, 22); this.dtAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.btnPesquisar.Text = "Pesquisar"; this.btnPesquisar.Location = new System.Drawing.Point(660, 10); this.btnPesquisar.Size = new System.Drawing.Size(110, 28);
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            this.btnNovo.Text = "Nova OS"; this.btnNovo.Location = new System.Drawing.Point(12, 50); this.btnNovo.Size = new System.Drawing.Size(110, 28);
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            this.btnEditar.Text = "Abrir / Editar"; this.btnEditar.Location = new System.Drawing.Point(130, 50); this.btnEditar.Size = new System.Drawing.Size(120, 28);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            this.btnCancelar.Text = "Cancelar OS"; this.btnCancelar.Location = new System.Drawing.Point(258, 50); this.btnCancelar.Size = new System.Drawing.Size(120, 28);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            this.grid.AllowUserToAddRows = false; this.grid.AllowUserToDeleteRows = false;
            this.grid.ReadOnly = true; this.grid.MultiSelect = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Location = new System.Drawing.Point(12, 90);
            this.grid.Size = new System.Drawing.Size(1000, 460);
            this.grid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colCliente, this.colAbertura, this.colConclusao, this.colStatus, this.colTotal });

            this.colId.HeaderText = "OS"; this.colId.DataPropertyName = "Id"; this.colId.Width = 70;
            this.colCliente.HeaderText = "Cliente"; this.colCliente.DataPropertyName = "ClienteNome"; this.colCliente.Width = 320;
            this.colAbertura.HeaderText = "Abertura"; this.colAbertura.DataPropertyName = "DataAbertura"; this.colAbertura.Width = 140;
            this.colAbertura.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.colConclusao.HeaderText = "Conclusao"; this.colConclusao.DataPropertyName = "DataConclusao"; this.colConclusao.Width = 140;
            this.colConclusao.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.colStatus.HeaderText = "Status"; this.colStatus.DataPropertyName = "Status"; this.colStatus.Width = 130;
            this.colTotal.HeaderText = "Total"; this.colTotal.DataPropertyName = "ValorTotal"; this.colTotal.Width = 130;
            this.colTotal.DefaultCellStyle.Format = "N2"; this.colTotal.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;

            this.lblPaginacao.AutoSize = true; this.lblPaginacao.Location = new System.Drawing.Point(12, 560);
            this.lblPaginacao.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;

            this.btnAnterior.Text = "<< Anterior"; this.btnAnterior.Location = new System.Drawing.Point(810, 555); this.btnAnterior.Size = new System.Drawing.Size(100, 26);
            this.btnAnterior.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            this.btnProxima.Text = "Proxima >>"; this.btnProxima.Location = new System.Drawing.Point(915, 555); this.btnProxima.Size = new System.Drawing.Size(100, 26);
            this.btnProxima.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnProxima.Click += new System.EventHandler(this.btnProxima_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.lblStatus); this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.chkDe); this.Controls.Add(this.dtDe);
            this.Controls.Add(this.chkAte); this.Controls.Add(this.dtAte);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.btnNovo); this.Controls.Add(this.btnEditar); this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.lblPaginacao); this.Controls.Add(this.btnAnterior); this.Controls.Add(this.btnProxima);
            this.Name = "FormOrdensLista";
            this.Text = "Ordens de Servico";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
