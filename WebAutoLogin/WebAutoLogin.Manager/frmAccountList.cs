using System;
using WebAutoLogin.Client;

namespace WebAutoLogin.Manager
{
    public partial class frmAccountList : frmBaseForm
    {
        private IApiHelper _apiHelper;

        public frmAccountList()
        {
            InitializeComponent();
        }

        private void frmAccountList_Load(object sender, EventArgs e)
        {
            _apiHelper = DependencyContainer.Resolve<IApiHelper>();
        }

        private void LoadList()
        {
            _apiHelper.GetAccounts();
        }
    }
}
