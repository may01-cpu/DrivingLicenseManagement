using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDriverData
    {
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT  *
                            FROM   Drivers_View
                            ORDER BY DriverID";

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
        public static bool GetDriverByID(
          int DriverID, ref int personID,
          ref int createdByUserID,
          ref DateTime createdDate)
        {
            bool isFound = false;

            string query = "SELECT * FROM Driver WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        createdDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue;
                        personID = reader["PersonID"] != DBNull.Value ? Convert.ToInt32(reader["PersonID"]) : -1;
                        createdByUserID = reader["CreatedByUserID"] != DBNull.Value ? Convert.ToInt32(reader["CreatedByUserID"]) : -1;

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

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int newDriverID = -1;

            string query = @"
                INSERT INTO Driver (PersonID, CreatedByUserID, CreatedDate) 
                VALUES 
                (@PersonID, @CreatedByUserID, @CreatedDate);
                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newDriverID = insertedID;
                }
            }
            catch
            {
                newDriverID = -1;
            }
            finally
            {
                connection.Close();
            }
            return newDriverID;
        }



    }
}

