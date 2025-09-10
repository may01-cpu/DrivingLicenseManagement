using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsUsersData
    {
        public static bool FindUser(string username, string password,ref bool isActive,ref int UserID,ref int PersonID)
        {
          bool userFound = false;
            string query = "SELECT UserID, IsActive,PersonID FROM Users WHERE Username = @Username AND Password = @Password";
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
                userFound=false;
            }
            finally { 
               connection.Close();
            }
            return userFound;
        }
    }
}
