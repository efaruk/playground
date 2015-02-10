namespace Sifu.Dialogs
{
	partial class ConnectServerDialog
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
			this.label1 = new System.Windows.Forms.Label();
			this.tbAddress = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
			this.nudInterval = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Host :";
			// 
			// tbAddress
			// 
			this.tbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbAddress.Location = new System.Drawing.Point(111, 10);
			this.tbAddress.Name = "tbAddress";
			this.tbAddress.Size = new System.Drawing.Size(447, 21);
			this.tbAddress.TabIndex = 6;
			this.tbAddress.Text = "localhost";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(402, 97);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(483, 97);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cbAutoRefresh
			// 
			this.cbAutoRefresh.AutoSize = true;
			this.cbAutoRefresh.Checked = true;
			this.cbAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAutoRefresh.Location = new System.Drawing.Point(111, 38);
			this.cbAutoRefresh.Name = "cbAutoRefresh";
			this.cbAutoRefresh.Size = new System.Drawing.Size(263, 17);
			this.cbAutoRefresh.TabIndex = 10;
			this.cbAutoRefresh.Text = "Auto Refresh Every                                  Seconds";
			this.cbAutoRefresh.UseVisualStyleBackColor = true;			
			// 
			// nudInterval
			// 
			this.nudInterval.Location = new System.Drawing.Point(233, 37);
			this.nudInterval.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudInterval.Name = "nudInterval";
			this.nudInterval.Size = new System.Drawing.Size(86, 21);
			this.nudInterval.TabIndex = 11;
			this.nudInterval.ThousandsSeparator = true;
			this.nudInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// ConnectServerDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(570, 132);
			this.Controls.Add(this.nudInterval);
			this.Controls.Add(this.cbAutoRefresh);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbAddress);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ConnectServerDialog";
			this.Text = "Connect Redis";
			this.Load += new System.EventHandler(this.DialogConnectServer_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbAddress;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox cbAutoRefresh;
		private System.Windows.Forms.NumericUpDown nudInterval;
	}
}