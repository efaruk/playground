using System;
using System.Windows.Forms;
using Sifu.Dialogs;
using StackExchange.Redis;

namespace Sifu
{
	public partial class frmMain : Form
	{
		private readonly ConnectServerDialog _connectServerDialog = new ConnectServerDialog();
		private static  readonly SharedConnection sharedConnection = SharedConnection.Instance;

		//private const string GET_DATABASE_ITEM_COUNT_SCRIPT = "local karr={}; karr=redis.call('SCAN', '0', 'MATCH', '*'); return table.getn(karr[2]);";
		private const string GET_DATABASE_ALL_KEYS_SCRIPT = "local karr={}; karr=redis.call('SCAN', '0', 'MATCH', '*'); return karr[2];";

		private const int REDIS_DATABASE_COUNT = 16;

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			sharedConnection.OnSharedConnectionStatusChanged += OnSharedConnectionStatusChanged;
			sharedConnection.OnTimerElapsed += sharedConnection_OnTimerElapsed;
		}

		void sharedConnection_OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (sharedConnection.Connected)
			{
				LoadServerTree();
			}
		}

		void OnSharedConnectionStatusChanged(bool connected)
		{
			tsmiConnect.Enabled = !connected;
			tsmiDisconnect.Enabled = connected;
		}

		private void tsmiConnect_Click(object sender, EventArgs e)
		{
			var result = _connectServerDialog.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				LoadServerTree();
			}
		}

		private void tsmiDisconnect_Click(object sender, EventArgs e)
		{
			sharedConnection.Disconnect();
			ClearServerTree();
		}

		private void LoadServerTree()
		{
			tvServer.Nodes.Clear();
			for (int i = 0; i < REDIS_DATABASE_COUNT; i++)
			{
				var database = sharedConnection.RedisConnection.GetDatabase(i);
				var result = database.ScriptEvaluate(GET_DATABASE_ALL_KEYS_SCRIPT);
				if (!result.IsNull)
				{
					var tn = new TreeNode("database [" + i + "]") {Tag = i};
					if (result.GetType().Name.Contains("ArrayRedisResult"))
					{
						var results = (RedisResult[]) result;
						foreach (var redisResult in results)
						{
							var redisValue = (RedisValue) redisResult;
							var tnc = new TreeNode(redisValue) {Name = redisValue};
							tn.Nodes.Add(tnc);
						}
					}
					tvServer.Nodes.Add(tn);
				}
			}
		}

		private void ClearServerTree()
		{
			tvServer.Nodes.Clear();
		}

		private void tvServer_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				int databaseId = -1;
				if (e.Node.Tag != null && int.TryParse(e.Node.Tag.ToString(), out databaseId))
				{ // Database Node

				}
				else
				{ // Key
					if (int.TryParse(e.Node.Parent.Tag.ToString(), out databaseId))
					{ // Database Node
						var database = sharedConnection.RedisConnection.GetDatabase(databaseId);
						var value = database.StringGet(e.Node.Text);
						tbValue.Text = value;
					}
				}
			}
		}

	}
}
