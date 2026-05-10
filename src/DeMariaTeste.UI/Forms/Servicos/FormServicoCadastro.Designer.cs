namespace DeMariaTeste.UI.Forms.Servicos
{
    partial class FormServicoCadastro
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblImposto;
        private System.Windows.Forms.TextBox txtImposto;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblImposto = new System.Windows.Forms.Label();
            this.txtImposto = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblId.Text = "Id:"; this.lblId.Location = new System.Drawing.Point(15, 18); this.lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(140, 15); this.txtId.Size = new System.Drawing.Size(100, 22); this.txtId.ReadOnly = true;

            this.lblNome.Text = "Nome:"; this.lblNome.Location = new System.Drawing.Point(15, 50); this.lblNome.AutoSize = true;
            this.txtNome.Location = new System.Drawing.Point(140, 47); this.txtNome.Size = new System.Drawing.Size(380, 22);

            this.lblValor.Text = "Valor base:"; this.lblValor.Location = new System.Drawing.Point(15, 82); this.lblValor.AutoSize = true;
            this.txtValor.Location = new System.Drawing.Point(140, 79); this.txtValor.Size = new System.Drawing.Size(150, 22);

            this.lblImposto.Text = "% Imposto:"; this.lblImposto.Location = new System.Drawing.Point(15, 114); this.lblImposto.AutoSize = true;
            this.txtImposto.Location = new System.Drawing.Point(140, 111); this.txtImposto.Size = new System.Drawing.Size(150, 22);

            this.chkAtivo.Text = "Ativo"; this.chkAtivo.Location = new System.Drawing.Point(140, 145); this.chkAtivo.AutoSize = true; this.chkAtivo.Checked = true;

            this.btnSalvar.Text = "Salvar"; this.btnSalvar.Location = new System.Drawing.Point(310, 180); this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            this.btnCancelar.Text = "Cancelar"; this.btnCancelar.Location = new System.Drawing.Point(420, 180); this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            this.AcceptButton = this.btnSalvar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 230);
            this.Controls.Add(this.lblId); this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblNome); this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblValor); this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblImposto); this.Controls.Add(this.txtImposto);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnSalvar); this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "FormServicoCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Servico";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
