using DVLD.Classes;
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
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {

        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;
        private int _LocaldrivingLicenseApplicationID;

        public frmIssueDrivingLicenseFirstTime(int LocaldrivingLicenseApplication)
        {
            InitializeComponent();
            _LocaldrivingLicenseApplicationID = LocaldrivingLicenseApplication;
            
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();

            
            _LocalDrivingLicenseApplications =    clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(_LocaldrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplications == null )
            {
                MessageBox.Show("No, Application with ID = "+ _LocaldrivingLicenseApplicationID,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if( !_LocalDrivingLicenseApplications.PassedAllTest())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int License = _LocalDrivingLicenseApplications.GetActiveLicensID();
             
            if(License != -1)
            {
                MessageBox.Show("Person Already has License befor with License  ID = " + License.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrDrivingLicenseAplicationInformation1.LoadApplicationInfoByLocalDrivingAppID(_LocaldrivingLicenseApplicationID);
        }

        private void Issue_Click(object sender, EventArgs e)
        {
            int License = _LocalDrivingLicenseApplications.IssueLicenseFirstTime(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);
            if (License != -1)
            {
                MessageBox.Show("Issue License Successfuly with ID = " + License.ToString(), "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
               
            }
            else
                MessageBox.Show("License Was Not Issue! ", "Falid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
        }

        private void Cloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
