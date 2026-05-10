namespace DeMariaTeste.UI.Forms.Clientes
{
    partial class FormClienteCadastro
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
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblId.Text = "Id:";
            this.lblId.Location = new System.Drawing.Point(15, 18);
            this.lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(120, 15);
            this.txtId.Size = new System.Drawing.Size(100, 22);
            this.txtId.ReadOnly = true;

            this.lblNome.Text = "Nome:";
            this.lblNome.Location = new System.Drawing.Point(15, 50);
            this.lblNome.AutoSize = true;
            this.txtNome.Location = new System.Drawing.Point(120, 47);
            this.txtNome.Size = new System.Drawing.Size(380, 22);

            this.lblDocumento.Text = "Documento:";
            this.lblDocumento.Location = new System.Drawing.Point(15, 82);
            this.lblDocumento.AutoSize = true;
            this.txtDocumento.Location = new System.Drawing.Point(120, 79);
            this.txtDocumento.Size = new System.Drawing.Size(220, 22);

            this.lblTipo.Text = "Tipo:";
            this.lblTipo.Location = new System.Drawing.Point(15, 114);
            this.lblTipo.AutoSize = true;
            this.cboTipo.Location = new System.Drawing.Point(120, 111);
            this.cboTipo.Size = new System.Drawing.Size(150, 22);
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblEmail.Text = "E-mail:";
            this.lblEmail.Location = new System.Drawing.Point(15, 146);
            this.lblEmail.AutoSize = true;
            this.txtEmail.Location = new System.Drawing.Point(120, 143);
            this.txtEmail.Size = new System.Drawing.Size(380, 22);

            this.lblTelefone.Text = "Telefone:";
            this.lblTelefone.Location = new System.Drawing.Point(15, 178);
            this.lblTelefone.AutoSize = true;
            this.txtTelefone.Location = new System.Drawing.Point(120, 175);
            this.txtTelefone.Size = new System.Drawing.Size(200, 22);

            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.Location = new System.Drawing.Point(120, 207);
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;

            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Location = new System.Drawing.Point(280, 240);
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(390, 240);
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            this.AcceptButton = this.btnSalvar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 290);
            this.Controls.Add(this.lblId); this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblNome); this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblDocumento); this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblTipo); this.Controls.Add(this.cboTipo);
            this.Controls.Add(this.lblEmail); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelefone); this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.btnSalvar); this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.Name = "FormClienteCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
