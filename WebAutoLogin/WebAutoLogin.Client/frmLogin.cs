﻿using System.Windows.Forms;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Client
{
    public partial class frmLogin : frmBaseForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private IApiHelper _apiHelper;
        private IHashService _hashService;

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            _hashService = DependencyContainer.Resolve<IHashService>();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (epLogin.GetError(tbUsername).Length > 0 || epLogin.GetError(tbPassword).Length > 0)
            {
                MessageBox.Show("Invalid username or password.", "Login");
                return;
            }
            var userName = tbUsername.Text.Trim();
            var password = tbPassword.Text.Trim();
            var token = _hashService.Hash(string.Format("{0}|{1}", userName, password));
            var account = _apiHelper.GetAccountByToken(token);
            if (account != null && account.Id > 0)
            {
                GlobalModule.Account = account;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login");
            }
        }

        private void tbUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            epLogin.SetError(tbUsername, tbUsername.Text.Trim().Length <= 3 ? "You should have a User Name..." : "");
        }

        private void tbPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            epLogin.SetError(tbPassword, tbPassword.Text.Trim().Length <= 3 ? "You should have a Password..." : "");
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
