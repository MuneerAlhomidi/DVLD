using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Data.SqlClient;

namespace DVDL_DataAccess
{
    public class clsLicenseData
    {
        public static bool GetLicenseInfoByID(int LicenseID,ref int ApplicationID,ref int DrivingID,ref int LicenseClass, ref DateTime IssueDate
            ,ref DateTime ExpirationDate, ref string Notes, ref float PiadFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
           bool isfound = false;

           SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Licenses where LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID",LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isfound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DrivingID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";
                    PiadFees =Convert.ToSingle (reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static int AddNewLicense(  int ApplicationID,  int DriverID,  int LicenseClass,  DateTime IssueDate
            ,  DateTime ExpirationDate,  string Notes,  float PaidFees,  bool IsActive,  byte IssueReason,  int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Insert into Licenses
                                    (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,
                                          Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                            VALUES (@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,
                                        @Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                              select SCOPE_IDENTITY()  ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if(Notes != "" && Notes != null)
            command.Parameters.AddWithValue("@Notes",Notes);
            else
                command.Parameters.AddWithValue("@Notes",System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int  dLicenseID))
                {
                    LicenseID = dLicenseID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        public static bool UpdateLicense(int LicensID,int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,
                                         string Notes,float PiadFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            int RrowAfficted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Licenses 
                               set   ApplicationID = @ApplicationID,
                                     DriverID = @DriverID,
                                     LicenseClass =@LicenseClass,
                                     IssueDate =@IssueDate,
                                     ExpirationDate =@ExpirationDate,
                                     Notes =@Notes,
                                     PaidFees =@PaidFees,
                                     IsActive =@IsActive,
                                     IssueReason =@IssueReason,
                                     CreatedByUserID = @CreatedByUserID
                               Where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PiadFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                RrowAfficted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (RrowAfficted >0);
        }
        public static DataTable GetAllLicense()
        {
            DataTable dtLicense = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "SELECT * FROM Licenses order by LicenseID";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dtLicense.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dtLicense;
        }

        public static DataTable GetDriverLicense(int DriverID)
        {
            DataTable dtDriverLicense = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query =  @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@DriverID",DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dtDriverLicense.Load(reader);
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
            return dtDriverLicense;
        }

        public static int GetActiveLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int dLicense))
                {
                    LicenseID = dLicense;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        public static bool DeActiveLicense(int LicenseID)
        {
            int RowsAfficted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Licenses 
                              set 
                                   IsActice =0 
                              where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                RowsAfficted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowsAfficted > 0);
        }
    }
}
