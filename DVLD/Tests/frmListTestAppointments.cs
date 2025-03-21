using DVLD.Properties;
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
using static DVLD_Buisness.clsTestTypes;

namespace DVLD
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtTestAppointments;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.visionTest;
        private int _TestAppointmentID  = -1;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;

        }

        public frmListTestAppointments()
        {
            InitializeComponent();
           
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            _dtTestAppointments = clsTestAppointments.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            ctrDrivingLicenseAplicationInformation1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            dgvLicenseAppointment.DataSource = _dtTestAppointments;
            lbRecords.Text = _dtTestAppointments.Rows.Count.ToString();

            if(dgvLicenseAppointment.Rows.Count >0)
            {
                dgvLicenseAppointment.Columns[0].HeaderText = "TestAppointment ID";
                dgvLicenseAppointment.Columns[0].Width = 200;

                dgvLicenseAppointment.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseAppointment.Columns[1].Width = 200;

                dgvLicenseAppointment.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseAppointment.Columns[2].Width = 100;

                dgvLicenseAppointment.Columns[3].HeaderText = "Is Locked";
                dgvLicenseAppointment.Columns[3].Width = 100;

            }
            
           
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch(_TestType)
            {
                case clsTestTypes. enTestType.visionTest:
                    lblTitle.Text = "Vision Test Appointment";
                    this.Text = lblTitle.Text;
                    pbTestType.Image = Resources.Vision_512;
                    break;
                case clsTestTypes.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointment";
                    this.Text = lblTitle.Text;
                    pbTestType.Image = Resources.Written_Test_512;
                    break;
                case clsTestTypes.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointment";
                    this.Text = lblTitle.Text;
                    pbTestType.Image = Resources.driving_test_512;
                    break;
            }
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (localDrivingLicenseApplications.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an Active Appointment for this test, you cannot add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTests LastTest = localDrivingLicenseApplications.GetLastTestPerTestType(_TestType);
            if(LastTest == null)
            {
                frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestType);
                frm.ShowDialog();
                
                frmListTestAppointments_Load(null, null);
                return;
            }

            if(LastTest.TestResult == true)
            {
                MessageBox.Show("this Person Already passed this Test befro, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm1 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID,_TestType);
            frm1.ShowDialog();
            frmListTestAppointments_Load(null,null);    

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppoinmentID = (int)dgvLicenseAppointment.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(_TestType, AppoinmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointment = (int)dgvLicenseAppointment.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new  frmScheduleTest (_LocalDrivingLicenseApplicationID,_TestType, TestAppointment);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}