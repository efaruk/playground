using System;
using System.Windows.Forms;
using WebAutoLogin.Client;

namespace WebAutoLogin.Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize Dependency Container
            DependencyContainer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmAccountList());
        }
    }
}
