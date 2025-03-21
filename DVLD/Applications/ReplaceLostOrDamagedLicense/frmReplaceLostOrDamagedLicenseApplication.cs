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
using static DVLD_Buisness.clsLicense;

namespace DVLD
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private int _GetApplicationType()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                lblTitle.Text = "Replace for Damaged Licens";
                this.Text = lblTitle.Text;
                lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationType()).ApplicationFees.ToString();
            }  
        }

        private void radioLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLostLicense.Checked)
            {
                lblTitle.Text = "Replace for Lost License";
                this.Text = lblTitle.Text;
                lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationType()).ApplicationFees.ToString();
            }
        }

      
        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserID.ToString();

            rbDamagedLicense.Enabled = true;
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrDriverLicensesInfoWithFilter1.txtLicenseIDfocus();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sour you went Reblecment ??", "Confi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.No)
            { return; }

            clsLicense NewLicense = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.Replace(_GetIssueReason(),clsGlobal.CurrentUser.UserID);

            if(NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();

            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrDriverLicensesInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void ctrDriverLicensesInfoWithFilter1_onPersonSelected(int obj)
        {
            int SelectLicenseID = obj;

            lblOldLicenseID.Text = SelectLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectLicenseID != -1);

            if (SelectLicenseID == -1)
            {
                return;
            }

            if (!ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license.", "Not Allow",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }
    }
}
