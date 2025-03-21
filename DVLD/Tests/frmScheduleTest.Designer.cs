namespace DVLD
{
    partial class frmScheduleTest
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
            this.ctrrSchduleTest1 = new DVLD.ctrrSchduleTest();
            this.SuspendLayout();
            // 
            // ctrrSchduleTest1
            // 
            this.ctrrSchduleTest1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrrSchduleTest1.Location = new System.Drawing.Point(0, 0);
            this.ctrrSchduleTest1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrrSchduleTest1.Name = "ctrrSchduleTest1";
            this.ctrrSchduleTest1.Size = new System.Drawing.Size(533, 725);
            this.ctrrSchduleTest1.TabIndex = 0;
            this.ctrrSchduleTest1.TestTypeID = DVLD_Buisness.clsTestTypes.enTestType.StreetTest;
            // 
            // frmScheduleTest
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(538, 714);
            this.Controls.Add(this.ctrrSchduleTest1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmScheduleTest";
            this.Text = "Schdule Test";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private ctrrSchduleTest ctrrSchduleTest1;
    }
}