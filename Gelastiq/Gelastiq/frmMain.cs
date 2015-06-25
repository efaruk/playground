using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ElasticLinq;
using ElasticLinq.Mapping;
using Newtonsoft.Json;


namespace Gelastiq
{
    public partial class frmMain : Form
    {
        private string elasticHost = ConfigurationManager.AppSettings.Get("elasticHost");

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var context = new LogEventContext(elasticHost);
            var query = context.Query<LogEvent>();
            var list = query.ToList();
            foreach (var logEvent in list)
            {
                var json = JsonConvert.SerializeObject(logEvent);
                WriteLog(json);
            }
        }

        public void WriteLog(string line)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => tbLog.AppendText(string.Format("\r\n {0}", line))));
            }
            else
            {
                tbLog.AppendText(string.Format("\r\n {0}", line));
            }
        }

    }
}
