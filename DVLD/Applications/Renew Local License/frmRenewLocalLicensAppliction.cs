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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace DVLD
{
    public partial class frmRenewLocalLicensAppliction : Form
    {
        private int _NewLicenseID = -1;
        public frmRenewLocalLicensAppliction()
        {
            InitializeComponent();
        }

        private void ctrDriverLicensesInfoWithFilter1_onPersonSelected(int obj)
        {
            int SelectLicenseID = obj;

            lblOldLicenseID.Text = SelectLicenseID.ToString();
            llbShowLicenseHistory.Enabled = (SelectLicenseID != -1);

            if (SelectLicenseID == -1)
            {
                return;
            }

            int DefaultValidityLength = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.LicensClassesIfo.DefaultValidityLength;

            lblExpirationDate.Text = DateTime.Now.AddYears( DefaultValidityLength).ToString();
            lblLicenseFees.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.PaidFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text).ToString());
            txtNotes.Text = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.Notes.ToString();

            if (!ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.IsLicenseExpird())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.ExpirationDate),
                    "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
               // llbShowLicenseInfo.Enabled = false;
                return;
            }

            if(!ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license.","Not Allow",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }
            
                btnRenew.Enabled = true;

        }

        private void frmRenewLocalLicensAppliction_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();

            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserID.ToString();

            lblExpirationDate.Text = "[????]";
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);
            if(NewLicense != null )
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrDriverLicensesInfoWithFilter1.Enabled = false;

            llbShowLicenseInfo.Enabled = true;

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
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();

        }

        private void frmRenewLocalLicensAppliction_Activated(object sender, EventArgs e)
        {
            ctrDriverLicensesInfoWithFilter1.txtLicenseIDfocus();
        }
    }
}
