namespace DVLD
{
    partial class frmIssueDrivingLicenseFirstTime
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
            this.ctrDrivingLicenseAplicationInformation1 = new DVLD.ctrDrivingLicenseAplicationInformation();
            this.Notes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.Cloes = new System.Windows.Forms.Button();
            this.Issue = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrDrivingLicenseAplicationInformation1
            // 
            this.ctrDrivingLicenseAplicationInformation1.BackColor = System.Drawing.Color.White;
            this.ctrDrivingLicenseAplicationInformation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrDrivingLicenseAplicationInformation1.Location = new System.Drawing.Point(0, 14);
            this.ctrDrivingLicenseAplicationInformation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrDrivingLicenseAplicationInformation1.Name = "ctrDrivingLicenseAplicationInformation1";
            this.ctrDrivingLicenseAplicationInformation1.Size = new System.Drawing.Size(918, 361);
            this.ctrDrivingLicenseAplicationInformation1.TabIndex = 0;
            // 
            // Notes
            // 
            this.Notes.AutoSize = true;
            this.Notes.Location = new System.Drawing.Point(12, 393);
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(69, 25);
            this.Notes.TabIndex = 1;
            this.Notes.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(120, 390);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(775, 101);
            this.txtNotes.TabIndex = 3;
            // 
            // Cloes
            // 
            this.Cloes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cloes.Image = global::DVLD.Properties.Resources.Close_32;
            this.Cloes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cloes.Location = new System.Drawing.Point(663, 508);
            this.Cloes.Name = "Cloes";
            this.Cloes.Size = new System.Drawing.Size(105, 38);
            this.Cloes.TabIndex = 5;
            this.Cloes.Text = "Cloes";
            this.Cloes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cloes.UseVisualStyleBackColor = true;
            this.Cloes.Click += new System.EventHandler(this.Cloes_Click);
            // 
            // Issue
            // 
            this.Issue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Issue.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_321;
            this.Issue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Issue.Location = new System.Drawing.Point(790, 508);
            this.Issue.Name = "Issue";
            this.Issue.Size = new System.Drawing.Size(105, 38);
            this.Issue.TabIndex = 4;
            this.Issue.Text = "Issue";
            this.Issue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Issue.UseVisualStyleBackColor = true;
            this.Issue.Click += new System.EventHandler(this.Issue_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Notes_32;
            this.pictureBox1.Location = new System.Drawing.Point(76, 393);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frmIssueDrivingLicenseFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(928, 564);
            this.Controls.Add(this.Cloes);
            this.Controls.Add(this.Issue);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Notes);
            this.Controls.Add(this.ctrDrivingLicenseAplicationInformation1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmIssueDrivingLicenseFirstTime";
            this.Text = "Issue Driving LicenseFirst Time";
            this.Load += new System.EventHandler(this.frmIssueDrivingLicenseFirstTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrDrivingLicenseAplicationInformation ctrDrivingLicenseAplicationInformation1;
        private System.Windows.Forms.Label Notes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button Issue;
        private System.Windows.Forms.Button Cloes;
    }
}