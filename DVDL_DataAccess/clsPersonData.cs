using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DVDL_DataAccess;
using System.Data;
using System.Web;
using System.Runtime.CompilerServices;

namespace DVDL_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonByID(int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string
            NationalNo, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationlityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from People where PersonID =@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {


                    isFound = true;
                   
                    FirstName = (string)reader["FirstName"];
                  
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    NationlityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
               
                reader.Close();

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static bool GetPersonByNationalNo(string NationalNo,ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationlityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Select * from People where NationalNo =@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    isFound = true;
                    PersonID = (int)reader["PersonID"];

                    FirstName = (string)reader["FirstName"];

                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];

                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

                    NationlityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
            
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static int AddNewPerson( string FirstName,string SecondName, string ThirdName, string LastName,
         string NationalNo, DateTime DateOfBirth,short Gendor, string Address,string Phone,string Email,int NationlityCountryID,string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"
           INSERT INTO [dbo].[people]
           (
            [FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
            ,[NationalNo]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
            VALUES
           (
             @FirstName,
              @SecondName,@ThirdName,@LastName,@NationalNo,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName",FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if(ThirdName != "" && ThirdName !=null)
            command.Parameters.AddWithValue("@ThirdName",ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName",System.DBNull.Value);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "" && Email != null)
            command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationlityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int inserted))
                    {
                    PersonID = inserted;
                }

            }
            catch (Exception ex )
            {

            }
            finally { connection.Close(); }

            return PersonID;


        }

        public static bool UpdatePerson(int ID, string FirstName,string SecondName,string ThirdName,string LastName,string NationalNo,
            DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email, int NationlityCountryID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
      //      string query = @"
      //  UPDATE [dbo].[peole]
      //  SET
      // [NationalNo] = @NationalNo
      //,[FirstName] = @FirstName
      //,[SecondName] = @SecondName
      //,[ThirdName] = @ThirdName
      //,[LastName] = @LastName
      //,[DateOfBirth] = @DateOfBirth
      //,[Gendor] = @Gendor
      //,[Address] = @Address
      //,[Phone] = @Phone
      //,[Email] = @Email
      //,[NationalityCountryID] = @NationalityCountryID
      //,[ImagePath] = @ImagePath
      // WHERE PersonID = @PersonID";
         string query =   @"Update People
              set
               FirstName = @FirstName,
SecondName =@SecondName,
ThirdName=@ThirdName,
LastName =@LastName,
NationalNo=@NationalNo,
DateOfBirth=@DateOfBirth,
Gendor =@Gendor,
Address =@Address,
Phone =@Phone,
Email =@Email,
NationalityCountryID =@NationalityCountryID,
ImagePath =@ImagePath
Where PersonID = @PersonID
";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue ("PersonID", ID);
           
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationlityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath",System.DBNull.Value);  

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeletePerson(int PersonID)
        {
            int  RowsAffected  = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "Delete from People where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
               
               RowsAffected= command.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 

            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        public static DataTable GetAllPerson()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName,
                               People.LastName, People.DateOfBirth, People.Gendor,
                              case
                              when people.Gendor = 0 then 'Male'
                               else 'Female'

                                 end as GendorCaption , 
                              People.Address, People.Phone, People.Email, People.NationalityCountryID,Countries.CountryName, 
                              People.ImagePath 
                               FROM     People INNER JOIN
                                   Countries ON People.NationalityCountryID = Countries.CountryID
                               order by People.FirstName
                                 ";
            SqlCommand command = new SqlCommand(query, connection);
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
            finally
            { connection.Close(); }
            return dataTable;
        }

        public static bool IsExistPerson(int PersonID)
        {
            bool isfound=false; 
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select Found=1 from peole where PersonID=@PersonID";
            SqlCommand command1 = new SqlCommand(query, connection);
            command1.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command1.ExecuteReader();
                isfound = reader.HasRows;

                reader.Close();
            }
            catch(Exception ex)
            {
                isfound = false;
            }

            finally { connection.Close(); }
            return isfound;
        }

        public static bool IsExistPerson(string NationalNo)
        {
            bool isfound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.StringConnaction);
            string query = "select Found=1 from People where NationalNo=@NationalNo";
            SqlCommand command1 = new SqlCommand(query, connection);
            command1.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command1.ExecuteReader();
                isfound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isfound = false;
            }

            finally { connection.Close(); }
            return isfound;
        }
    }

   
}
