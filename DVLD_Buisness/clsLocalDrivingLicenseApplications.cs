using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Xml.Linq;
using DVDL_DataAccess;
using static System.Net.Mime.MediaTypeNames;
using static DVLD_Buisness.clsTestTypes;

namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplications:clsApplication
    {
        public enum enMode { AddNew=0, Update=1 }
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set;}
        public string PersonFullName
        {
            get { return base.ApplicationFullName; }
        }
        public int LicenseClassID {  get; set;}

       public clsLicensClasses InfoLicesnseClass;

        public clsLocalDrivingLicenseApplications()
        {
            this.LicenseClassID = -1;
            this.LocalDrivingLicenseApplicationID = -1;

            Mode = enMode.AddNew;
        }
        public clsLocalDrivingLicenseApplications(int localDrivingLicenseApplicationID, int licenseClassID,int ApplicationID,int ApplicationPersonID,
           DateTime ApplicationDate, int appliactionTypeID, enApplicationStatus appliactionStatus,
           DateTime lastDateStatus, float paidFees, int createdByUserID)
        {
            
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
          
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID = ApplicationPersonID;
            this.ApplicationDate = ApplicationDate;
            this.AppliactionTypeID = (int)appliactionTypeID;
            this.AppliactionStatus = appliactionStatus;
            this.LastDateStatus = lastDateStatus;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.LicenseClassID = licenseClassID;
            this.InfoLicesnseClass = clsLicensClasses.Find(licenseClassID);

            Mode = enMode.Update;
        }

        private bool _AddNewlocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,ApplicationID, LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplications FindLocalDrivingLicenseApplicationID(int localDrivingLicenseApplicationID)
        {
            int licenseClassID = -1, ApplicationID = -1;
           
            bool isfound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsID(localDrivingLicenseApplicationID, ref licenseClassID, ref ApplicationID);
            if(isfound)
            {
                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplications(localDrivingLicenseApplicationID, licenseClassID, ApplicationID, application.ApplicationPersonID,
                                               application.ApplicationDate, application.AppliactionTypeID, (enApplicationStatus)application.AppliactionStatus,
                                               application.LastDateStatus, application.PaidFees, application.CreatedByUserID);
            }   
            else
            { return null; }
        }

        public static clsLocalDrivingLicenseApplications FindByApplicationID(int ApplicationID)
        {
            int localDrivingLicenseApplicationID=-1, licenseClassID = -1;

            bool isfound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsInfoForApplicationID(ApplicationID, ref localDrivingLicenseApplicationID, ref licenseClassID);
             if( isfound)
            {
                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplications(localDrivingLicenseApplicationID, licenseClassID, ApplicationID, application.ApplicationPersonID,
                                               application.ApplicationDate, application.AppliactionTypeID, (enApplicationStatus)application.AppliactionStatus,
                                               application.LastDateStatus, application.PaidFees, application.CreatedByUserID);
            } 
             else { return null; }
        }

        public  bool Delete()
        {

            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted =clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.DeleteApplication();
            return IsBaseApplicationDeleted;
        }

        public static DataTable GetAllLocalDrivingLicenseApplicationsData()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();
        }

        public  bool DoesPassTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DosePassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }


        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DosePassTestType(LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }

        public  bool DoesAttendTestType(clsTestTypes. enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }

        public bool Save()
        {
            if(!base.Save())
                return false;
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewlocalDrivingLicenseApplications())
                    {
                       Mode= enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplications();
            }
            return false;
        }

        public  bool TotalTrialsPerTest(clsTestTypes. enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public  bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public  clsTests GetLastTestPerTestType(clsTestTypes. enTestType TestTypeID)
        {
            return clsTests.FindLastTestByPersonAndTestTypeAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID, TestTypeID);
        }

        public  byte GetPassedTestCount()
        {
            return clsTests.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTest(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public  bool PassedAllTest()
        {
            return clsTests.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicationPersonID);
            if(Driver == null)
            {
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicationPersonID;
                Driver.CreatedByUserID = this.CreatedByUserID;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    return -1;
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationPersonID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.InfoLicesnseClass.DefaultValidityLength) ;
            License.Notes = Notes;
            License.PaidFees = this.InfoLicesnseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (Driver.Save())
            {
                this.SetComplet();
                return License.LicenseID;
            }
            else
                return -1;


        }

        public bool IsLicenseIssue()
        {
            return (GetActiveLicensID() != -1);
        }

        public int GetActiveLicensID()
        {
            return clsLicense.GetActiveLicenseByPersonID(this.ApplicationPersonID,this.LicenseClassID);
        }
    }
}
