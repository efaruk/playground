using System;
using System.Net;
using System.Windows.Forms;

namespace WebAutoLogin.Client
{
    public partial class frmClient : Form
    {
        private bool _closing = false;
        private bool _loggedIn = false;

        private DriveDetector _driveDetector;
        //private frmLogin _frmLogin;
        private IApiHelper _apiHelper;

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            //_frmLogin = new frmLogin();
            _driveDetector = new DriveDetector();
            _driveDetector.Start();
        }

        private void tsmiTest_Click(object sender, EventArgs e)
        {
            btnTestConnection_Click(sender, e);
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            var healthy = _apiHelper.HealthCheck();
            if (healthy)
            {
                niMain.ShowBalloonTip(GlobalModule.NotificationTimeout, Text, "Server is working...", ToolTipIcon.Info);
            }
            else
            {
                niMain.ShowBalloonTip(GlobalModule.NotificationTimeout, Text, "Can not connect to server", ToolTipIcon.Error);
            }
        }
    }
}