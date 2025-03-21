using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsDriver
    {
        public enum enMode { AddNew =0,Update =1 }
        private enMode _Mode = enMode.AddNew;

        public int DriverID {  get; set; }
        public int PersonID {  get; set; }
        public int CreatedByUserID {  get; set; }
        public clsPerson PersonInfo { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver() 
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        public clsDriver( int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;

            _Mode= enMode.Update;
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            
            if (clsDriverData.GetDriverInfoByDriverID(DriverID,ref PersonID,ref CreatedByUserID,ref CreatedDate))
                return new clsDriver(DriverID,PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public static clsDriver FindByPersonID(int personID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;


            if (clsDriverData.GetDriverInfoByPersonID(personID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, personID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);
            return (DriverID != -1);

        }

        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(DriverID, PersonID, CreatedByUserID);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDriver())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }

        public static DataTable GetLicense(int DriverID)
        {
            return clsLicense.GetDriverLicense(DriverID);
        }
    }
}
