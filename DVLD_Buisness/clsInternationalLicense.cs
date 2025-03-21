using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Buisness.clsApplication;

namespace DVLD_Buisness
{
    public class clsInternationalLicense:clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public int DriverID { get; set; }
        public DateTime IssueDate {  get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive {  get; set; }
       

        public clsDriver DriverInfo;

        public clsInternationalLicense()
        {
            this.AppliactionTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
          

            _Mode = enMode.AddNew;
        }

        public clsInternationalLicense(int ApplicationID,int ApplicationPersonID,DateTime ApplictionDate,int ApplicationTypeID,
           enApplicationStatus ApplicationStatus, DateTime LastStatusDate,float PaidFees,int CreatedByUserID,
           int internationalLicenseID, int DriverID ,int issuedUsingLocalLicenseID, DateTime issueDate,
           DateTime expirationDate, bool isActive, int createdByUserID)
        {
            
            base.ApplicationID = ApplicationID;
            base.ApplicationPersonID = ApplicationPersonID;
            base.ApplicationDate = ApplictionDate;
            base.AppliactionTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.AppliactionStatus = ApplicationStatus;
            base.LastDateStatus = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = createdByUserID;

            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.DriverID = DriverID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);


            _Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID,this.DriverID, 
                                                                                                 this.IssuedUsingLocalLicenseID,
                                                                                                 this.IssueDate,this.ExpirationDate,
                                                                                                 this.IsActive,this.CreatedByUserID);  
            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID,this.ApplicationID,
                                                                         this.DriverID,this.IssuedUsingLocalLicenseID,
                                                                         this.IssueDate,this.ExpirationDate, this.IsActive,
                                                                         this.CreatedByUserID);  
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, IssuedUsingLocalLicenseID = -1, DriverID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = true ;
            int CreatedByUserID = -1;

            if(clsInternationalLicenseData.GetInternationalLicenseByID( InternationalLicenseID,ref ApplicationID,
                ref DriverID, ref IssuedUsingLocalLicenseID,ref IssueDate,ref ExpirationDate,ref IsActive,ref CreatedByUserID))
            {
                clsApplication application =  clsApplication.FindBaseApplication(ApplicationID);
               
                return new clsInternationalLicense(application.ApplicationID,application.ApplicationPersonID,application.ApplicationDate,
                    application.AppliactionTypeID,(enApplicationStatus)application.AppliactionStatus,application.LastDateStatus,
                    application.PaidFees,application.CreatedByUserID,InternationalLicenseID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID);
            }
            else
            { return null; }
                
             
        }

        public static DataTable AllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save())
            {
                return false;
            }

            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewInternationalLicense())
                    {
                        _Mode = enMode.AddNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicense();
            }
            return false;
        }

        public static int GetActiveInternationLicenseByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static DataTable GetDriverInternationalLicense(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicense(DriverID);
        }
    }
}
