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
    public partial class frmNewInternationalLicense : Form
    {
        private int _InternationalID = -1;

        public frmNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        public void InternationalInfo()
        {

        }

        private void ctrDriverLicensesInfoWithFilter1_onPersonSelected(int obj)
        {
            int SelectLicenseID = obj;

            lblLocalLicenseID.Text = SelectLicenseID.ToString();
            llbShowLicenseHistory.Enabled = (SelectLicenseID != -1);

            if(SelectLicenseID == -1)
            {
                return;
            }

            if(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicense = clsInternationalLicense.GetActiveInternationLicenseByDriverID(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverID);
            if (ActiveInternationalLicense != -1)
            {
                MessageBox.Show("Person Already Have Internation License with ID = " + ActiveInternationalLicense.ToString(), "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llbShowLicenseInfo.Enabled = true;
                _InternationalID = ActiveInternationalLicense;
            }
            else
                btnIssue.Enabled = true;
            
              
                


        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //base class Appliction

            InternationalLicense.ApplicationPersonID = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.AppliactionStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            InternationalLicense.DriverID = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Faild Issue International License!."+_InternationalID.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            llbShowLicenseInfo.Enabled = true;
            ctrDriverLicensesInfoWithFilter1.Enabled = false;




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowLicenseInfo frm = new frmShowLicenseInfo(_InternationalID);
            frm.ShowDialog();

        }

        private void frmNewInternationalLicense_Activated(object sender, EventArgs e)
        {
            ctrDriverLicensesInfoWithFilter1.txtLicenseIDfocus();
        }
    }
}
