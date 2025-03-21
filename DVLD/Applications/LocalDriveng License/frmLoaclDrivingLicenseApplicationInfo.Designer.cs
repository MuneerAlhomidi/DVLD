namespace DVLD
{
    partial class frmLoaclDrivingLicenseApplicationInfo
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
            this.btnCloes = new System.Windows.Forms.Button();
            this.ctrDrivingLicenseAplicationInformation1 = new DVLD.ctrDrivingLicenseAplicationInformation();
            this.SuspendLayout();
            // 
            // btnCloes
            // 
            this.btnCloes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloes.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnCloes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloes.Location = new System.Drawing.Point(787, 395);
            this.btnCloes.Name = "btnCloes";
            this.btnCloes.Size = new System.Drawing.Size(124, 35);
            this.btnCloes.TabIndex = 1;
            this.btnCloes.Text = "Cloes";
            this.btnCloes.UseVisualStyleBackColor = true;
            this.btnCloes.Click += new System.EventHandler(this.btnCloes_Click);
            // 
            // ctrDrivingLicenseAplicationInformation1
            // 
            this.ctrDrivingLicenseAplicationInformation1.BackColor = System.Drawing.Color.White;
            this.ctrDrivingLicenseAplicationInformation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrDrivingLicenseAplicationInformation1.Location = new System.Drawing.Point(2, 14);
            this.ctrDrivingLicenseAplicationInformation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrDrivingLicenseAplicationInformation1.Name = "ctrDrivingLicenseAplicationInformation1";
            this.ctrDrivingLicenseAplicationInformation1.Size = new System.Drawing.Size(909, 360);
            this.ctrDrivingLicenseAplicationInformation1.TabIndex = 0;
            // 
            // frmLoaclDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(927, 454);
            this.Controls.Add(this.btnCloes);
            this.Controls.Add(this.ctrDrivingLicenseAplicationInformation1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLoaclDrivingLicenseApplicationInfo";
            this.Text = "frmLoaclDrivingLicenseApplicationInfo";
            this.Load += new System.EventHandler(this.frmLoaclDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrDrivingLicenseAplicationInformation ctrDrivingLicenseAplicationInformation1;
        private System.Windows.Forms.Button btnCloes;
    }
}