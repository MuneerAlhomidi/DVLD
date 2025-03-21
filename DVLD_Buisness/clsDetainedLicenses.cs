using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDetainedLicenses
    {
        public enum enMode { AddNew =0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int DetainID {  get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees {  get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased {  get; set; }
        public DateTime ReleaseDate {  get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsUsers CreatedByUserInfo {  get; set; }
        public clsUsers ReleasedByUserInfo { get; set; }

        public clsDetainedLicenses()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;

        }

        public clsDetainedLicenses(int DetainID,int LicenseID,DateTime DetainDate,float FineFees,int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,int ReleaseApplicationID)
        {
            this.DetainID= DetainID;
            this.LicenseID= LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees= FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsers.FindByPersonID(ReleasedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.ReleaseApplicationID= ReleaseApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicenses(this.LicenseID, this.DetainDate, this.FineFees,
                                                                           this.CreatedByUserID);
            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID,
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID );
        }

        public static clsDetainedLicenses FindByDetainID(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees,
                                                                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,
                                                                ref ReleaseApplicationID))
                return new  clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
             IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }

        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MaxValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicenseData.GetDetainLicenseInfoByLicnseID( LicenseID,ref DetainID, ref DetainDate, ref FineFees,
                                                                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,
                                                                ref ReleaseApplicationID))
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
             IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public  bool ReleaseDetainedLicense(int ReleasedByUserID,int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }

        public static bool IsDetainedLicense(int LicenseID)
        {
            return clsDetainedLicenseData.IsDetainedLicense(LicenseID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDetainedLicense())
                    {
                        Mode = enMode.AddNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateDetainedLicense();
            }
            return false;
        }
    }
}
