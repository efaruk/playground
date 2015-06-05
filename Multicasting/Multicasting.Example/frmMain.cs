using System;
using System.Windows.Forms;
using Multicasting;

namespace MulticastServiceSyncTest
{
    public partial class frmMain : Form, ITraceWriter
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            SimpleServiceSync.FailoverInterval = 10;
            SimpleServiceSync.MaxHandshakeRetry = 10;
            SimpleServiceSync.SetTraceWriter(this);
            SimpleServiceSync.OnFailover += SimpleServiceSync_OnFailover;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SimpleServiceSync.Stop();
            //mcReceiver.Stop();
        }

        void McReceiverMessageReceived(string message)
        {
            var m = string.Format("{0} received at {1}", message, DateTime.Now.ToString("u"));
            WriteLine(m);
        }

        void SimpleServiceSync_OnFailover(bool isMaster)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    tbLog.AppendText(string.Format("\r\n failover as {0}", isMaster));
                    cbMaster.Checked = isMaster;
                }));
            }
            else
            {
                tbLog.AppendText(string.Format("\r\n failover as {0}", isMaster));
                cbMaster.Checked = isMaster;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SimpleServiceSync.MachineName = tbMachineName.Text;
            SimpleServiceSync.ServiceName = tbServiceName.Text;
            SimpleServiceSync.Start();
            cbMaster.Checked = SimpleServiceSync.IsMaster;
            btnStop.Enabled = true;
            btnStart.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SimpleServiceSync.Stop();
            cbMaster.Checked = SimpleServiceSync.IsMaster;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }


        public void WriteLine(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => tbLog.AppendText(string.Format("\r\n {0}", message))));
            }
            else
            {
                tbLog.AppendText(string.Format("\r\n {0}", message));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //mcSender.SendMessage(tbMessage.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
        }

        
    }
}
