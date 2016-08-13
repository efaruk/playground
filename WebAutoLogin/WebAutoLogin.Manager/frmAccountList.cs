using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebAutoLogin.Client;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Manager
{
    public partial class frmAccountList : frmBaseForm
    {
        private IApiHelper _apiHelper;
        private List<Account> _accounts;

        public frmAccountList()
        {
            InitializeComponent();
        }

        private void frmAccountList_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            Visible = false;
            using (var loginForm = new frmLogin())
            {
                var result = loginForm.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    Close();
                }
                else
                {
                    Visible = true;
                }
            }
            LoadList();
        }

        private void LoadList()
        {
            _accounts = _apiHelper.GetAccounts();
            bsAccounts.DataSource = _accounts;
        }

        private void tsbList_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            using (var accountForm = new frmAccount {Account = new Account()})
            {
                var result = accountForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadList();
                }
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            var account = SelectedAccount();
            if (account == null) return;

            using (var accountForm = new frmAccount { Account = account })
            {
                var result = accountForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadList();
                }
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Account will be deleted permanently. \r\n Are you sure?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            var account = SelectedAccount();
            if (account == null) return;

            var success = _apiHelper.Delete(account.Id);
            if (!success)
            {
                MessageBox.Show("Account could not be deleted.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadList();
        }

        private void tsbSetupUsbStick_Click(object sender, EventArgs e)
        {
            var account = SelectedAccount();
            if (account == null) return;

            using (var frmUsbSetup = new frmUsbSetup { Account = account })
            {
                frmUsbSetup.ShowDialog(this);
            }
        }

        private Account SelectedAccount()
        {
            if (dgAccounts.SelectedRows.Count == 0) return null;

            var row = dgAccounts.SelectedRows[0];
            var id = Convert.ToInt32(row.Cells["idColumn"].Value);
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            return account;
        }
    }
}
