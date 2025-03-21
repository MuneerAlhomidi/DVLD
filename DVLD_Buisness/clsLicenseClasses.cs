using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLicensClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge {  get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicensClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            Mode = enMode.AddNew;
        }

        public clsLicensClasses(int licenseClassID, string ClassName, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, float classFees)
        {
            this.LicenseClassID = licenseClassID;
            this. ClassName = ClassName;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
            Mode = enMode.Update;
           
        }

        public static clsLicensClasses Find(int LicencseClassID)
        {
           
            string ClassName = "", ClassDescription="";
            byte MinimumAllowedAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;

            bool isfound = clsLicenseClassesData.GetLisenseClassByID(LicencseClassID,ref ClassName,ref ClassDescription,
                                                                    ref MinimumAllowedAge,ref DefaultValidityLength,ref ClassFees);
            if (isfound)
            
                return new clsLicensClasses(LicencseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength,ClassFees);
            else
                return null;
        }


        public static clsLicensClasses Find(string ClassName)
        {

            int LicenseClassID = -1;string ClassDescription = "";
            byte MinimumAllowedAge = 18, DefaultValidityLength = 10;
            float ClassFees = 0;

            bool isfound = clsLicenseClassesData.GetLisenseClassByName( ClassName,ref LicenseClassID, ref ClassDescription,
                                                                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees);
            if (isfound)

                return new clsLicensClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }

        private bool _AddNewLicenseClasses()
        {
            this.LicenseClassID = clsLicenseClassesData.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                                                                           this.MinimumAllowedAge,this.DefaultValidityLength,
                                                                           this.ClassFees);
            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClasses()
        {
            return clsLicenseClassesData.UpdateLicenseClass(this.LicenseClassID, this.ClassName,
                                                            this.ClassDescription, this.MinimumAllowedAge,
                                                            this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenseClasses())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    case enMode.Update:
                    return _UpdateLicenseClasses();
            }
            return false;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

    }
}
