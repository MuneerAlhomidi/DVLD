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
    public partial class frmListLocalDrivindLicenseAppliction : Form
    {
        private DataTable _dgvAllLocalDrivingLicenseApplication;

        public frmListLocalDrivindLicenseAppliction()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivindLicenseAppliction_Load(object sender, EventArgs e)
        {
            _dgvAllLocalDrivingLicenseApplication= clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplicationsData();
            dgvLocalDrivingLicenseApplications.DataSource = _dgvAllLocalDrivingLicenseApplication;

            
            lbRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;

            }
            cbFilterValue.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFliter.Visible = (cbFilterValue.Text != "None");
            if (txtFliter.Visible )
            {
                txtFliter.Visible = true;
                cbFilterValue.Focus();
            }
            
                
        }

        private void txtFliter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterValue.Text)
            {
                case "L.D.L.AppID":
                 FilterColumn = "LocalDrivingLicenseApplicationID";
                break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                   FilterColumn = "None";
                    break;
            }

            if(txtFliter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dgvAllLocalDrivingLicenseApplication.DefaultView.RowFilter = "";
                lbRecords.Text = _dgvAllLocalDrivingLicenseApplication.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            
                _dgvAllLocalDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFliter.Text.Trim());
            
            else
                _dgvAllLocalDrivingLicenseApplication.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFliter.Text.Trim());

            lbRecords.Text = _dgvAllLocalDrivingLicenseApplication.Rows.Count.ToString();
        }

        private void txtFliter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterValue.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar); 
        }

        private void AddNew_Click(object sender, EventArgs e)
        {
            FrmAddNewLocalDrivingLicenseApplication frm = new FrmAddNewLocalDrivingLicenseApplication();
            frm.ShowDialog();

            frmListLocalDrivindLicenseAppliction_Load(null,null);
        }

        private void showApplictionDitelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoaclDrivingLicenseApplicationInfo frm = new frmLoaclDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListLocalDrivindLicenseAppliction_Load(null, null);
        }

        private void _SchduleTest(clsTestTypes.enTestType TestTypeID)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID,TestTypeID);
            frm.ShowDialog();
            frmListLocalDrivindLicenseAppliction_Load(null, null);
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleTest(clsTestTypes.enTestType.visionTest);
        }

        private void editApplictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddNewLocalDrivingLicenseApplication frm = new FrmAddNewLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivindLicenseAppliction_Load(null, null);
        }

        private void deleteApplicatioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you Surs Delete Local Driving License Apllication ","Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LocalDrivingLicenseApplicationID=(int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications =
                clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(LocalDrivingLicenseApplications != null)
            {
               if(LocalDrivingLicenseApplications.Delete())
                {
                    MessageBox.Show("Application Delete successfully ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    frmListLocalDrivindLicenseAppliction_Load(null,null);
                }
               else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sors do you went Cancel this Application? ","Confrm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplication = 
            clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(LocalDrivingLicenseApplication != null)
            {
                if(LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancel Successfully.","Cancelld",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleSteepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SchduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplications LocalDrivingLicensAppliction = clsLocalDrivingLicenseApplications.
                 FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            int TotalPassdTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            bool LicenseExist = LocalDrivingLicensAppliction.IsLicenseIssue();

            issuToolStripMenuItem.Enabled = (TotalPassdTests == 3) && !LicenseExist;

            showLicenseToolStripMenuItem.Enabled = LicenseExist;
            editApplictionToolStripMenuItem.Enabled = LicenseExist && (LocalDrivingLicensAppliction.AppliactionStatus == clsApplication.enApplicationStatus.New);
            deleteApplicatioToolStripMenuItem.Enabled = (LocalDrivingLicensAppliction.AppliactionStatus == clsApplication.enApplicationStatus.New);
            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicensAppliction.AppliactionStatus == clsApplication.enApplicationStatus.New);
            sehToolStripMenuItem.Enabled = !LicenseExist;

            bool PassedVisionTest = LocalDrivingLicensAppliction.DoesPassTestType(clsTestTypes.enTestType.visionTest);
            bool PassedWrittenTes = LocalDrivingLicensAppliction.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicensAppliction.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            sehToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTes || !PassedStreetTest) && (LocalDrivingLicensAppliction.AppliactionStatus == clsApplication.enApplicationStatus.New);

            if(sehToolStripMenuItem.Enabled )
            {
                
                schduleVisionTest.Enabled = !PassedVisionTest;
                scheduleWrittenToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTes;
                scheduleSteepsToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTes && !PassedStreetTest; 
            }
            
        }

        private void issuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueDrivingLicenseFirstTime frm = new frmIssueDrivingLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm. ShowDialog();
            frmListLocalDrivindLicenseAppliction_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

           int LicenseID = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).GetActiveLicensID();
            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
                MessageBox.Show("No,License found! ", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int PersonID = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).ApplicationPersonID;
            if(PersonID != -1)
            {
                frmShowLicensePersonHistory frm = new frmShowLicensePersonHistory(PersonID);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Person Is not Found!","No Found",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
