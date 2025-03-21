using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLoaclDrivingLicenseApplicationInfo : Form
    {
        private int _ApplicationID=-1;

        public frmLoaclDrivingLicenseApplicationInfo(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void frmLoaclDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrDrivingLicenseAplicationInformation1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);
        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
