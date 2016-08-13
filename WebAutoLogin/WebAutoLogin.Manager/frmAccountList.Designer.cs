namespace WebAutoLogin.Manager
{
    partial class frmAccountList
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
            this.dgAccounts = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tokenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lockedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.adminDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bsAccounts = new System.Windows.Forms.BindingSource(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbList = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbSetupUsbStick = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAccounts)).BeginInit();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgAccounts
            // 
            this.dgAccounts.AllowUserToAddRows = false;
            this.dgAccounts.AllowUserToDeleteRows = false;
            this.dgAccounts.AllowUserToOrderColumns = true;
            this.dgAccounts.AutoGenerateColumns = false;
            this.dgAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.tokenDataGridViewTextBoxColumn,
            this.lockedDataGridViewCheckBoxColumn,
            this.adminDataGridViewCheckBoxColumn});
            this.dgAccounts.DataSource = this.bsAccounts;
            this.dgAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAccounts.EnableHeadersVisualStyles = false;
            this.dgAccounts.Location = new System.Drawing.Point(0, 25);
            this.dgAccounts.MultiSelect = false;
            this.dgAccounts.Name = "dgAccounts";
            this.dgAccounts.ReadOnly = true;
            this.dgAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAccounts.Size = new System.Drawing.Size(782, 402);
            this.dgAccounts.TabIndex = 0;
            // 
            // idColumn
            // 
            this.idColumn.DataPropertyName = "Id";
            this.idColumn.HeaderText = "Id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "FullName";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tokenDataGridViewTextBoxColumn
            // 
            this.tokenDataGridViewTextBoxColumn.DataPropertyName = "Token";
            this.tokenDataGridViewTextBoxColumn.HeaderText = "Token";
            this.tokenDataGridViewTextBoxColumn.Name = "tokenDataGridViewTextBoxColumn";
            this.tokenDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lockedDataGridViewCheckBoxColumn
            // 
            this.lockedDataGridViewCheckBoxColumn.DataPropertyName = "Locked";
            this.lockedDataGridViewCheckBoxColumn.HeaderText = "Locked";
            this.lockedDataGridViewCheckBoxColumn.Name = "lockedDataGridViewCheckBoxColumn";
            this.lockedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // adminDataGridViewCheckBoxColumn
            // 
            this.adminDataGridViewCheckBoxColumn.DataPropertyName = "Admin";
            this.adminDataGridViewCheckBoxColumn.HeaderText = "Admin";
            this.adminDataGridViewCheckBoxColumn.Name = "adminDataGridViewCheckBoxColumn";
            this.adminDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // bsAccounts
            // 
            this.bsAccounts.AllowNew = false;
            this.bsAccounts.DataSource = typeof(WebAutoLogin.Data.Entities.Account);
            // 
            // tsMain
            // 
            this.tsMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbEdit,
            this.tsbList,
            this.tsbDelete,
            this.tsbSetupUsbStick});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(782, 25);
            this.tsMain.TabIndex = 1;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(36, 22);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(32, 22);
            this.tsbEdit.Text = "Edit";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbList
            // 
            this.tsbList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbList.Name = "tsbList";
            this.tsbList.Size = new System.Drawing.Size(29, 22);
            this.tsbList.Text = "List";
            this.tsbList.Click += new System.EventHandler(this.tsbList_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(47, 22);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbSetupUsbStick
            // 
            this.tsbSetupUsbStick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSetupUsbStick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetupUsbStick.Name = "tsbSetupUsbStick";
            this.tsbSetupUsbStick.Size = new System.Drawing.Size(100, 22);
            this.tsbSetupUsbStick.Text = "Setup USB Stick";
            this.tsbSetupUsbStick.Click += new System.EventHandler(this.tsbSetupUsbStick_Click);
            // 
            // frmAccountList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 427);
            this.Controls.Add(this.dgAccounts);
            this.Controls.Add(this.tsMain);
            this.Name = "frmAccountList";
            this.Text = "Account List - WAL Manager";
            this.Load += new System.EventHandler(this.frmAccountList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAccounts)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgAccounts;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ısLockedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ısAdminDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource bsAccounts;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbList;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tokenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn lockedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn adminDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripButton tsbSetupUsbStick;
    }
}