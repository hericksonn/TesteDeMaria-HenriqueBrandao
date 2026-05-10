using System.Windows.Forms;

namespace DeMariaTeste.UI.Forms.Common
{
    internal static class Mensagens
    {
        public static void Info(string texto, string titulo = "Informacao")
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Aviso(string texto, string titulo = "Atencao")
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Erro(string texto, string titulo = "Erro")
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string pergunta, string titulo = "Confirmacao")
        {
            return MessageBox.Show(pergunta, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
        }
    }
}
