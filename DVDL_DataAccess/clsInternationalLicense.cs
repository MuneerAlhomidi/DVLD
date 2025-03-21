using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
            ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpirationDate, ref bool IsActive,ref int CreatedByUserID)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                else
                {
                    isfound = false;
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

        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Select InternationalLicenseID,ApplicationID,
                                     DriverID,IssuedUsingLocalLicenseID,IssueDate,
                                     ExpirationDate,IsActive
                             from InternationalLicenses
                                 order by IsActive , ExpirationDate ";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                         dt.Load(reader);
                    }
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetDriverInternationalLicense(int  driverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"select InternationalLicenseID,ApplicationID,
                                     IssuedUsingLocalLicenseID,IssueDate,
                                     ExpirationDate,IsActive
                             from InternationalLicenses where DriverID = @DriverID
                                   order by ExpirationDate desc";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@DriverID",driverID);

            try
            {
                connection.Open();
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

        public static int AddNewInternationalLicense(  int ApplicationID,  int DriverID,
             int IssuedUsingLocalLicenseID,  DateTime IssueDate,  DateTime ExpirationDate,  bool IsActive,  int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @" 
                               Update InternationalLicenses
                                    sel IsActive = 0 
                               wher DriverID = @DriverID;
                             
                               insert into InternationalLicenses(
                                         DriverID,
                                         IssuedUsingLocalLicenseID,
                                         IssueDate,ExpirationDate,
                                         IsActive,
                                         CreatedUserByID)
                                    values 
                                         (@DriverID,
                                          @IssuedUsingLocalLicenseID,
                                          @IssueDate,@ExpirationDate,
                                          @IsActive,
                                          @CreatedUserByID);
                                    SELECT SCOPE_IDENTITY();  ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int International))
                {
                    InternationalLicenseID= International;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
             int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAfficted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"
                                Update InternationalLicenses 
                            set 
                                ApplicationID = @ApplicationID,
                                DriverID = @DriverID,
                                IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                                IssueDate = @IssueDate,
                                ExpirationDate = @ExpirationDate,
                                IsActive = @IsActive,
                                CreatedByUserID = @CreatedByUserID
                           where InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAfficted = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAfficted > 0);

        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @" select top 1 InternationalLicenseID
                                    FROM InternationalLicenses 
                                    Where DriverID = @DriverID and GetDate() between IssueDate and ExpirationDate
                              order by ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("DriverID",DriverID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(),out int IsActive))
                {
                    InternationalLicenseID = IsActive;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                connection.Close();
            }
            return InternationalLicenseID;
        }
    }
}
