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
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID =-1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.visionTest;
        private int _AppointmentID = -1;
      
        public frmScheduleTest(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestTypeID,int  AppointmentID =-1)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrrSchduleTest1.TestTypeID = _TestTypeID;
            ctrrSchduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }
    }
}
