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
    public partial class frmShowLicensePersonHistory : Form
    {
        private int _PersonID = -1;
        public frmShowLicensePersonHistory()
        {
            InitializeComponent();
          
        }

        public frmShowLicensePersonHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowLicensePersonHistory_Load_1(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrPersonCardWithFilter1.LoadPresonInfo(_PersonID);
                ctrPersonCardWithFilter1.Enabled = true;
                ctrDriverLicense1.LoadInfoByPerson(_PersonID);
            }
            else
            {
                ctrPersonCardWithFilter1.Enabled=false;
                ctrPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if(_PersonID == -1)
            {
                ctrDriverLicense1.Clear();
            }
            else
                ctrDriverLicense1.LoadInfoByPerson(_PersonID);
        }
    }
}
