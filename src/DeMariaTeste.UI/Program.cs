using System;
using System.Windows.Forms;
using DeMariaTeste.UI.Forms;
using DeMariaTeste.UI.Forms.Common;

// Alias para evitar conflito com o namespace DeMariaTeste.Application.
using WinFormsApp = System.Windows.Forms.Application;

namespace DeMariaTeste.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            WinFormsApp.ThreadException += (s, e) =>
                TratadorErro.Tratar(e.Exception, "Erro nao tratado na UI");
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                TratadorErro.Tratar(e.ExceptionObject as Exception, "Erro nao tratado no AppDomain");

            WinFormsApp.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            WinFormsApp.EnableVisualStyles();
            WinFormsApp.SetCompatibleTextRenderingDefault(false);

            using (var login = new FormLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            WinFormsApp.Run(new FormPrincipal());
        }
    }
}
