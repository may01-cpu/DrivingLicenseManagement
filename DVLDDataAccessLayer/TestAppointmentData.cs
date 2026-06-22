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
    public class clsTestAppointmentData
    {
        public static bool GetAppointmentInfoByID(
          int TestAppointmentID, ref int DLAppID, ref DateTime AppointmentDate,
          ref decimal paidFees, ref bool isLocked, ref int CreatedByUserID,
          ref int retakeTestAppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DLAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = (decimal)reader["PaidFees"];
                    isLocked = (bool)reader["IsLocked"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    retakeTestAppID = reader["RetakeTestApplicationID"] == DBNull.Value
                                      ? -1
                                      : (int)reader["RetakeTestApplicationID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }



        public static DataTable GetAllTestAppointments(int LocalAppID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                     FROM TestAppointments
                     WHERE LocalDrivingLicenseApplicationID = @LocalAppID
                     AND TestTypeID = @TestTypeID 
                     ORDER BY AppointmentDate";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving test appointments: " + ex.Message);
            }
            finally { connection.Close(); }
            return dt;
        }
        
        public static int AddNewTestAppointment(int TestTypeID, int LocalDLAppID, DateTime AppointDate,
     decimal PaidFees, int CreatedBy, bool isLocked, int retakeTestID)
        {
            int newTestAppointmentID = -1;
            string query = @"
        INSERT INTO TestAppointments
        (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
        VALUES 
        (@TestTypeID, @LocalDLAppID, @AppointDate, @PaidFees, @CreatedBy, @IsLocked, @RetakeTestID);
        SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDLAppID", LocalDLAppID);
            command.Parameters.AddWithValue("@AppointDate", AppointDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            command.Parameters.AddWithValue("@IsLocked", isLocked);
            command.Parameters.AddWithValue("@RetakeTestID",
                retakeTestID == -1 ? (object)DBNull.Value : retakeTestID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    newTestAppointmentID = insertedID;
            }
            catch { newTestAppointmentID = -1; }
            finally { connection.Close(); }
            return newTestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointID, DateTime AppointmentDate)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE TestAppointments SET 
                    AppointmentDate = @AppointmentDate
                WHERE TestAppointmentID = @TestAppointmentID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

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

        public static bool MarkTestAppointmentAsLocked(int TestAppointID)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE TestAppointments SET 
                    IsLocked = 1
                WHERE TestAppointmentID = @TestAppointmentID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointID);
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

