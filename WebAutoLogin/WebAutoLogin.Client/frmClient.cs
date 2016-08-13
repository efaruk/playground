using System;
using System.Net;
using System.Windows.Forms;

namespace WebAutoLogin.Client
{
    public partial class frmClient : Form
    {
        private TokenDetector _tokenDetector;
        private IApiHelper _apiHelper;

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            
            btnTestConnection_Click(sender, e);

            _tokenDetector = new TokenDetector();
            _tokenDetector.OnTokenChange += TokenChangeDetectorOnTokenChange;
            _tokenDetector.Start();
        }

        private void TokenChangeDetectorOnTokenChange(string token)
        {
            throw new NotImplementedException();
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

        private void tsmiToggle_Click(object sender, EventArgs e)
        {
            _tokenDetector.Toggle();
        }
    }
}