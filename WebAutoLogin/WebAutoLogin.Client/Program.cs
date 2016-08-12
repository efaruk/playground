using System;
using System.Windows.Forms;

namespace WebAutoLogin.Client
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Initialize Dependency Container
            DependencyContainer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmClient());
        }
    }
}