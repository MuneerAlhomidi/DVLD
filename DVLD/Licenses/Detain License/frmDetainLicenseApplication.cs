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
    public partial class frmDetainLicenseApplication : Form
    {
        private int _DetainID = -1;
        private int _SelectLicenseID  = -1;
        private clsDetainedLicenses _DetainedLicenses;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

       

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort( DateTime.Now);
            lblCreatedByUser .Text = clsGlobal.CurrentUser.UserName;

            //btnDetain.Enabled = false;
        }

        private void ctrDriverLicensesInfoWithFilter1_onPersonSelected(int obj)
        {
            _SelectLicenseID = obj;

            lblLicenseID.Text = _SelectLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectLicenseID != -1);

            if(_SelectLicenseID == -1)
            {
                return;
            }

            if(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.IsDetained )
            {
                MessageBox.Show("Select License Already Detained. Chooese another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //lblDetainID.Text = _DetainID.ToString();
            //txtFineFees.Text =Convert.ToSingle( _DetainedLicenses.FineFees).ToString();

            btnDetain.Enabled = true;
            txtFineFees.Focus();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sour do you went Relese Detain License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                
            }



            _DetainID = ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblDetainID.Text = _DetainID.ToString();
            // txtFineFees.Text = Convert.ToSingle(_DetainedLicenses.FineFees).ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrDriverLicensesInfoWithFilter1.Enabled = false;
            txtFineFees.Enabled = false;
          //  llShowLicenseInfo.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(ctrDriverLicensesInfoWithFilter1.SelectLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees,null);
            };

            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalied Number!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }

        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrDriverLicensesInfoWithFilter1.txtLicenseIDfocus();
        }
    }
}
