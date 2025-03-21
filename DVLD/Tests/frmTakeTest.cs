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
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        clsTestTypes.enTestType _TestTypeID;

        private clsTests _Test;
        private int _TestID;

        public frmTakeTest(clsTestTypes.enTestType testTypeID, int AppointmentID)
        {
            InitializeComponent();
            _TestTypeID = testTypeID;
            _AppointmentID = AppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctlSchduledTest1.LoadInfo(_AppointmentID);
            ctlSchduledTest1.TestTypeID = _TestTypeID;

            if(ctlSchduledTest1.TestAppointmentID == -1)
            {
                btnSave.Enabled = false;
                
            }
            else
            {
                btnSave.Enabled = true;
            }

            int _TestID = ctlSchduledTest1.TestID;

            if(_TestID != -1)
            {
                _Test = clsTests.Find(_TestID);
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Nots;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;

                
            }
            else
                _Test = new clsTests();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                     
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Nots = txtNotes.Text.Trim();
            _Test.CreatedUserByID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
