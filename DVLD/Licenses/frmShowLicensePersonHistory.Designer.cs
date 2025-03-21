namespace DVLD
{
    partial class frmShowLicensePersonHistory
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCloes = new System.Windows.Forms.Button();
            this.ctrDriverLicense1 = new DVLD.ctrDriverLicense();
            this.ctrPersonCardWithFilter1 = new DVLD.CtrPersonCardWithFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(-7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1103, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "License History";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.Location = new System.Drawing.Point(1, 207);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 202);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnCloes
            // 
            this.btnCloes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloes.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnCloes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloes.Location = new System.Drawing.Point(913, 772);
            this.btnCloes.Name = "btnCloes";
            this.btnCloes.Size = new System.Drawing.Size(157, 38);
            this.btnCloes.TabIndex = 5;
            this.btnCloes.Text = "Cloes";
            this.btnCloes.UseVisualStyleBackColor = true;
            this.btnCloes.Click += new System.EventHandler(this.btnCloes_Click);
            // 
            // ctrDriverLicense1
            // 
            this.ctrDriverLicense1.BackColor = System.Drawing.Color.White;
            this.ctrDriverLicense1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrDriverLicense1.Location = new System.Drawing.Point(13, 435);
            this.ctrDriverLicense1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrDriverLicense1.Name = "ctrDriverLicense1";
            this.ctrDriverLicense1.Size = new System.Drawing.Size(1057, 329);
            this.ctrDriverLicense1.TabIndex = 4;
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.BackColor = System.Drawing.Color.White;
            this.ctrPersonCardWithFilter1.FilterEnabled = false;
            this.ctrPersonCardWithFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(242, 50);
            this.ctrPersonCardWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(841, 384);
            this.ctrPersonCardWithFilter1.TabIndex = 3;
            this.ctrPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrPersonCardWithFilter1_OnPersonSelected);
            // 
            // frmShowLicensePersonHistory
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1086, 825);
            this.Controls.Add(this.btnCloes);
            this.Controls.Add(this.ctrDriverLicense1);
            this.Controls.Add(this.ctrPersonCardWithFilter1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowLicensePersonHistory";
            this.Text = "License History";
            this.Load += new System.EventHandler(this.frmShowLicensePersonHistory_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CtrPersonCardWithFilter ctrPersonCardWithFilter1;
        private ctrDriverLicense ctrDriverLicense1;
        private System.Windows.Forms.Button btnCloes;
    }
}