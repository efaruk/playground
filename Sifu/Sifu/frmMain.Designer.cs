namespace Sifu
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.msMain = new System.Windows.Forms.MenuStrip();
			this.tsmiServer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDisconnect = new System.Windows.Forms.ToolStripMenuItem();
			this.ssMain = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.tvServer = new System.Windows.Forms.TreeView();
			this.lvServer = new System.Windows.Forms.ListView();
			this.msMain.SuspendLayout();
			this.ssMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiServer});
			this.msMain.Location = new System.Drawing.Point(0, 0);
			this.msMain.Name = "msMain";
			this.msMain.Size = new System.Drawing.Size(785, 24);
			this.msMain.TabIndex = 0;
			this.msMain.Text = "Main Menu";
			// 
			// tsmiServer
			// 
			this.tsmiServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.tsmiDisconnect});
			this.tsmiServer.Name = "tsmiServer";
			this.tsmiServer.Size = new System.Drawing.Size(51, 20);
			this.tsmiServer.Text = "&Server";
			// 
			// tsmiConnect
			// 
			this.tsmiConnect.Name = "tsmiConnect";
			this.tsmiConnect.Size = new System.Drawing.Size(152, 22);
			this.tsmiConnect.Text = "&Connect";
			this.tsmiConnect.Click += new System.EventHandler(this.tsmiConnect_Click);
			// 
			// tsmiDisconnect
			// 
			this.tsmiDisconnect.Name = "tsmiDisconnect";
			this.tsmiDisconnect.Size = new System.Drawing.Size(152, 22);
			this.tsmiDisconnect.Text = "&Disconnect";
			// 
			// ssMain
			// 
			this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
			this.ssMain.Location = new System.Drawing.Point(0, 502);
			this.ssMain.Name = "ssMain";
			this.ssMain.Size = new System.Drawing.Size(785, 22);
			this.ssMain.TabIndex = 1;
			this.ssMain.Text = "Status";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(88, 17);
			this.tsslStatus.Text = "Status: Ready...";
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(0, 24);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.tvServer);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.lvServer);
			this.scMain.Size = new System.Drawing.Size(785, 478);
			this.scMain.SplitterDistance = 261;
			this.scMain.TabIndex = 2;
			// 
			// tvServer
			// 
			this.tvServer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvServer.Location = new System.Drawing.Point(0, 0);
			this.tvServer.Name = "tvServer";
			this.tvServer.Size = new System.Drawing.Size(261, 478);
			this.tvServer.TabIndex = 0;
			// 
			// lvServer
			// 
			this.lvServer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvServer.Location = new System.Drawing.Point(0, 0);
			this.lvServer.Name = "lvServer";
			this.lvServer.Size = new System.Drawing.Size(520, 478);
			this.lvServer.TabIndex = 0;
			this.lvServer.UseCompatibleStateImageBehavior = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(785, 524);
			this.Controls.Add(this.scMain);
			this.Controls.Add(this.ssMain);
			this.Controls.Add(this.msMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.MainMenuStrip = this.msMain;
			this.Name = "frmMain";
			this.Text = "Sifu";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.ssMain.ResumeLayout(false);
			this.ssMain.PerformLayout();
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
			this.scMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip msMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiServer;
		private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
		private System.Windows.Forms.ToolStripMenuItem tsmiDisconnect;
		private System.Windows.Forms.StatusStrip ssMain;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TreeView tvServer;
		private System.Windows.Forms.ListView lvServer;
	}
}

