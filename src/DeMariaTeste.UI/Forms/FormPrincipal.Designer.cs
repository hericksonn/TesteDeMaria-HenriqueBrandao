namespace DeMariaTeste.UI.Forms
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem miCadastros;
        private System.Windows.Forms.ToolStripMenuItem miClientes;
        private System.Windows.Forms.ToolStripMenuItem miServicos;
        private System.Windows.Forms.ToolStripMenuItem miOS;
        private System.Windows.Forms.ToolStripMenuItem miRelatorios;
        private System.Windows.Forms.ToolStripMenuItem miRelatorio;
        private System.Windows.Forms.ToolStripMenuItem miArquivo;
        private System.Windows.Forms.ToolStripMenuItem miSair;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusUsuario;

        private void InitializeComponent()
        {
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.miArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSair = new System.Windows.Forms.ToolStripMenuItem();
            this.miCadastros = new System.Windows.Forms.ToolStripMenuItem();
            this.miClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.miServicos = new System.Windows.Forms.ToolStripMenuItem();
            this.miOS = new System.Windows.Forms.ToolStripMenuItem();
            this.miRelatorios = new System.Windows.Forms.ToolStripMenuItem();
            this.miRelatorio = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuPrincipal.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            //
            // menuPrincipal
            //
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.miArquivo, this.miCadastros, this.miOS, this.miRelatorios });
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1000, 24);
            //
            // miArquivo
            //
            this.miArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.miSair });
            this.miArquivo.Text = "&Arquivo";
            //
            // miSair
            //
            this.miSair.Text = "Sair";
            this.miSair.Click += new System.EventHandler(this.miSair_Click);
            //
            // miCadastros
            //
            this.miCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.miClientes, this.miServicos });
            this.miCadastros.Text = "&Cadastros";
            //
            // miClientes
            //
            this.miClientes.Text = "Clientes";
            this.miClientes.Click += new System.EventHandler(this.miClientes_Click);
            //
            // miServicos
            //
            this.miServicos.Text = "Servicos";
            this.miServicos.Click += new System.EventHandler(this.miServicos_Click);
            //
            // miOS
            //
            this.miOS.Text = "&Ordens de Servico";
            this.miOS.Click += new System.EventHandler(this.miOS_Click);
            //
            // miRelatorios
            //
            this.miRelatorios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.miRelatorio });
            this.miRelatorios.Text = "&Relatorios";
            //
            // miRelatorio
            //
            this.miRelatorio.Text = "Relatorio de OS";
            this.miRelatorio.Click += new System.EventHandler(this.miRelatorio_Click);
            //
            // statusBar
            //
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusUsuario });
            this.statusBar.Location = new System.Drawing.Point(0, 600);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1000, 22);
            //
            // statusUsuario
            //
            this.statusUsuario.Name = "statusUsuario";
            this.statusUsuario.Size = new System.Drawing.Size(80, 17);
            this.statusUsuario.Text = "Usuario:";
            //
            // FormPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 622);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "FormPrincipal";
            this.Text = "DeMaria - Gestao de OS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
