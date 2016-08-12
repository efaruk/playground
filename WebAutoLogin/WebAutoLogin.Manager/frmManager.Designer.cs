namespace WebAutoLogin.Manager
{
    partial class frmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
            this.btnManage = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
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
            this.tsmiTest});
            this.cmsNotification.Name = "cmsNotification";
            this.cmsNotification.Size = new System.Drawing.Size(96, 26);
            // 
            // tsmiTest
            // 
            this.tsmiTest.Name = "tsmiTest";
            this.tsmiTest.Size = new System.Drawing.Size(95, 22);
            this.tsmiTest.Text = "Test";
            this.tsmiTest.Click += new System.EventHandler(this.tsmiTest_Click);
            // 
            // btnManage
            // 
            this.btnManage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManage.Location = new System.Drawing.Point(205, 92);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(131, 23);
            this.btnManage.TabIndex = 1;
            this.btnManage.Text = "Manage Accounts";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestConnection.Location = new System.Drawing.Point(12, 92);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(131, 23);
            this.btnTestConnection.TabIndex = 2;
            this.btnTestConnection.Text = "Test Server Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 127);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnManage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Web Auto Login";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmsNotification.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.ContextMenuStrip cmsNotification;
        private System.Windows.Forms.ToolStripMenuItem tsmiTest;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Button btnTestConnection;
    }
}

