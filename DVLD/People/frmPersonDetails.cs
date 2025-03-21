using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmPersonDetails : Form
    {
      private  int _PersonID = -1;
            
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
           
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
           ctsPersonCard1.LoadPersonInfo(_PersonID);

        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
