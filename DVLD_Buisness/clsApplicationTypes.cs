using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode _Mode = enMode.AddNew;


        public  int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitl { get; set; }
        public float ApplicationFees { get; set; }

        public clsApplicationTypes() 
        {
            this.ApplicationTypeID = -1;
            this.ApplicationFees = 0;
            this.ApplicationTypeTitl = "";

            _Mode = enMode.AddNew;
        }

        public clsApplicationTypes( int applicationTypeID, string applicationTypeTitl, float applicationFees)
        {
            
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitl = applicationTypeTitl;
            this.ApplicationFees = applicationFees;

            _Mode= enMode.Update;
        }

        public static clsApplicationTypes Find(int ID )
        {
            string ApplicationTypeTitel = "";
            float ApplicationFees = 0;

            bool isFound = clsApplicationTypeData.GetApplicationTypeByID( ID,ref ApplicationTypeTitel,ref ApplicationFees);
            if (isFound )
                return new clsApplicationTypes(ID, ApplicationTypeTitel, ApplicationFees);
            else
                return null;

        }

        public bool _AddApplicatoinType()
        {
            this.ApplicationTypeID = (clsApplicationTypeData.AddNewApplicationTypes(ApplicationTypeTitl, ApplicationFees));
            return (this.ApplicationTypeID != -1);
        }

        private bool _UpdateApplication()
        {
            return (clsApplicationTypeData.UpdateApplicationTypes(this.ApplicationTypeID,this.ApplicationTypeTitl,this.ApplicationFees));
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch ( _Mode )
            {
                case enMode.AddNew:
                    if(_AddApplicatoinType())
                    {
                        _Mode = enMode.Update;
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
