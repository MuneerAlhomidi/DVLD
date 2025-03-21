using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVDL_DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationByID(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate,ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastApplicationDate,ref float PaidFees,ref int CreatedUserByID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Applications WHERE ApplicationID =@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                   // ApplicationID = (int)reader["ApplicationID"];
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastApplicationDate = (DateTime)reader["LastStatusDate"];
                    PaidFees =Convert.ToSingle( reader["PaidFees"]);
                    CreatedUserByID = (int)reader["CreatedByUserID"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            { connection.Close(); }
            return isFound;
        }

        public static int AddNewApplication( int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastApplicationDate,float PaidFees, int CrestedUserByID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastApplicationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CrestedUserByID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int Inserted))
                {
                    ApplicationID = Inserted;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID,int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastApplicationDate, float PaidFees, int CrestedUserByID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Applications 
                             set ApplicantPersonID =@ApplicantPersonID,
                                 ApplicationDate=@ApplicationDate,
                                 ApplicationTypeID =@ApplicationTypeID,
                                 ApplicationStatus =@ApplicationStatus,
                                 LastApplicationDate =@LastApplicationDate,
                                 PaidFees =@PaidFees,
                                 CrestedUserByID =@CrestedUserByID
                                 WHERE ApplicationID =@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastApplicationDate", LastApplicationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CrestedUserByID", CrestedUserByID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeteteAplication(int ApplicationID)
        {
            int rowsAffcted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Delete Applications where ApplicationID =@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffcted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }

            return (rowsAffcted > 0);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
           bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction) ;
            string query = "SELECT Found=1 From Applications Where ApplicationID=@ApplicationID";
            SqlCommand commed = new SqlCommand(query, connection);
            commed.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = commed.ExecuteReader();
                isfound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }
            finally
            { connection.Close(); }
            return isfound;
        }

        public static bool DoesPersonHasActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) !=-1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"SELECT IsActiveApplication=ApplicationID FROM Application WHERE ApplicantPersonID=@ApplicantPersonID
                            and 
                              ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1 ";
            SqlCommand commed = new SqlCommand( query, connection);
            commed.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            commed.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
               object result = commed.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(),out int AppID) )
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            { 
                connection.Close();
            }
            return ActiveApplicationID;

        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID,int LicenseClassID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"	SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";
            SqlCommand commed = new SqlCommand(query, connection);
            commed.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            commed.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            commed.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = commed.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(),out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID,short NewStatus)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Applications 
                             set ApplicationStatus = @NewStatus,
                                 LastStatusDate = @LastStatusDate
                              where ApplicationID = @ApplicationID";
            SqlCommand commed = new SqlCommand( query, connection);
            commed.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            commed.Parameters.AddWithValue("@NewStatus", NewStatus);
            commed.Parameters.AddWithValue("@LastStatusDate",DateTime.Now);

            try
            {
                connection.Open();
                rowsAffected = commed.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close() ;
            }
            return (rowsAffected > 0);
        }
    }
}
