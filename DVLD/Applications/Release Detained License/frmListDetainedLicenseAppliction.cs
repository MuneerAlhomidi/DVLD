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
    public partial class frmListDetainedLicenseAppliction : Form
    {
        
        private DataTable _dtDetainedLicense;

        public frmListDetainedLicenseAppliction()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenseAppliction_Load(object sender, EventArgs e)
        {
            _dtDetainedLicense = clsDetainedLicenses.GetAllDetainedLicenses();
            dgvDetainedLicense.DataSource = _dtDetainedLicense;

            lbRecord.Text = dgvDetainedLicense.Rows.Count.ToString();

            if(dgvDetainedLicense.Rows.Count > 0)
            {
                dgvDetainedLicense.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicense.Columns[0].Width = 100;

                dgvDetainedLicense.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicense.Columns[1].Width = 100;

                dgvDetainedLicense.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicense.Columns[2].Width = 150;

                dgvDetainedLicense.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicense.Columns[3].Width = 120;

                dgvDetainedLicense.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicense.Columns[4].Width = 100;

                dgvDetainedLicense.Columns[5].HeaderText = "Released Date";
                dgvDetainedLicense.Columns[5].Width = 150;

                dgvDetainedLicense.Columns[6].HeaderText = "N.No";
                dgvDetainedLicense.Columns[6].Width = 100;

                dgvDetainedLicense.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicense.Columns[7].Width = 250;

                dgvDetainedLicense.Columns[8].HeaderText = "Released App ID";
                dgvDetainedLicense.Columns[8].Width = 170;

            }
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsRelease.Visible = true;
                cbIsRelease.Focus();
                cbIsRelease.SelectedIndex = 0;

            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                if (cbFilterBy.Text == "None")
                {
                    
                    txtFilterValue.Visible = false;
                    cbIsRelease.Visible = false;
                    cbFilterBy.Focus();
                }
                else
                    txtFilterValue.Visible = true;
                cbIsRelease.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilertColumn = "";
            
            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    FilertColumn = "DetainID";
                    break;
                case "Is Released":
                    FilertColumn = "IsReleased";
                    break;
                case "National No":
                    FilertColumn = "NationalNo" ;
                    break;
                case "Full Name":
                    FilertColumn = "FullName";
                    break;
                case "Released Application ID":
                    FilertColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilertColumn = "None";
                    break;

            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtDetainedLicense.DefaultView.RowFilter = "";
                lbRecord.Text = _dtDetainedLicense.Rows.Count.ToString();
                return;
            }
            if (FilertColumn == "DetainID" || FilertColumn == "ReleaseApplicationID")
            
                _dtDetainedLicense.DefaultView.RowFilter = string.Format("[{0}] = {1} ", FilertColumn, txtFilterValue.Text.Trim());
            else 
                _dtDetainedLicense.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'",FilertColumn, txtFilterValue.Text.Trim());

                lbRecord.Text = _dtDetainedLicense.Rows.Count.ToString();
            
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text.Trim() == "DetainID " || cbFilterBy.Text.Trim() == "ReleaseApplicationID")
            e.Handled = !char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsRelease.Text;
            switch (FilterValue)
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
            {
                _dtDetainedLicense.DefaultView.RowFilter = "";
                _dtDetainedLicense.Rows.Count.ToString();
                return;
            }

            _dtDetainedLicense.DefaultView.RowFilter = string.Format("[{0}] = {1} ",FilterColumn,FilterValue);
            _dtDetainedLicense.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReleased_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicenseApplication frm = new frmReleasedDetainLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenseAppliction_Load(null,null);
        }

        private void btnDetained_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenseAppliction_Load(null, null);
        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void fToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void PersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
            frm.ShowDialog();
        }

        private void releasedDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicenseApplication frm = new frmReleasedDetainLicenseApplication((int)dgvDetainedLicense.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
