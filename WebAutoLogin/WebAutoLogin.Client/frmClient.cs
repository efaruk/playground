using System;
using System.Net;
using System.Windows.Forms;
using WebAutoLogin.Configuration;

namespace WebAutoLogin.Client
{
    public partial class frmClient : Form
    {
        private TokenDetector _tokenDetector;
        private IApiHelper _apiHelper;
        private AutoLoginSettings _autoLoginSettings;

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            
            btnTestConnection_Click(sender, e);

            _tokenDetector = new TokenDetector();
            _tokenDetector.OnTokenChange += TokenDetectorOnTokenChange;
            _tokenDetector.Start();
        }

        private void TokenDetectorOnTokenChange(string token)
        {
            throw new NotImplementedException();
        }

        private void tsmiTest_Click(object sender, EventArgs e)
        {
            btnTestConnection_Click(sender, e);
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (_apiHelper.HealthCheck())
            {
                niMain.ShowBalloonTip(GlobalModule.NotificationTimeout, Text, "Server is working...", ToolTipIcon.Info);
            }
            else
            {
                niMain.ShowBalloonTip(GlobalModule.NotificationTimeout, Text, "Can not connect to server", ToolTipIcon.Error);
            }
        }

        private void tsmiToggle_Click(object sender, EventArgs e)
        {
            _tokenDetector.Toggle();
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenDetector.Stop();
        }
    }
}