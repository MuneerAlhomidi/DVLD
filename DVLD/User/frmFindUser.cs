using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD
{
    public partial class    frmUserInfo : Form
    {

        private int _UserID ;
      
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
           _UserID = UserID;
        }

        public frmUserInfo()
        {
            InitializeComponent();
           
        }

        

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load_1(object sender, EventArgs e)
        {
            ctrUserCard1.LoadUserInfo(_UserID);
        }
    }
}
