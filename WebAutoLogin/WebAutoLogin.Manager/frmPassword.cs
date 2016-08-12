using System;
using System.Windows.Forms;
using WebAutoLogin.Client;
using WebAutoLogin.Security.Cryptography;

namespace WebAutoLogin.Manager
{
    public partial class frmPassword : frmBaseForm
    {
        public frmPassword()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (epPassword.GetError(tbPassword1).Length > 0 || epPassword.GetError(tbPassword2).Length > 0)
            {
                MessageBox.Show("Invalid password length. It must be at least 3 letter.", "Set Password");
                return;
            }
            if (tbPassword1.Text != tbPassword2.Text)
            {
                MessageBox.Show("Paswords are not match.", "Set Password");
                return;
            }
            Password = tbPassword1.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        public string Password { get; set; }

        private void tbPassword1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            epPassword.SetError(tbPassword1, tbPassword1.Text.Trim().Length <= 3 ? "You should have a Password..." : "");
        }

        private void tbPassword2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            epPassword.SetError(tbPassword2, tbPassword1.Text.Trim().Length <= 3 ? "You should have a Password..." : "");
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
