using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode _Mode = enMode.AddNew;

        public int PresonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationlityCountryID { get; set; }

        public clsCountry CountryInfo;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }



        public clsPerson(int presonID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo,
            DateTime dateOfBirth, short gender, string address, string phone, string email, int NationlityCountryID, string imagePath)
        {

            this.PresonID = presonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationlityCountryID = NationlityCountryID;
            this.ImagePath = imagePath;
            this.CountryInfo = clsCountry.Find(NationlityCountryID);

            _Mode = enMode.Update;
        }

        public clsPerson()
        {
            this.PresonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationlityCountryID = -1;
            this.ImagePath = "";

            _Mode = enMode.AddNew;

        }

        public static clsPerson Find(int ID)
        {
            string FirstName = "", SecondName = "", ThirdName = "",LastName = "", NationalNo = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int  NationlityCountryID = -1;
            short Gendor = 0;
           

            bool isFound = clsPersonData.GetPersonByID(ID,  ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth,
                ref Gendor, ref Address, ref Phone, ref Email, ref NationlityCountryID, ref ImagePath);
            if(isFound)
                return new clsPerson(ID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationlityCountryID, ImagePath);
            else
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationlityCountryID = -1, PersonID = -1;
            short Gendor = 0;


            bool isFound = clsPersonData.GetPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,  ref DateOfBirth,
                ref Gendor, ref Address, ref Phone, ref Email, ref NationlityCountryID, ref ImagePath);
            if (isFound)
                return new clsPerson( PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationlityCountryID, ImagePath);
            else
                return null;
        }

        private bool _AddNewPerson()
        {
            this.PresonID = clsPersonData.AddNewPerson( this.FirstName,  this.SecondName,this.ThirdName,this.LastName,this.NationalNo, this.DateOfBirth,
                this.Gendor, this.Address,this.Phone, this.Email, this.NationlityCountryID, this.ImagePath);
            return (this.PresonID != -1);
        }

        private bool _UpdatePerson()
        {
            return (clsPersonData.UpdatePerson(this.PresonID, this.FirstName,this.SecondName,this.ThirdName,this.LastName,this.NationalNo, this.DateOfBirth,
                this.Gendor, this.Address, this.Phone, this.Email, this.NationlityCountryID, this.ImagePath));
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewPerson())
                    {
                        
                       _Mode= enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    case enMode.Update:
                   return _UpdatePerson();
            }
            return false;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPerson()
        {
            return clsPersonData.GetAllPerson();
        }

        public static bool isExistPepole(int PersonID) 
        {
            return clsPersonData.IsExistPerson(PersonID);
        }

        public static bool isExistPepole(string NationalNo)
        {
            return clsPersonData.IsExistPerson(NationalNo);
        }
    }

}
