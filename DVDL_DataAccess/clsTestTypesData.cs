using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DVDL_DataAccess
{
    public class clsTestTypesData
    {
        public static bool GetTestTypesByID(int ID,ref string TestTypeTital, ref string TestTypeDescription,ref float TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select * from TestTypes Where TestTypeID =@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTital = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = Convert.ToSingle( reader["TestTypeFees"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally { connection.Close(); }
            return isFound;
        }

        public static int AddNewTestType(string TestTypeTitle,  string TestTypeDescription, float TestTypeFees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"INSERT INTO [dbo].[TestTypes]
           ([TestTypeTitle]
           ,[TestTypeDescription]
           ,[TestTypeFees])
     VALUES
           (<@TestTypeTitle, nvarchar(100),>
           ,<@TestTypeDescription, nvarchar(500),>
           ,<@TestTypeFees, smallmoney,>);
             select SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            cmd.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            cmd.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (int.TryParse(Result.ToString(), out int Inserted))
                    {
                    TestTypeID = Inserted;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return TestTypeID;
        }

        public static bool Update(int ID,string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int  IsAffected = -1;
            SqlConnection connection = new SqlConnection (clsDataAccessSetting.StringConnaction);
            string query = @"
                              UPDATE [dbo].[TestTypes]
                              SET TestTypeTitle = @TestTypeTitle, 
                                  TestTypeDescription = @TestTypeDescription,
                                  TestTypeFees = @TestTypeFees
                              where  TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue ("@TestTypeID", ID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                IsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                IsAffected = -1;
            }
            finally
            { connection.Close(); }
            return (IsAffected > 0);
        }

        public static DataTable GetAllTestType()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection ( clsDataAccessSetting.StringConnaction);
            string query = "SELECT * FROM TestTypes order by TestTypeTitle ";
            SqlCommand command = new SqlCommand (query, connection);
            try
            {
                connection.Open ();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return dt;
        }


    }
}
