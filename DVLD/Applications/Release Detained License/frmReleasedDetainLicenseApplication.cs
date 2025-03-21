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
using DVLD_Buisness;

namespace DVLD
{
    public partial class frmReleasedDetainLicenseApplication : Form
    {
        private int _SelectLicenseID = -1;
        public frmReleasedDetainLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _SelectLicenseID = LicenseID;

            ctrDriverLicensesInfoWithFilter1.LoadLicenseInfo(_SelectLicenseID);
            ctrDriverLicensesInfoWithFilter1.FilterEnabled = false;
        }

        public frmReleasedDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrDriverLicensesInfoWithFilter1_onPersonSelected(int obj)
        {
            _SelectLicenseID = obj;
            lblLicenseID.Text = _SelectLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectLicenseID != -1);

            if(_SelectLicenseID  == -1)
            {
                return;
            }

            if(!ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License  is not detained, choose another one. ", "Not Allow",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            lblDetainID.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DetainedInfo.LicenseID.ToString();

            lbApplicationFees.Text =clsApplicationTypes.Find( (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DetainedInfo.DetainDate);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            //lblCreatedByUser.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;
            lbFineFees.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DetainedInfo.FineFees.ToString();
            lbTotalFees.Text = (Convert.ToSingle(lbApplicationFees.Text) + Convert.ToSingle(lbFineFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sour do you went Release License whit ID = " + lblDetainID.Text.ToString(), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsRelease = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.ReleasedDetainLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);
            lbApplicationID.Text = ApplicationID.ToString();

            if(!IsRelease)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License is Successfully Release","Save",MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false ;
            llShowLicenseInfo.Enabled = true ;
            ctrDriverLicensesInfoWithFilter1.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm =new frmShowLicensePersonHistory(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectLicenseID);
            frm.ShowDialog();
        }

        private void frmReleasedDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrDriverLicensesInfoWithFilter1.txtLicenseIDfocus();
        }
    }
}
