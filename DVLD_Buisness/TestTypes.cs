using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDL_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestTypes
    {
        public enum enMode { AddNew=0,Update=1};
        public enMode _Mode = enMode.AddNew;
        public enum enTestType { visionTest=1,WrittenTest=2,StreetTest=3};
        public clsTestTypes. enTestType ID {  get; set; }
   
        public string TestTypeTital {  get; set; }
        public string TestTypeDescription {  get; set; }
        public float TestTypeFees {  get; set; }

        public clsTestTypes() 
        {
            this.ID = clsTestTypes.enTestType.visionTest;
            this.TestTypeTital = ""; 
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
            _Mode = enMode.AddNew;
        }

        public clsTestTypes(clsTestTypes.enTestType ID,  string testTypeTital, string testTypeDescription, float testTypeFees)
        {
            this.ID=ID;
            this.TestTypeTital = testTypeTital;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;
            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.ID=(clsTestTypes.enTestType)  clsTestTypesData.AddNewTestType(this.TestTypeTital,this.TestTypeDescription,this.TestTypeFees);
            return (this.TestTypeTital != "");
        }

        private bool _Update()
        {
            return clsTestTypesData.Update((int)this.ID, this.TestTypeTital, this.TestTypeDescription, this.TestTypeFees);
        }

        public static clsTestTypes Find(clsTestTypes.enTestType ID)
        {
            string TestTypeTital = "", TestTypeDescription = "";
            float TestTypeFees = 0;
            bool isFound = clsTestTypesData.GetTestTypesByID((int)ID,ref TestTypeTital,ref TestTypeDescription,ref TestTypeFees);

            if (isFound)
            {
                return new clsTestTypes(ID, TestTypeTital, TestTypeDescription, TestTypeFees);
            }
            else
                return null;
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else { return false; }
                    case enMode.Update:
                   return _Update();
            }
            return false;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestType();
        }
    }
}
