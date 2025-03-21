using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsTestsData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult,ref string Notes, ref int CreatedUserByID)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Tests Where TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID",TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    CreatedUserByID = (int)reader["CreatedByUserID"];
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

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID,int TestTypeID,
           ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedUserByID)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    CreatedUserByID = (int)reader["CreatedByUserID"];
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

        public static int AddNewTest( int TestAppointmentID,  bool TestResult,  string Notes,  int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Insert into Tests (TestAppointmentID,TestResult,Notes,CreatedByUserID)
                            Values (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);

                              UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                              select SCOPE_IDENTITY(); ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if(Notes != "" && Notes!= null)
            command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes",System.DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int InsetTest))
                {
                    TestID = InsetTest;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }

        public static bool UpdateTest(int TestID,int TestAppointmentID, bool TestResult, string Nots, int CreatedUserByID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Tests 
                            set TestAppointmentID = @TestAppointmentID,
                                 TestResult = @TestResult,
                                 Notes = @Notes,
                                 CreatedByUserID = @CreatedByUserID 
                             WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if(Nots != "") 
            command.Parameters.AddWithValue("@Notes", Nots);
            else
                command.Parameters.AddWithValue("@Notes",System. DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedUserByID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close() ;
            }
            return (RowsAffected > 0);
        }

        public static bool Delete(int TestID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction) ;
            string query = "Delete From Tests Where TestID = @TestID";

            SqlCommand  command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@TestID",TestID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close() ;
            }
            return (RowsAffected > 0);
        }

        public static DataTable GetAllTests()
        {
            DataTable dtTest = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "SELECT * From Tests order by TestID";
            SqlCommand command = new SqlCommand(query, connection);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dtTest.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dtTest;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction) ;
            string query = @"SELECT TestPassedCount = COUNT(TestTypeID)  
                      FROM     Tests INNER JOIN
                                 TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                      where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestResult=1;
                             ";
            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && byte.TryParse(Result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
                
            }
            catch(Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return PassedTestCount;
        }
    }
}
