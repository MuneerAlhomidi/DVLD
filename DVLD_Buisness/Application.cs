using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVDL_DataAccess;

namespace DVLD_Buisness
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Canceled = 2, Completed = 3 };

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 8
        };

        public int ApplicationID { get; set; }
        public int ApplicationPersonID { get; set; }
        public string ApplicationFullName
        {
            get
            {
                return clsPerson.Find(ApplicationPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int AppliactionTypeID { get; set; }
        public clsApplicationTypes AppliactionTypeInfo;
        public enApplicationStatus AppliactionStatus { get; set; }
        public string StatusNext
        {
            get
            {
                switch (AppliactionStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Canceled:
                        return "Canceled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastDateStatus { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID {  get; set; }
        public clsUsers CreatedByUserInfo;

        public clsApplication( int applicationID, int applicationPersonID, DateTime applicationDate, int appliactionTypeID, 
             enApplicationStatus appliactionStatus, 
            DateTime lastDateStatus, float paidFees, int createdByUserID)
        {
           
            this.ApplicationID = applicationID;
            this.ApplicationPersonID = applicationPersonID;
            this.ApplicationDate = applicationDate;
            this.AppliactionTypeID = appliactionTypeID;
            this.AppliactionTypeInfo = clsApplicationTypes.Find(appliactionTypeID);
            this.AppliactionStatus = appliactionStatus;
            this.LastDateStatus = lastDateStatus;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.FindByUserID(createdByUserID);

            Mode = enMode.Update;
        }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationPersonID = -1;
            this.ApplicationDate=DateTime.Now;
            this.AppliactionTypeID = -1;
            this.AppliactionStatus = enApplicationStatus.New;
            this.LastDateStatus= DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewApplication()
        {
           this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationPersonID,this.ApplicationDate,this.AppliactionTypeID,
              (byte) this.AppliactionStatus,this.LastDateStatus,this.PaidFees,this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate, this.AppliactionTypeID,
              (byte)this.AppliactionStatus, this.LastDateStatus, this.PaidFees, this.CreatedByUserID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicationPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int AppliactionTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastDateStatus = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;

            bool isFound = clsApplicationData.GetApplicationByID(
                                         ApplicationID,ref ApplicationPersonID,
                                         ref ApplicationDate, ref AppliactionTypeID,
                                         ref ApplicationStatus,  ref LastDateStatus, 
                                         ref PaidFees, ref CreatedByUserID);
            if (isFound)
            
                return new clsApplication(ApplicationID, ApplicationPersonID,
                                          ApplicationDate, AppliactionTypeID, 
                                          (enApplicationStatus)ApplicationStatus, LastDateStatus,
                                          PaidFees, CreatedByUserID);
            else
                return null;    

        }

        public bool DeleteApplication()
        {
            return clsApplicationData.DeteteAplication(this.ApplicationID);
        }

        public bool IsExistApplication(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHasActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHasActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHasActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHasActiveApplication(this.ApplicationPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicationPersonID,clsApplication.enApplicationType ApplicationTypeID,int LicensClass)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass( ApplicationPersonID,(int)ApplicationTypeID, LicensClass);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplet()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public  int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicationPersonID, ApplicationTypeID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case  enMode.AddNew:
                        if(_AddNewApplication())
                        {
                        Mode = enMode.Update;
                        return true;
                        }
                        else
                        { 
                        return false;
                        }
                case enMode.Update:
                    return _UpdateApplication();

            }
            return false;
        }

        
    }
}
