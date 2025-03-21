using DVLD.Classes;
using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Buisness.clsTestTypes;

namespace DVLD
{
    public partial class ctrrSchduleTest : UserControl
    {
        public enum enMode { AddNew =0, Update=1}
        private enMode Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchdule =0, RetakeTestSchdule = 1}
        private enCreationMode CreationMode = enCreationMode.FirstTimeSchdule;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.visionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;

        private clsTestAppointments _TestAppointment;
        public clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;

        public clsTestTypes.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch(_TestTypeID)
                {
                    case clsTestTypes.enTestType.visionTest:
                        {
                            lblTitle.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            lblTitle.Text = "WrittenTest";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            lblTitle.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID =-1)
        {
            if (AppointmentID == -1)
                Mode = enMode.AddNew;
            else
                Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;

            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("Error, Not have Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplications.DoesAttendTestType(_TestTypeID))
                CreationMode = enCreationMode.RetakeTestSchdule;
            else
                CreationMode = enCreationMode.FirstTimeSchdule;

            if(CreationMode == enCreationMode.RetakeTestSchdule)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = " Schdule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Retake Test";
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplications.InfoLicesnseClass.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplications.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseApplications.TotalTrialsPerTest(_TestTypeID).ToString();


            if(Mode == enMode.AddNew)
            {
                dtpTestDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestTypes.Find(_TestTypeID).TestTypeFees.ToString();
                lblRetakeTestAppID.Text = "A/N";

                _TestAppointment = new clsTestAppointments();
            }
            else
            {
                if (_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveAppointmentConstrint())
                return;
            if(!_HandlePrviousTestConstraint())
                return;
            if (!_HandleAppointmentLockedConstrint())
                return;
                
        }

        private bool _HandleAppointmentLockedConstrint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Enabled = true;
                lblUserMessage.Text = "Person Already have appointment with ID = " + _TestAppointmentID.ToString();
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                lblUserMessage.Enabled=false;
            return true;
        }

        private bool _HandleRetakeTestAppointmentTest()
        {
            if(Mode == enMode.AddNew && CreationMode == enCreationMode.RetakeTestSchdule)
            {
                clsApplication application = new clsApplication();

                application.ApplicationID = _TestAppointment.RetakeTestApplicationID;
                application.ApplicationPersonID = _LocalDrivingLicenseApplications.ApplicationPersonID;
                application.ApplicationDate = DateTime.Now;
                application.AppliactionTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.AppliactionStatus = clsApplication.enApplicationStatus.Completed;
                application.LastDateStatus = DateTime.Now;
                application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees ;
                application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if(!application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Error, faild","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = application.ApplicationID;
            }
            return true;  
            
        }

        private bool _HandleActiveAppointmentConstrint()
        {
            if (Mode == enMode.AddNew && clsLocalDrivingLicenseApplications.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            { 
            lblUserMessage.Text = "Person Already Appoinment An Active with ID = "+_TestAppointmentID.ToString();
            lblUserMessage.Enabled = true;
            btnSave.Enabled = false;
            dtpTestDate.Enabled = false;
            return false;
            }
            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (TestTypeID)
            {
                case clsTestTypes. enTestType.visionTest:

                    lblUserMessage.Visible = false;
                    return true;
                case clsTestTypes. enTestType.WrittenTest:
                    if(!_LocalDrivingLicenseApplications.DoesPassTestType(clsTestTypes.enTestType.visionTest))
                    {
                        lblUserMessage.Text = "Cannot Schdule , vision Test Passed First ";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible=true;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled=true;

                    }
                    return true;
                case clsTestTypes.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplications.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = " Cannot Schdule , Written Test Passed First";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled=true;

                    }
                    return true;

            }
            return true;

        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointments.Find(_TestAppointmentID);

            if(_TestAppointment == null)
            {
                MessageBox.Show("Error, No Appoinment with ID = "+_TestAppointmentID,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate= _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if(_TestAppointment.RetakeTestApplicationID ==-1)
            {
                lblRetakeTestAppID.Text = "A/N";
                lblRetakeAppFees.Text = "0";
            }
            else
            {
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = _TestAppointment.PaidFees.ToString();
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;

            }
            return true;
        }
        public ctrrSchduleTest()
        {
            InitializeComponent();
        }

       

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!_HandleRetakeTestAppointmentTest())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (_TestAppointment.Save())
            {
                Mode = enMode.Update;
                MessageBox.Show("Save Data Successfuly", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Save not Successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
