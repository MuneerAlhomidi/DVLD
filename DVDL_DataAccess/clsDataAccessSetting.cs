using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public  class clsDataAccessSetting
    {
         //public static string StringConnaction = "Server=.;Database=DVLD; User Id= sa;Password=sa123456;";

        public static string StringConnaction = ConfigurationManager.ConnectionStrings["DVLD.Properties.Settings.Setting"].ConnectionString;

        
    }
}
