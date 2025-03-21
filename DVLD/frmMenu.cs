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
    public partial class frmMain : Form
    {
        frmLogin  _frmlogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmlogin = frm;
        }

        public void PersonCard()
        {
          
                 
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenagePeople frm = new frmMenagePeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMangeUser frm = new frmMangeUser();
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void manageApplictionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddNewLocalDrivingLicenseApplication frm = new FrmAddNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
           _frmlogin.ShowDialog();
            this.Close();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivindLicenseAppliction frm = new frmListLocalDrivindLicenseAppliction();
            frm.ShowDialog();
        }

        private void tspDrivers_Click(object sender, EventArgs e)
        {
            frmListDriver frm = new frmListDriver();
            frm.ShowDialog();

        }

        private void internationalDriveingLicenseApplicatiomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseAppliction frm = new frmListInternationalLicenseAppliction();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
        }

        private void renewDrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalLicensAppliction frm = new frmRenewLocalLicensAppliction();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenseAppliction frm = new frmListDetainedLicenseAppliction();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicenseApplication frm = new frmReleasedDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmListLocalDrivindLicenseAppliction frm = new frmListLocalDrivindLicenseAppliction();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleasedDetainLicenseApplication frm = new frmReleasedDetainLicenseApplication();
            frm.ShowDialog();
        }
    }

    
}
