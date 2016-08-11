using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebAutoLogin.Client
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
