namespace DVLD
{
    partial class frmListDetainedLicenseAppliction
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.btnDetained = new System.Windows.Forms.Button();
            this.btnReleased = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvDetainedLicense = new System.Windows.Forms.DataGridView();
            this.lbRecord = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbIsRelease = new System.Windows.Forms.ComboBox();
            this.cmsApplication = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowPersonDetailsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLicensDetailsStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.PersonHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releasedDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).BeginInit();
            this.cmsApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(-6, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1188, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "List Detained Licene";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter By :";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National No",
            "Full Name",
            "Released Application ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(105, 256);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(214, 33);
            this.cbFilterBy.TabIndex = 3;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(334, 259);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(183, 30);
            this.txtFilterValue.TabIndex = 4;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // btnDetained
            // 
            this.btnDetained.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetained.Image = global::DVLD.Properties.Resources.Detain_641;
            this.btnDetained.Location = new System.Drawing.Point(1306, 229);
            this.btnDetained.Name = "btnDetained";
            this.btnDetained.Size = new System.Drawing.Size(73, 61);
            this.btnDetained.TabIndex = 6;
            this.btnDetained.UseVisualStyleBackColor = true;
            this.btnDetained.Click += new System.EventHandler(this.btnDetained_Click);
            // 
            // btnReleased
            // 
            this.btnReleased.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReleased.Image = global::DVLD.Properties.Resources.Release_Detained_License_64;
            this.btnReleased.Location = new System.Drawing.Point(1225, 229);
            this.btnReleased.Name = "btnReleased";
            this.btnReleased.Size = new System.Drawing.Size(75, 61);
            this.btnReleased.TabIndex = 5;
            this.btnReleased.UseVisualStyleBackColor = true;
            this.btnReleased.Click += new System.EventHandler(this.btnReleased_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(463, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 179);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // dgvDetainedLicense
            // 
            this.dgvDetainedLicense.AllowUserToAddRows = false;
            this.dgvDetainedLicense.AllowUserToDeleteRows = false;
            this.dgvDetainedLicense.AllowUserToOrderColumns = true;
            this.dgvDetainedLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetainedLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicense.ContextMenuStrip = this.cmsApplication;
            this.dgvDetainedLicense.Location = new System.Drawing.Point(6, 295);
            this.dgvDetainedLicense.Name = "dgvDetainedLicense";
            this.dgvDetainedLicense.ReadOnly = true;
            this.dgvDetainedLicense.RowHeadersWidth = 51;
            this.dgvDetainedLicense.RowTemplate.Height = 24;
            this.dgvDetainedLicense.Size = new System.Drawing.Size(1380, 293);
            this.dgvDetainedLicense.TabIndex = 7;
            // 
            // lbRecord
            // 
            this.lbRecord.AutoSize = true;
            this.lbRecord.Location = new System.Drawing.Point(105, 601);
            this.lbRecord.Name = "lbRecord";
            this.lbRecord.Size = new System.Drawing.Size(34, 25);
            this.lbRecord.TabIndex = 8;
            this.lbRecord.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 601);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "#Record :";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1247, 593);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(139, 41);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbIsRelease
            // 
            this.cbIsRelease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsRelease.FormattingEnabled = true;
            this.cbIsRelease.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsRelease.Location = new System.Drawing.Point(325, 259);
            this.cbIsRelease.Name = "cbIsRelease";
            this.cbIsRelease.Size = new System.Drawing.Size(107, 33);
            this.cbIsRelease.TabIndex = 11;
            this.cbIsRelease.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cmsApplication
            // 
            this.cmsApplication.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsApplication.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowPersonDetailsStripMenuItem,
            this.ShowLicensDetailsStripMenuItem1,
            this.PersonHistoryToolStripMenuItem,
            this.releasedDetainedLicenseToolStripMenuItem});
            this.cmsApplication.Name = "cmsApplication";
            this.cmsApplication.Size = new System.Drawing.Size(282, 184);
            // 
            // ShowPersonDetailsStripMenuItem
            // 
            this.ShowPersonDetailsStripMenuItem.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.ShowPersonDetailsStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowPersonDetailsStripMenuItem.Name = "ShowPersonDetailsStripMenuItem";
            this.ShowPersonDetailsStripMenuItem.Size = new System.Drawing.Size(281, 38);
            this.ShowPersonDetailsStripMenuItem.Text = "Show Person Details";
            this.ShowPersonDetailsStripMenuItem.Click += new System.EventHandler(this.fToolStripMenuItem_Click);
            // 
            // ShowLicensDetailsStripMenuItem1
            // 
            this.ShowLicensDetailsStripMenuItem1.Image = global::DVLD.Properties.Resources.License_View_32;
            this.ShowLicensDetailsStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicensDetailsStripMenuItem1.Name = "ShowLicensDetailsStripMenuItem1";
            this.ShowLicensDetailsStripMenuItem1.Size = new System.Drawing.Size(281, 38);
            this.ShowLicensDetailsStripMenuItem1.Text = "Show License Details";
            this.ShowLicensDetailsStripMenuItem1.Click += new System.EventHandler(this.fToolStripMenuItem1_Click);
            // 
            // PersonHistoryToolStripMenuItem
            // 
            this.PersonHistoryToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.PersonHistoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PersonHistoryToolStripMenuItem.Name = "PersonHistoryToolStripMenuItem";
            this.PersonHistoryToolStripMenuItem.Size = new System.Drawing.Size(281, 38);
            this.PersonHistoryToolStripMenuItem.Text = "Show Person License History";
            this.PersonHistoryToolStripMenuItem.Click += new System.EventHandler(this.PersonHistoryToolStripMenuItem_Click);
            // 
            // releasedDetainedLicenseToolStripMenuItem
            // 
            this.releasedDetainedLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.Detain_32;
            this.releasedDetainedLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.releasedDetainedLicenseToolStripMenuItem.Name = "releasedDetainedLicenseToolStripMenuItem";
            this.releasedDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(281, 38);
            this.releasedDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releasedDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releasedDetainedLicenseToolStripMenuItem_Click);
            // 
            // frmListDetainedLicenseAppliction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1387, 643);
            this.Controls.Add(this.cbIsRelease);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbRecord);
            this.Controls.Add(this.dgvDetainedLicense);
            this.Controls.Add(this.btnDetained);
            this.Controls.Add(this.btnReleased);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListDetainedLicenseAppliction";
            this.Text = "f";
            this.Load += new System.EventHandler(this.frmListDetainedLicenseAppliction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicense)).EndInit();
            this.cmsApplication.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnReleased;
        private System.Windows.Forms.Button btnDetained;
        private System.Windows.Forms.DataGridView dgvDetainedLicense;
        private System.Windows.Forms.Label lbRecord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbIsRelease;
        private System.Windows.Forms.ContextMenuStrip cmsApplication;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonDetailsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLicensDetailsStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem PersonHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releasedDetainedLicenseToolStripMenuItem;
    }
}