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
    public class clsUsersData
    {
        public static bool FindUser(string username, string password, ref bool isActive, ref int UserID, ref int PersonID)
        {
            bool userFound = false;
            string query = "SELECT UserID, IsActive ,PersonID FROM Users WHERE Username = @Username AND Password = @Password";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userFound = true;
                    isActive = Convert.ToBoolean(reader["IsActive"]);
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                userFound = false;
            }
            finally
            {
                connection.Close();
            }
            return userFound;
        }

        public static DataTable GetAllUsers()
        {


            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT 
                               Users.UserID,
                               Users.UserName,
                               Users.Password,
                               Users.IsActive,
                               Users.PersonID,
                               People.FirstName,
                               People.SecondName,
                               People.ThirdName,
                               People.LastName
                             FROM Users
                             INNER JOIN People 
                             ON People.PersonID = Users.PersonID;
                           ";
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

        public static bool GetUserByID( int UserID,
       ref string Username, ref string Password,
       ref int PersonID,ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        Username = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : "";
                        Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : "";
                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false;
                        PersonID = reader["PersonID"] != DBNull.Value ? Convert.ToInt32(reader["PersonID"]) : -1;


                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
            }
        }


        public static bool GetUserByPersonID(int PersonID,
    ref string Username, ref string Password,
    ref int UserID, ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        Username = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : "";
                        Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : "";
                        IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false;
                        PersonID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : -1;


                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
            }
        }


        public static int AddNewUser(string UserName,
          string Password, 
          bool IsActive,
          int PersonID)
        {
            int newUserID = -1;

            string query = @"
                INSERT INTO Users 
                (UserName,Password,IsActive,PersonID) 
                VALUES 
                (@UserName, @Password, @IsActive, @PersonID);
                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newUserID = insertedID;
                }
            }
            catch
            {
                newUserID = -1;
            }
            finally
            {
                connection.Close();
            }
            return newUserID;
        }

        public static bool UpdateUser(int UserID, string UserName,
          string Password,bool IsActive)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE Users SET 
                    UserName = @UserName,
                    [Password] = @Password,
                    IsActive = @IsActive
                WHERE UserID = @UserID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

      
        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsUserExistBYPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool ChangePassword(int UserID, string newPassword)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                UPDATE Users SET 
                    [Password] = @Password
                WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
         
            command.Parameters.AddWithValue("@Password", newPassword);
            
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
