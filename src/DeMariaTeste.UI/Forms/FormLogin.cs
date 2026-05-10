using System;
using System.Windows.Forms;
using DeMariaTeste.Application.Services;
using DeMariaTeste.UI.Forms.Common;

namespace DeMariaTeste.UI.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.Icon = IconeApp.LogoIcon;
            picLogo.Image = IconeApp.LogoImage;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Sem autenticacao real; o nome digitado vai para a auditoria.
            string usuario = txtUsuario.Text.Trim();
            if (string.IsNullOrWhiteSpace(usuario))
            {
                Mensagens.Aviso("Informe um nome de usuario.");
                txtUsuario.Focus();
                return;
            }

            SessaoUsuario.UsuarioLogado = usuario;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
