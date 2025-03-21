using DVDL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsUsers
    {
        public enum enMpde { AddNew=0, Update=1};
        public enMpde Mode = enMpde.AddNew;

        public int UserID { get; set; }
        public int PersonID {  get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool  IsActiv {  get; set; }

       public clsUsers()
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActiv = false;
            Mode = enMpde.AddNew;
        }

       public clsUsers( int userID, int personID, string userName, string password, bool isActiv)
        {
            
           this. UserID = userID;
           this. PersonID = personID;
           this.PersonInfo = clsPerson.Find(personID);
           this. UserName = userName;
           this.Password = password;
           this.IsActiv = isActiv;
            Mode = enMpde.Update;
        }

        public static clsUsers FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActiv = false;

            bool isFound = clsUserData.GetUserByID(UserID,ref PersonID,ref UserName,ref  Password,ref IsActiv);

            if(isFound)
            
                return new clsUsers(UserID, PersonID, UserName,Password, IsActiv);
            else
                return null;
        }

        public static clsUsers FindByUserName(string UserName)
        {
            int PersonID = -1,UserID =-1;
            string Password = "";
            bool IsActiv = false;

            bool isFound = clsUserData.GetUserByUserName(UserName, ref UserID, ref PersonID, ref Password, ref IsActiv);

            if(isFound) 
                return new clsUsers(UserID, PersonID, UserName, Password, IsActiv);
            else
                return null;   

        }

        public static clsUsers FindByUserNameAndPassword(string UserName, string Password)
        {
            int PersonID = -1, UserID = -1;
           
            bool IsActiv = false;

            bool isFound = clsUserData.GetUserByUserNameAndPassword(UserName, Password,ref UserID,ref PersonID,ref IsActiv);

            if (isFound)
                return new clsUsers(UserID, PersonID, UserName, Password, IsActiv);
            else
                return null;

        }

        public static clsUsers FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            int UserID = -1; bool IsActiv = false;

            bool isFound = clsUserData.GetUserByPesonID(PersonID,ref UserName, ref UserID, ref Password,ref IsActiv);

            if (isFound)
            return new clsUsers(UserID,PersonID, UserName, Password, IsActiv);
            else
                return null;
        }
        private bool _AddNewUser()
        {
            this.Password = clshashing.ComputeHash(this.Password);

          this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName,this. Password,this. IsActiv);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            this.Password = clshashing.ComputeHash(this.Password);

            return (clsUserData.UpdateUser(this.UserID,this.PersonID,this.UserName,this.Password,this.IsActiv));
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMpde.AddNew:
                    if(_AddNewUser())
                    {
                        Mode = enMpde.Update;
                        return true;
                    }
                    else

                    {
                        return false; 
                    }
                case enMpde.Update:
                 return   _UpdateUser();
                
            }
            return false;
        }

        public static bool DeleteUser(int  UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static DataTable GetAllUser()
        {
            return clsUserData.GetAllUser();
        }

        public static bool IsExistUser(int UserID)
        {
            return clsUserData.IsExistUser(UserID);
        }

        public static bool IsExistUserByUserName(string UserName)
        {
            return clsUserData.IsExistUser(UserName);
        }

      
        public static bool IsExistForPersonID(int PersonID)
        {
            return clsUserData.IsExistUserForPersonID(PersonID);
        }
    }
}
