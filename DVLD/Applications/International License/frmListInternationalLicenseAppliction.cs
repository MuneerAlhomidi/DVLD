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
    public partial class frmListInternationalLicenseAppliction : Form
    {

        private DataTable _dtInternationalLicense;

        public frmListInternationalLicenseAppliction()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicenseAppliction_Load(object sender, EventArgs e)
        {
            _dtInternationalLicense = clsInternationalLicense.AllInternationalLicense();
            dgvInternationalLicense.DataSource = _dtInternationalLicense;

            lbRecord.Text = _dtInternationalLicense.Rows.Count.ToString();
            if(dgvInternationalLicense.Rows.Count > 0 )
            {
                dgvInternationalLicense.Columns[0].HeaderText = "Int License ID";
                dgvInternationalLicense.Columns[0].Width = 120;

                dgvInternationalLicense.Columns[1].HeaderText = "Appliction ID";
                dgvInternationalLicense.Columns[1].Width = 120;

                dgvInternationalLicense.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicense.Columns[2].Width = 120;

                dgvInternationalLicense.Columns[3].HeaderText = "l.License ID";
                dgvInternationalLicense.Columns[3].Width = 120;

                dgvInternationalLicense.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicense.Columns[4].Width = 170;

                dgvInternationalLicense.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicense.Columns[5].Width = 170;

                dgvInternationalLicense.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicense.Columns[6].Width = 120;
            }
            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "IsActive")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilter.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilter.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    
                }
                else
                    txtFilterValue.Enabled= true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplictionID";
                        break;
                    };
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";

                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                        break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicense.DefaultView.RowFilter = "";
                lbRecord.Text = dgvInternationalLicense.Rows.Count.ToString();
                return;
            }
            _dtInternationalLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn, txtFilterValue.Text.Trim());

            lbRecord.Text = _dtInternationalLicense.Rows.Count.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsReleased.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if(FilterValue == "All")
            
                _dtInternationalLicense.DefaultView.RowFilter = "";
            else
                _dtInternationalLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,FilterValue);
            lbRecord.Text = _dtInternationalLicense.Rows.Count.ToString();
        }

        private void btnClo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicense.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicense.CurrentRow.Cells[0].Value;
            frmShowDriverInternationLicenseApplications frm = new frmShowDriverInternationLicenseApplications(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void PersonLicenseHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicense.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
            frm.ShowDialog();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();

            frmListInternationalLicenseAppliction_Load(null, null);
        }
    }
}
