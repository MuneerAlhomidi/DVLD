namespace DVLD
{
    partial class frmUserInfo
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
            this.ctrUserCard1 = new DVLD.ctrUserCard();
            this.btnCloes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrUserCard1
            // 
            this.ctrUserCard1.BackColor = System.Drawing.Color.White;
            this.ctrUserCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctrUserCard1.Location = new System.Drawing.Point(0, 0);
            this.ctrUserCard1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrUserCard1.Name = "ctrUserCard1";
            this.ctrUserCard1.Size = new System.Drawing.Size(861, 418);
            this.ctrUserCard1.TabIndex = 0;
            // 
            // btnCloes
            // 
            this.btnCloes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloes.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnCloes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloes.Location = new System.Drawing.Point(696, 383);
            this.btnCloes.Name = "btnCloes";
            this.btnCloes.Size = new System.Drawing.Size(149, 37);
            this.btnCloes.TabIndex = 1;
            this.btnCloes.Text = "Cloes";
            this.btnCloes.UseVisualStyleBackColor = true;
            this.btnCloes.Click += new System.EventHandler(this.btnCloes_Click);
            // 
            // frmUserInfo
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(850, 431);
            this.Controls.Add(this.btnCloes);
            this.Controls.Add(this.ctrUserCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUserInfo";
            this.Text = "frm User Info";
            this.Load += new System.EventHandler(this.frmUserInfo_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        //private ctrUserCard ctrUserCard1;
        //private System.Windows.Forms.Button button1;
        //private ctrUserCard ctrUserCard2;
       // private System.Windows.Forms.Button button2;
        private ctrUserCard ctrUserCard1;
        private System.Windows.Forms.Button btnCloes;
    }
}