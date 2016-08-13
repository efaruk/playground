namespace WebAutoLogin.Client
{
    partial class frmClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.tsmiToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNotification.SuspendLayout();
            this.SuspendLayout();
            // 
            // niMain
            // 
            this.niMain.BalloonTipTitle = "Web Auto Login";
            this.niMain.ContextMenuStrip = this.cmsNotification;
            this.niMain.Icon = ((System.Drawing.Icon)(resources.GetObject("niMain.Icon")));
            this.niMain.Visible = true;
            // 
            // cmsNotification
            // 
            this.cmsNotification.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTest,
            this.tsmiToggle});
            this.cmsNotification.Name = "cmsNotification";
            this.cmsNotification.Size = new System.Drawing.Size(197, 70);
            // 
            // tsmiTest
            // 
            this.tsmiTest.Name = "tsmiTest";
            this.tsmiTest.Size = new System.Drawing.Size(196, 22);
            this.tsmiTest.Text = "Test Server Connection";
            this.tsmiTest.Click += new System.EventHandler(this.tsmiTest_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConnection.Location = new System.Drawing.Point(205, 92);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(131, 23);
            this.btnTestConnection.TabIndex = 2;
            this.btnTestConnection.Text = "Test Server Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // tsmiToggle
            // 
            this.tsmiToggle.Name = "tsmiToggle";
            this.tsmiToggle.Size = new System.Drawing.Size(196, 22);
            this.tsmiToggle.Text = "Start / Stop";
            this.tsmiToggle.Click += new System.EventHandler(this.tsmiToggle_Click);
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 127);
            this.Controls.Add(this.btnTestConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmClient";
            this.Text = "WAL Client";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmsNotification.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.ContextMenuStrip cmsNotification;
        private System.Windows.Forms.ToolStripMenuItem tsmiTest;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiToggle;
    }
}

