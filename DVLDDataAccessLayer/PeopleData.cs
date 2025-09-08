using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsPeopleDataAccess
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
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
            return dt;

        }

        public static bool GetPersonByID(
          int PersonID,
          ref string nationalNo,
          ref string firstName,
          ref string lastName,
          ref string secondName,
          ref string thirdName,
          ref DateTime dateOfBirth,
          ref bool gender,
          ref string address,
          ref string phone,
          ref string email,
          ref string imagePath,
          ref int countryID)
        {
            bool isFound = false;

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        nationalNo = reader["NationalNo"] != DBNull.Value ? (string)reader["NationalNo"] : "";
                        firstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : "";
                        lastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";
                        secondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                        thirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                        dateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : DateTime.MinValue;

                        // careful: DB column is spelled "Gendor"
                        gender = reader["Gendor"] != DBNull.Value ? Convert.ToBoolean(reader["Gendor"]) : false;

                        address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : "";
                        phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : "";
                        email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                        imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";

                        // careful: DB column is "NationalityCountryID"
                        countryID = reader["NationalityCountryID"] != DBNull.Value ? Convert.ToInt32(reader["NationalityCountryID"]) : -1;
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static int AddNewPerson( string nationalNo,
          string firstName,
          string lastName,
          string secondName,
          string thirdName,
          DateTime dateOfBirth,
          bool gender,
          string address,
          string phone,
          string email,
          string imagePath,
          int countryID)
        {
            int newPersonID = -1;

            string query = @"
                INSERT INTO People 
                (NationalNo, FirstName, LastName, SecondName, ThirdName, DateOfBirth, Gendor, Address, Phone, Email, ImagePath, NationalityCountryID) 
                VALUES 
                (@NationalNo, @FirstName, @LastName, @SecondName, @ThirdName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @ImagePath, @NationalityCountryID);
                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@SecondName", secondName);
            command.Parameters.AddWithValue("@ThirdName", thirdName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor",gender);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@ImagePath", imagePath);
            command.Parameters.AddWithValue("@NationalityCountryID", countryID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newPersonID = insertedID;
                }
            }catch
            {
                newPersonID = -1;
            }
            finally
            {
                connection.Close();
            }
            return newPersonID;
        }
    
        public static bool UpdatePerson(int personID,string nationalNo,
          string firstName,string lastName,
          string secondName, string thirdName,
          DateTime dateOfBirth,bool Gender, 
          string Address, string Phone, string Email,
          string ImagePath,int CountryID)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE People SET 
                    NationalNo = @NationalNo,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    SecondName = @SecondName,
                    ThirdName = @ThirdName,
                    DateOfBirth = @DateOfBirth,
                    Gendor = @Gendor,
                    Address = @Address,
                    Phone = @Phone,
                    Email = @Email,
                    ImagePath = @ImagePath,
                    NationalityCountryID = @NationalityCountryID
                WHERE PersonID = @PersonID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@SecondName", secondName);
            command.Parameters.AddWithValue("@ThirdName", thirdName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gendor",Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);

        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM People WHERE PersonID = @PersonID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }


    }
}
