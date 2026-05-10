namespace DeMariaTeste.UI.Forms.Clientes
{
    partial class FormClientesLista
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblAtivo;
        private System.Windows.Forms.ComboBox cboAtivo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblPaginacao;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProxima;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAtivo;

        private void InitializeComponent()
        {
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblAtivo = new System.Windows.Forms.Label();
            this.cboAtivo = new System.Windows.Forms.ComboBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.lblPaginacao = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnProxima = new System.Windows.Forms.Button();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();

            this.lblNome.Text = "Nome:";
            this.lblNome.Location = new System.Drawing.Point(12, 15);
            this.lblNome.AutoSize = true;
            this.txtNome.Location = new System.Drawing.Point(70, 12);
            this.txtNome.Size = new System.Drawing.Size(200, 22);

            this.lblDocumento.Text = "Documento:";
            this.lblDocumento.Location = new System.Drawing.Point(290, 15);
            this.lblDocumento.AutoSize = true;
            this.txtDocumento.Location = new System.Drawing.Point(370, 12);
            this.txtDocumento.Size = new System.Drawing.Size(180, 22);

            this.lblAtivo.Text = "Status:";
            this.lblAtivo.Location = new System.Drawing.Point(570, 15);
            this.lblAtivo.AutoSize = true;
            this.cboAtivo.Location = new System.Drawing.Point(620, 12);
            this.cboAtivo.Size = new System.Drawing.Size(120, 22);
            this.cboAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAtivo.Items.AddRange(new object[] { "Todos", "Ativos", "Inativos" });
            this.cboAtivo.SelectedIndex = 0;

            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Location = new System.Drawing.Point(760, 10);
            this.btnPesquisar.Size = new System.Drawing.Size(100, 28);
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            this.btnNovo.Text = "Novo";
            this.btnNovo.Location = new System.Drawing.Point(12, 50);
            this.btnNovo.Size = new System.Drawing.Size(100, 28);
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            this.btnEditar.Text = "Editar";
            this.btnEditar.Location = new System.Drawing.Point(120, 50);
            this.btnEditar.Size = new System.Drawing.Size(100, 28);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Location = new System.Drawing.Point(228, 50);
            this.btnExcluir.Size = new System.Drawing.Size(100, 28);
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);

            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ReadOnly = true;
            this.grid.MultiSelect = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Location = new System.Drawing.Point(12, 90);
            this.grid.Size = new System.Drawing.Size(960, 460);
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colNome, this.colDocumento, this.colTipo, this.colAtivo });

            this.colId.HeaderText = "Id";
            this.colId.DataPropertyName = "Id";
            this.colId.Width = 60;
            this.colNome.HeaderText = "Nome";
            this.colNome.DataPropertyName = "Nome";
            this.colNome.Width = 320;
            this.colDocumento.HeaderText = "Documento";
            this.colDocumento.DataPropertyName = "Documento";
            this.colDocumento.Width = 200;
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.DataPropertyName = "Tipo";
            this.colTipo.Width = 100;
            this.colAtivo.HeaderText = "Ativo";
            this.colAtivo.DataPropertyName = "Ativo";
            this.colAtivo.Width = 80;

            this.lblPaginacao.AutoSize = true;
            this.lblPaginacao.Location = new System.Drawing.Point(12, 560);
            this.lblPaginacao.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;

            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.Location = new System.Drawing.Point(770, 555);
            this.btnAnterior.Size = new System.Drawing.Size(100, 26);
            this.btnAnterior.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);

            this.btnProxima.Text = "Proxima >>";
            this.btnProxima.Location = new System.Drawing.Point(875, 555);
            this.btnProxima.Size = new System.Drawing.Size(100, 26);
            this.btnProxima.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnProxima.Click += new System.EventHandler(this.btnProxima_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 600);
            this.Controls.Add(this.lblNome); this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblDocumento); this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblAtivo); this.Controls.Add(this.cboAtivo);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.btnNovo); this.Controls.Add(this.btnEditar); this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.lblPaginacao);
            this.Controls.Add(this.btnAnterior); this.Controls.Add(this.btnProxima);
            this.Name = "FormClientesLista";
            this.Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
