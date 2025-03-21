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
using DVLD.Classes;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class ctrDrivingLicenseAplicationInformation : UserControl
    {
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _LicenseID = -1;
        public int LocalDrivingLicenseApplicationID 
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctrDrivingLicenseAplicationInformation()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplications =
            clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplications == null)
            {
                _RestApplicationInfo();
                MessageBox.Show("No,ApplicationID With =  "+ LocalDrivingLicenseApplicationID.ToString() , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
            
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplications=
         clsLocalDrivingLicenseApplications.FindByApplicationID(ApplicationID);
            if (_LocalDrivingLicenseApplications == null)
            {
                _RestApplicationInfo();
                MessageBox.Show("No, ApplicationID with = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
        }


        private void _RestApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrApllicationBasicInformation1.RestApplicationInfo();
            lbDLAppID.Text = "[????]";
            lbAppliedForLicense.Text = "[???]";
 
        }

        private void _FillApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplications.GetActiveLicensID();

            llbShowLicenseInfo.Enabled = (_LicenseID != -1);

            lbDLAppID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
            lbAppliedForLicense.Text = clsLicensClasses.Find(_LocalDrivingLicenseApplications.LicenseClassID).ClassName;
            lbPessedTests.Text =_LocalDrivingLicenseApplications.GetPassedTestCount().ToString() + "/3";
            ctrApllicationBasicInformation1.LoadApplicationInfo(_LocalDrivingLicenseApplications.ApplicationID);
        }

       
    }
}
