using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Sifu.Dialogs;
using StackExchange.Redis;

namespace Sifu
{
	public partial class frmMain : Form
	{
		private readonly ConnectServerDialog _connectServerDialog = new ConnectServerDialog();
		private static  readonly SharedConnection sharedConnection = SharedConnection.Instance;

		private const string GET_DATABASE_ITEM_COUNT_SCRIPT = "local karr={}; karr=redis.call('SCAN', '0', 'MATCH', '*'); return table.getn(karr[2]);";
		private const string GET_DATABASE_ALL_KEYS_SCRIPT = "local karr={}; karr=redis.call('SCAN', '0', 'MATCH', '*'); return karr[2];";

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{

		}

		private void tsmiConnect_Click(object sender, EventArgs e)
		{
			var result = _connectServerDialog.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				LoadServerTree();
			}
		}

		private void LoadServerTree()
		{
			tvServer.Nodes.Clear();
			for (int i = 0; i < 16; i++)
			{
				var database = sharedConnection.RedisConnection.GetDatabase(i);
				var result = database.ScriptEvaluate(GET_DATABASE_ALL_KEYS_SCRIPT);
				if (!result.IsNull)
				{
					
					var tn = new TreeNode("db" + i) {Name = i.ToString(CultureInfo.InvariantCulture)};
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
	}
}
