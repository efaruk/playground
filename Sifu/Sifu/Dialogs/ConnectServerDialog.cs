using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StackExchange.Redis;

namespace Sifu.Dialogs
{
	public partial class ConnectServerDialog : Form
	{
		public ConnectServerDialog()
		{
			InitializeComponent();
		}

		private readonly SharedConnection _sharedConnection = SharedConnection.Instance; 

		private void DialogConnectServer_Load(object sender, EventArgs e)
		{

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_sharedConnection.Connect(tbAddress.Text);
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
	}
}
