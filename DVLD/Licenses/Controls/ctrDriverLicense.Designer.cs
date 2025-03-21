namespace DVLD
{
    partial class ctrDriverLicense
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbLocal = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.lbRecordLocalLicens = new System.Windows.Forms.Label();
            this.lable = new System.Windows.Forms.Label();
            this.dgvLocalLicense = new System.Windows.Forms.DataGridView();
            this.cmsLocalLicenseHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowLocalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbInterNational = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRecordInternational = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.dgvInternational = new System.Windows.Forms.DataGridView();
            this.cmsInternationalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicense)).BeginInit();
            this.cmsLocalLicenseHistory.SuspendLayout();
            this.tbInterNational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternational)).BeginInit();
            this.cmsInternationalLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(0, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1054, 327);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver License";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbLocal);
            this.tabControl1.Controls.Add(this.tbInterNational);
            this.tabControl1.Location = new System.Drawing.Point(6, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1042, 293);
            this.tabControl1.TabIndex = 0;
            // 
            // tbLocal
            // 
            this.tbLocal.Controls.Add(this.label5);
            this.tbLocal.Controls.Add(this.lbRecordLocalLicens);
            this.tbLocal.Controls.Add(this.lable);
            this.tbLocal.Controls.Add(this.dgvLocalLicense);
            this.tbLocal.Location = new System.Drawing.Point(4, 34);
            this.tbLocal.Name = "tbLocal";
            this.tbLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tbLocal.Size = new System.Drawing.Size(1034, 255);
            this.tbLocal.TabIndex = 1;
            this.tbLocal.Text = "Local";
            this.tbLocal.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(236, 25);
            this.label5.TabIndex = 137;
            this.label5.Text = "Local Licenses History:";
            // 
            // lbRecordLocalLicens
            // 
            this.lbRecordLocalLicens.AutoSize = true;
            this.lbRecordLocalLicens.Location = new System.Drawing.Point(93, 221);
            this.lbRecordLocalLicens.Name = "lbRecordLocalLicens";
            this.lbRecordLocalLicens.Size = new System.Drawing.Size(34, 25);
            this.lbRecordLocalLicens.TabIndex = 5;
            this.lbRecordLocalLicens.Text = "??";
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable.Location = new System.Drawing.Point(6, 221);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(99, 25);
            this.lable.TabIndex = 4;
            this.lable.Text = "#Record:";
            // 
            // dgvLocalLicense
            // 
            this.dgvLocalLicense.AllowUserToAddRows = false;
            this.dgvLocalLicense.AllowUserToDeleteRows = false;
            this.dgvLocalLicense.AllowUserToOrderColumns = true;
            this.dgvLocalLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicense.ContextMenuStrip = this.cmsLocalLicenseHistory;
            this.dgvLocalLicense.Location = new System.Drawing.Point(6, 42);
            this.dgvLocalLicense.Name = "dgvLocalLicense";
            this.dgvLocalLicense.ReadOnly = true;
            this.dgvLocalLicense.RowHeadersWidth = 51;
            this.dgvLocalLicense.RowTemplate.Height = 24;
            this.dgvLocalLicense.Size = new System.Drawing.Size(1022, 176);
            this.dgvLocalLicense.TabIndex = 0;
            // 
            // cmsLocalLicenseHistory
            // 
            this.cmsLocalLicenseHistory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsLocalLicenseHistory.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.cmsLocalLicenseHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLocalLicenseToolStripMenuItem});
            this.cmsLocalLicenseHistory.Name = "cmsLocalLicenseHistory";
            this.cmsLocalLicenseHistory.Size = new System.Drawing.Size(233, 36);
            // 
            // ShowLocalLicenseToolStripMenuItem
            // 
            this.ShowLocalLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.ShowLocalLicenseToolStripMenuItem.Name = "ShowLocalLicenseToolStripMenuItem";
            this.ShowLocalLicenseToolStripMenuItem.Size = new System.Drawing.Size(232, 32);
            this.ShowLocalLicenseToolStripMenuItem.Text = "Show Licnese Info";
            this.ShowLocalLicenseToolStripMenuItem.Click += new System.EventHandler(this.ShowLocalLicenseToolStripMenuItem_Click);
            // 
            // tbInterNational
            // 
            this.tbInterNational.Controls.Add(this.label1);
            this.tbInterNational.Controls.Add(this.lbRecordInternational);
            this.tbInterNational.Controls.Add(this.lable1);
            this.tbInterNational.Controls.Add(this.dgvInternational);
            this.tbInterNational.Location = new System.Drawing.Point(4, 34);
            this.tbInterNational.Name = "tbInterNational";
            this.tbInterNational.Padding = new System.Windows.Forms.Padding(3);
            this.tbInterNational.Size = new System.Drawing.Size(1034, 255);
            this.tbInterNational.TabIndex = 2;
            this.tbInterNational.Text = "International";
            this.tbInterNational.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 25);
            this.label1.TabIndex = 136;
            this.label1.Text = "International Licenses History:";
            // 
            // lbRecordInternational
            // 
            this.lbRecordInternational.AutoSize = true;
            this.lbRecordInternational.Location = new System.Drawing.Point(90, 228);
            this.lbRecordInternational.Name = "lbRecordInternational";
            this.lbRecordInternational.Size = new System.Drawing.Size(34, 25);
            this.lbRecordInternational.TabIndex = 3;
            this.lbRecordInternational.Text = "??";
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable1.Location = new System.Drawing.Point(6, 228);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(99, 25);
            this.lable1.TabIndex = 2;
            this.lable1.Text = "#Record:";
            // 
            // dgvInternational
            // 
            this.dgvInternational.AllowUserToAddRows = false;
            this.dgvInternational.AllowUserToDeleteRows = false;
            this.dgvInternational.AllowUserToOrderColumns = true;
            this.dgvInternational.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternational.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternational.ContextMenuStrip = this.cmsInternationalLicense;
            this.dgvInternational.Location = new System.Drawing.Point(3, 39);
            this.dgvInternational.Name = "dgvInternational";
            this.dgvInternational.ReadOnly = true;
            this.dgvInternational.RowHeadersWidth = 51;
            this.dgvInternational.RowTemplate.Height = 24;
            this.dgvInternational.Size = new System.Drawing.Size(1025, 186);
            this.dgvInternational.TabIndex = 1;
            // 
            // cmsInternationalLicense
            // 
            this.cmsInternationalLicense.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.cmsInternationalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInfoToolStripMenuItem});
            this.cmsInternationalLicense.Name = "cmsInternationalLicense";
            this.cmsInternationalLicense.Size = new System.Drawing.Size(206, 36);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(205, 32);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Info";
            // 
            // ctrDriverLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrDriverLicense";
            this.Size = new System.Drawing.Size(1057, 340);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbLocal.ResumeLayout(false);
            this.tbLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicense)).EndInit();
            this.cmsLocalLicenseHistory.ResumeLayout(false);
            this.tbInterNational.ResumeLayout(false);
            this.tbInterNational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternational)).EndInit();
            this.cmsInternationalLicense.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbLocal;
        private System.Windows.Forms.DataGridView dgvLocalLicense;
        private System.Windows.Forms.TabPage tbInterNational;
        private System.Windows.Forms.DataGridView dgvInternational;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label lbRecordInternational;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbRecordLocalLicens;
        private System.Windows.Forms.Label lable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem ShowLocalLicenseToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
    }
}
