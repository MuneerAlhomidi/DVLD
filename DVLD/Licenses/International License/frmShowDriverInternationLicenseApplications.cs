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
    public partial class frmShowDriverInternationLicenseApplications : Form
    {
        private int _InternationalLicenseID = -1;
        public frmShowDriverInternationLicenseApplications(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowDriverInternationLicenseApplications_Load(object sender, EventArgs e)
        {
            ctrInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
        }
    }
}
