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

namespace DVLD
{
    public partial class ctlSchduledTest : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.visionTest;
        private int _TestID = -1;

        private int _TestAppointmentID = -1;
        private clsTestAppointments _TestAppointments;

        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;

        public clsTestTypes.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.visionTest:
                        lblTitle.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    case clsTestTypes.enTestType.WrittenTest:
                        lblTitle.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    case clsTestTypes.enTestType.StreetTest:
                        lblTitle.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;

                }
            }
        }

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID; 
            }
        }

        public void LoadInfo(int TestAppointment)
        {
            _TestAppointmentID = TestAppointment;

            _TestAppointments = clsTestAppointments.Find(_TestAppointmentID);
            if( _TestAppointments == null )
            {
                MessageBox.Show("Error, No Appointment with ID = "+_TestAppointmentID.ToString(),"Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

             _TestID = _TestAppointments.TestID;
            _LocalDrivingLicenseApplicationID = _TestAppointments.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplications == null )
            {
                MessageBox.Show("Error, No local Driving License Application With ID = " + _LocalDrivingLicenseApplicationID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _TestAppointments.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplications.InfoLicesnseClass.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplications.PersonFullName;

            lblTitle.Text = _LocalDrivingLicenseApplications.TotalTrialsPerTest(_TestTypeID).ToString();
            lblDate.Text = clsFormat.DateToShort(_TestAppointments.AppointmentDate);

            lblFees.Text = _TestAppointments.PaidFees.ToString();
            lblTestID.Text = (_TestAppointments.TestID ==-1)? "No Taken Yet":_TestAppointments.AppointmentDate.ToString();

            
        }
        public ctlSchduledTest()
        {
            InitializeComponent();
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
