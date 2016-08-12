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
        private frmLogin _loginForm;
        private List<Account> _accounts;

        public frmAccountList()
        {
            InitializeComponent();
        }

        private void frmAccountList_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
            Visible = false;
            _loginForm = new frmLogin();
            var result = _loginForm.ShowDialog(this);
            if (result != DialogResult.OK)
            {
                Close();
            }
            else
            {
                Visible = true;
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
            var accountForm = new frmAccount { Account = new Account() };
            var result = accountForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                LoadList();
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgAccounts.SelectedRows.Count == 0) return;

            var row = dgAccounts.SelectedRows[0];
            var id = Convert.ToInt32(row.Cells["idColumn"].Value);
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            if (account == null) return;

            var accountForm = new frmAccount { Account = account };
            var result = accountForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                LoadList();
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not supported.", Text);
        }
    }
}
