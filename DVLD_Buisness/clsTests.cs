using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVDL_DataAccess;

namespace DVLD_Buisness
{
    public class clsTests
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public clsTestAppointments TestAppointmentInfo { get; set; }
        public bool TestResult {  get; set; }
        public string Nots {  get; set; }
        public int CreatedUserByID {  get; set; }

        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Nots = "";
            this.CreatedUserByID = -1;

            _Mode = enMode.AddNew;
        }

        public clsTests( int TestID, int TestAppointmentID, bool TestResult, string Nots, int CreatedUserByID)
        {
            
           this.TestID = TestID;
           this.TestAppointmentID = TestAppointmentID;
           this.TestAppointmentInfo = clsTestAppointments.Find(TestAppointmentID);
           this.TestResult = TestResult;
           this.Nots = Nots;
           this.CreatedUserByID = CreatedUserByID;

            _Mode = enMode.Update;
        }

        public static clsTests Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Nots = "";
            int CreatedUserByID = -1;

            bool isFound = clsTestsData.GetTestInfoByID( TestID,ref TestAppointmentID,ref TestResult, ref Nots, ref CreatedUserByID);
            if (isFound)
            {
                return new clsTests(TestID,TestAppointmentID, TestResult, Nots, CreatedUserByID);
            }
            else
                return null;
        }

        public static clsTests FindLastTestByPersonAndTestTypeAndLicenseClass(int PersonID,  int LicenseClass ,clsTestTypes.enTestType TestTypeID)
        {
            int TestAppointmentID = -1,TestID = -1;
            bool TestResult = false;
            string Nots = "";
            int CreatedUserByID = -1;

            bool isfound = clsTestsData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, (int)TestTypeID, LicenseClass
                ,ref TestID,ref TestAppointmentID,ref TestResult,ref Nots,ref CreatedUserByID);
            if(isfound)
                return new clsTests(TestID, TestAppointmentID, TestResult, Nots,CreatedUserByID);
            else
                return null;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID,this.TestResult,this.Nots,this.CreatedUserByID);
            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(this.TestID,this.TestAppointmentID, this.TestResult,this.Nots, this.CreatedUserByID);
        }

        public bool Dalete(int TestID)
        {
            return clsTestsData.Delete(TestID);
        }

        public DataTable GetAllTests()
        {
            return clsTestsData.GetAllTests();
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = enMode.AddNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                  return  _UpdateTest();
                  
            }
            return false;
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return  GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
