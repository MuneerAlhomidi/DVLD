using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVDL_DataAccess;

namespace DVLD_Buisness
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string CountryName { get; set; }

        clsCountry() 
        {
            this.ID = -1;
            this.CountryName = "";
        }
        clsCountry(int Id, string CountryName)
        {
            this.ID = Id;
            this.CountryName = CountryName;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            if(clsCountryData.GetCountryInfoByID(ID,ref CountryName))
              return new  clsCountry(ID,CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
          int ID = -1;
            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))
                return new clsCountry(ID, CountryName);
            else
                return null;
            
        }

        public static DataTable GetCountryDataTable()
        {
            return clsCountryData.GetAllCountries();
        }

    }
}
