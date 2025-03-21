using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVDL_DataAccess
{
    public class clsCountryData
    {
      public static bool GetCountryInfoByID(int  ID, ref string CountryName)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select * from Countries where CountryID =@CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    CountryName = (string)reader["CountryName"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }
            finally
            {
                connection.Close();
            }
            return isfound;
      }
     
        public static bool GetCountryInfoByName(string CountryName, ref int ID)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Countries where CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    ID = (int)reader["CountryID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }
            finally
            {
                connection.Close();
            }
            return isfound;
        }

        public static DataTable GetAllCountries()
        {
           DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Countries order by CountryName";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close() ;
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return dataTable;

        }
    }
}
