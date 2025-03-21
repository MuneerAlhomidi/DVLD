using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsLicense;

namespace DVLD_Buisness
{
    public class clsLicense
    {
        public enum enMode { Addnew = 0, Update = 1 };
        private enMode _Mode = enMode.Addnew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 }
       
        public clsDriver DriverInfo { get; set; }
        public int LicenseID {  get; set; }
        public int ApplicationID {  get; set; }
        public int DriverID {  get; set; }
        public int LicenseClass {  get; set; }
        public clsLicensClasses LicensClassesIfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate {  get; set; }
        public string Notes { get; set; }
        public float PaidFees {  get; set; }
        public bool IsActive {  get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID {  get; set; }
        public clsDetainedLicenses DetainedInfo {  get; set; }
        public bool IsDetained
        {
            get {return clsDetainedLicenses.IsDetainedLicense(this.LicenseID); }
        }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(IssueReason);
            }
        }

        public clsLicense() 
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            

            _Mode = enMode.Addnew;

        }
        public clsLicense( int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PiadFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
           
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PiadFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.DetainedInfo = clsDetainedLicenses.FindByLicenseID(this.LicenseID);
            this.LicensClassesIfo = clsLicensClasses.Find(this.LicenseClass);

            _Mode = enMode.Update;
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DrivingID = -1,  LicenseClass = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PiadFees = 0;
            bool IsActive = false;
            byte IssueReason =1;
            int CreatedByUserID = -1;

            if(clsLicenseData.GetLicenseInfoByID(LicenseID,ref ApplicationID,ref DrivingID,ref LicenseClass,
                                                  ref IssueDate,ref ExpirationDate,ref Notes,ref PiadFees,
                                                   ref IsActive,ref IssueReason,ref CreatedByUserID))
                return new clsLicense(LicenseID,ApplicationID,DrivingID,LicenseClass,
                                       IssueDate,ExpirationDate,Notes,PiadFees,IsActive
                                       ,(enIssueReason)IssueReason,CreatedByUserID);
            else
                return null;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
                                             this.IssueDate, this.ExpirationDate,this.Notes,this.PaidFees,
                                             this.IsActive,(byte)this.IssueReason,this.CreatedByUserID) ;
            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                                             this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                                             this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static DataTable GetAllLicense()
        {
            return clsLicenseData.GetAllLicense();
        }

        public static DataTable GetDriverLicense(int DriverID)
        {
            return clsLicenseData.GetDriverLicense(DriverID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.Addnew:
                    if (_AddNewLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
           return (GetActiveLicenseByPersonID(PersonID, LicenseClassID) !=-1);
        }

        public static int GetActiveLicenseByPersonID(int PersonID,int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseByPersonID(PersonID, LicenseClassID);
        }

        public  bool DeActiveCrruentLicense()
        {
            return clsLicenseData.DeActiveLicense(this.LicenseID);
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch(IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "First Time";


            }
        }

        public Boolean IsLicenseExpird()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication Appliction = new clsApplication();
            Appliction.ApplicationPersonID = this.DriverInfo.PersonID;
            Appliction.ApplicationDate = DateTime.Now;
            Appliction.AppliactionStatus = clsApplication.enApplicationStatus.Completed;
            Appliction.AppliactionTypeID = (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            Appliction.LastDateStatus = DateTime.Now;
            Appliction.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Appliction.CreatedByUserID = CreatedByUserID;

            if(!Appliction.Save())
            {
                return null ;
            }

            clsLicense Licenses = new clsLicense();
            Licenses.ApplicationID = Appliction.ApplicationID;
            Licenses.DriverID = this.DriverID;
            Licenses.LicenseClass = this.LicenseClass;
            Licenses.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicensClassesIfo.DefaultValidityLength;

            Licenses.IssueReason = clsLicense.enIssueReason.Renew;
            Licenses.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            Licenses.Notes = Notes;
            Licenses.PaidFees = this.LicensClassesIfo.ClassFees;
            Licenses.CreatedByUserID = CreatedByUserID;

            if(!Licenses.Save())
            {
                return null ;
            }

            DeActiveCrruentLicense();
            return Licenses ;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication Appliction = new clsApplication();

            Appliction.ApplicationID = this.ApplicationID;
            Appliction.ApplicationPersonID = this.DriverInfo.PersonID;
            Appliction.ApplicationDate = DateTime.Now;
            
            Appliction.AppliactionTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Appliction.AppliactionStatus = clsApplication.enApplicationStatus.Completed;
            Appliction.LastDateStatus = DateTime.Now;
            Appliction.CreatedByUserID = CreatedByUserID;

            if(!Appliction.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();
           
            NewLicense.ApplicationID = Appliction.ApplicationID;
            NewLicense.DriverID = this.DriverInfo.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if(!NewLicense.Save()) { return null; }
            DeActiveCrruentLicense();
            return NewLicense;

        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicenses detainedLicense = new clsDetainedLicenses();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }


        public bool ReleasedDetainLicense(int ReleasedByUserID,ref int ApplicationID)
        {
            clsApplication application = new clsApplication();
            
            application.ApplicationPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.AppliactionTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            application.AppliactionStatus = clsApplication.enApplicationStatus.Completed;
            application.LastDateStatus = DateTime.Now;
            application.CreatedByUserID = this.CreatedByUserID;
            
            if(!application.Save())
            {
                ApplicationID = -1;
                return false;
            }

          ApplicationID = application.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, application.ApplicationID);

        }


    }
}
