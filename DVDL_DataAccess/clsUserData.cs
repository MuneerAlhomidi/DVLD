using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVDL_DataAccess
{
    public class clsUserData
    {
       // private clsErrorEventLog log;
        public static bool GetUserByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool  isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select * from Users Where UserID =@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                clsErrorEventLog.LogExseptionsToLogerViewr("Get UserBy ID", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetUserByUserName(string UserName,ref int UserID,ref int PersonID,ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string Query = "Select * from Users Where UserName = @UserName";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@UserName",UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;

                clsErrorEventLog.LogExseptionsToLogerViewr("Get User By UserName",EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetUserByPesonID(  int PersonID,ref string UserName, ref int UserID, ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select * from Users Where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }
            }
            catch(Exception ex)
            {
                isFound=false;
                clsErrorEventLog.LogExseptionsToLogerViewr("Get User By PesonID", EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool GetUserByUserNameAndPassword(string UserName,string Paswword, ref int UserID,ref int PersonID,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from Users Where UserName =@UserName and Password =@Password";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Paswword);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Paswword = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                IsFound = false;

                clsErrorEventLog.LogExseptionsToLogerViewr("Get User By UserName And Password",EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Insert into Users (PersonID,UserName,Password,IsActive)
                           values
                             (@PersonID,@UserName,@Password,@IsActive);
                              SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(@query,connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int Inserted))
                {
                   UserID = Inserted;
                }
            }
            catch (Exception ex)
            {
                
                    clsErrorEventLog.LogExseptionsToLogerViewr("Add New User",EventLogEntryType.Error);
                
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, int PersonID,string UserName, string Password, bool IsActive)
        {
            int RosAfficted = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string Query = @"
                           UPDATE [dbo].[Users]
                           SET [PersonID] = @PersonID
                              ,[UserName] = @UserName
                              ,[Password] = @Password
                              ,[IsActive] = @IsActive
                           WHERE UserID =@UserID";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                RosAfficted = command.ExecuteNonQuery();
            }
            catch
            {
                clsErrorEventLog.LogExseptionsToLogerViewr("Update User", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (RosAfficted > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            int  RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Delete from Users where UserID =@UserID";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserID",UserID);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                clsErrorEventLog.LogExseptionsToLogerViewr("Delete User",EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        public static DataTable GetAllUser()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"SELECT Users.UserID, Users.PersonID, 
                           FullName = People.FirstName + ' ' +People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,Users.UserName, Users.IsActive
                        FROM     Users INNER JOIN
                                 People ON Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorEventLog.LogExseptionsToLogerViewr("Get AllUser", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }

        public static bool IsExistUser(int UserID)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select Found=1 from User Where UserID =@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isfound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;

                clsErrorEventLog.LogExseptionsToLogerViewr("Is Exist User",EventLogEntryType.Error);
            }
            finally
            { connection.Close(); }
            return isfound;
        }

        public static bool IsExistUser(string UserName)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select Found=1 from Users Where UserName =@UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isfound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;

                clsErrorEventLog.LogExseptionsToLogerViewr ("Is Exist User", EventLogEntryType.Error);
            }
            finally
            { connection.Close(); }
            return isfound;
        }

        public static bool IsExistUserForPersonID(int PersonID)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select Found=1 from Users Where PersonID =@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isfound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;

                clsErrorEventLog.LogExseptionsToLogerViewr("IsExist User For PersonID",EventLogEntryType.Error);
            }
            finally
            { connection.Close(); }
            return isfound;
        }

        public static bool ChangePassword(int UserID, string Password)
        {
            int RewAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"Update Users
                            set UserID =@UserID,
                                Password=@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                RewAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                clsErrorEventLog.LogExseptionsToLogerViewr("Change Password", EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (RewAffected > 0);
        }
    }
}
