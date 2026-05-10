using System;
using System.Windows.Forms;

namespace DeMariaTeste.UI.Forms.Common
{
    // Dialog para pedir um texto sem usar Microsoft.VisualBasic.Interaction.
    internal class InputDialog : Form
    {
        private Label _lbl;
        private TextBox _txt;
        private Button _ok;
        private Button _cancel;

        public string Valor { get { return _txt.Text; } }

        public InputDialog(string mensagem, string titulo)
        {
            this.Text = titulo;
            this.Icon = IconeApp.LogoIcon;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(420, 130);

            _lbl = new Label { Text = mensagem, Location = new System.Drawing.Point(12, 12), AutoSize = true };
            _txt = new TextBox { Location = new System.Drawing.Point(12, 40), Size = new System.Drawing.Size(396, 22) };
            _ok = new Button { Text = "OK", Location = new System.Drawing.Point(220, 80), Size = new System.Drawing.Size(90, 28), DialogResult = DialogResult.OK };
            _cancel = new Button { Text = "Cancelar", Location = new System.Drawing.Point(318, 80), Size = new System.Drawing.Size(90, 28), DialogResult = DialogResult.Cancel };

            this.Controls.Add(_lbl); this.Controls.Add(_txt); this.Controls.Add(_ok); this.Controls.Add(_cancel);
            this.AcceptButton = _ok;
            this.CancelButton = _cancel;
        }
    }
}
