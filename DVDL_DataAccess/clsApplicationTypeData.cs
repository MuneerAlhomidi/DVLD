using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DVDL_DataAccess
{
    public class clsApplicationTypeData
    {
       public static bool  GetApplicationTypeByID(int ApplicationTypeID,ref string ApplicationTypeTitle,ref float ApplicationFees )
        {
            bool  isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string qurey = "Select * From ApplicationTypes Where ApplicationTypeID =@ApplicationTypeID";
            SqlCommand command = new SqlCommand(qurey,connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound=false;
            }
            finally
            { connection.Close(); }
            return isFound;
        }

        public static int AddNewApplicationTypes(string ApplicationTypeTitle,float ApplicationFees)
        {
            int ApplicationTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"intsert into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                             Values (@ApplicationTypeTitle,@ApplicationFess);
                                  SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int Inserted))
                {
                    ApplicationTypeID = Inserted;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return ApplicationTypeID;
        }

        public static bool UpdateApplicationTypes(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
        {
            int RowAfficted = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update ApplicationTypes 
                            set ApplicationTypeTitle=@ApplicationTypeTitle,ApplicationFees=@ApplicationFees
                              where ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                RowAfficted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return (RowAfficted > 0);
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select * from ApplicationTypes order by ApplicationTypeTitle";
            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return dataTable;
        }
       
    }
}
