using System;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.UI.Forms.Clientes;
using DeMariaTeste.UI.Forms.OrdensServico;
using DeMariaTeste.UI.Forms.Relatorios;
using DeMariaTeste.UI.Forms.Servicos;

namespace DeMariaTeste.UI.Forms
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            this.Icon = DeMariaTeste.UI.Forms.Common.IconeApp.LogoIcon;
            this.IsMdiContainer = true;
            this.statusUsuario.Text = "Usuario: " + SessaoUsuario.ObterUsuarioOuPadrao();
        }

        private void miClientes_Click(object sender, EventArgs e)
        {
            AbrirComoFilha(new FormClientesLista());
        }

        private void miServicos_Click(object sender, EventArgs e)
        {
            AbrirComoFilha(new FormServicosLista());
        }

        private void miOS_Click(object sender, EventArgs e)
        {
            AbrirComoFilha(new FormOrdensLista());
        }

        private void miRelatorio_Click(object sender, EventArgs e)
        {
            AbrirComoFilha(new FormRelatorioOS());
        }

        private void miSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Garante uma instancia por tipo de form filha.
        private void AbrirComoFilha(Form filha)
        {
            foreach (var f in this.MdiChildren)
            {
                if (f.GetType() == filha.GetType())
                {
                    f.BringToFront();
                    filha.Dispose();
                    return;
                }
            }

            filha.MdiParent = this;
            filha.WindowState = FormWindowState.Maximized;
            filha.Show();
        }
    }
}
