using System.IO;
using System.Linq;
using System.Windows.Forms;
using WebAutoLogin.Client;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Manager
{
    public partial class frmUsbSetup : frmBaseForm
    {
        public frmUsbSetup()
        {
            InitializeComponent();
        }

        public Account Account { get; set; }

        private void frmUsbSetup_Load(object sender, System.EventArgs e)
        {
            FillDriveList();

            lblFullName.Text = string.Format("Full Name: {0}", Account.FullName);
            lblUsername.Text = string.Format("Username: {0}", Account.UserName);
            lblToken.Text = string.Format("Token: \r\n{0}", Account.Token);
        }

        private void FillDriveList()
        {
            var driveList = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable);
            foreach (var driveInfo in driveList)
            {
                cbxDiskDrive.Items.Add(string.Format("{0} [{1}]", driveInfo.Name, driveInfo.VolumeLabel));
            }
            if (cbxDiskDrive.Items.Count > 0)
            {
                cbxDiskDrive.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (cbxDiskDrive.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a removable drive first!", Text);
                return;
            }
            var s = cbxDiskDrive.SelectedItem.ToString();
            var drive = s.Substring(0, 1).Trim();

            var di = new DriveInfo(drive);
            if (!di.IsReady)
            {
                MessageBox.Show("Drive is not ready, please try again after few seconds.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fi = new FileInfo(Path.Combine(di.Name, GlobalModule.TokenFileName));
            if (fi.Exists)
            {
                fi.Attributes = FileAttributes.Normal;
                fi.Delete();
            }
            using (var sw = fi.CreateText())
            {
                sw.Write(Account.Token);
            }
            fi.Attributes = FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System;

            MessageBox.Show("Token file successfully created.", Text);

            DialogResult = DialogResult.OK;
        }
    }
}
