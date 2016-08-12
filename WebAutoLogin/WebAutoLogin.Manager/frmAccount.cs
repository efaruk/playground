using System.Windows.Forms;
using WebAutoLogin.Client;
using WebAutoLogin.Data.Entities;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Manager
{
    public partial class frmAccount : frmBaseForm
    {
        private Account _account;
        private bool _isDirty = false;
        private IHashService _hashService;
        private IApiHelper _apiHelper;

        public frmAccount()
        {
            InitializeComponent();
        }

        public Account Account
        {
            get { return _account; }
            set
            {
                _account = value;
                ToggleSetupButton();
            }
        }

        private void ToggleSetupButton()
        {
            if (Account.Id > 0 && !_isDirty)
            {
                btnSetupUsbStick.Enabled = true;
            }
            else
            {
                btnSetupUsbStick.Enabled = false;
            }
        }

        private bool _binding = false;
        private void frmAccount_Load(object sender, System.EventArgs e)
        {
            _hashService = DependencyContainer.Resolve<IHashService>();
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            _binding = true;
            if (Account.Id > 0)
            {
                tbUsername.ReadOnly = true;
            }
            bsAccount.DataSource = Account;
            _binding = false;
        }

        private void btnSetupUsbStick_Click(object sender, System.EventArgs e)
        {

        }

        private void btnSetPassword_Click(object sender, System.EventArgs e)
        {
            var passwordForm = new frmPassword();
            var result = passwordForm.ShowDialog(this);
            if (result != DialogResult.OK) return;

            Account.Password = passwordForm.Password;
            UpdateToken();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var resultAccount = _apiHelper.InsertAccount(Account);
            if (resultAccount == null) return;

            Account = resultAccount;
            _isDirty = false;
        }

        private void btnSaveAndClose_Click(object sender, System.EventArgs e)
        {
            btnSave_Click(sender, e);
            if (!_isDirty)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        private void bsAccount_CurrentItemChanged(object sender, System.EventArgs e)
        {
            if (_binding) return;
            _isDirty = true;
        }

        private void UpdateToken()
        {
            _isDirty = true;

            Account.Token =
                _hashService.Hash(string.Format(GlobalModule.TokenHashFormat, tbUsername.Text.Trim(),
                    tbPassword.Text.Trim()));
        }
    }
}
