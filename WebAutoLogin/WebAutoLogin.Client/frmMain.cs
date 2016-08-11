using System;
using System.Windows.Forms;

namespace WebAutoLogin.Client
{
    public partial class frmMain : frmBaseForm
    {
        private bool _closing = false;
        private bool _loggedIn = false;

        private DriveDetector _driveDetector;
        private frmLogin _frmLogin;
        private IApiHelper _apiHelper;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            _frmLogin = new frmLogin();
            _driveDetector = new DriveDetector();
            _driveDetector.Start();
        }

        private void tsmiTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello", Text);
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            var result = _frmLogin.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                MessageBox.Show(string.Format("Welcome {0}", GlobalModule.Account.FullName), "Login");
            }
        }
    }
}