using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestData
    {
        public static int AddNewTest(int testAppointment, byte testResult, string notes, int createdBy)
        {
            int newTestID = -1;
            string query = @"
                INSERT INTO Tests
                (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                VALUES 
                (@TestAppointmentID, @TestResult, @Notes, @CreatedBy);
                SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointment);
            command.Parameters.AddWithValue("@TestResult", testResult);
            command.Parameters.AddWithValue("@Notes", notes);
            command.Parameters.AddWithValue("@CreatedBy", createdBy);
            try
            {
                connection.Open();
                newTestID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new test: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return newTestID;
        }
        public static bool UpdateTest(int testID, byte testResult, string notes)
        {
            string query = @"
                UPDATE Tests
                SET TestResult = @TestResult,
                    Notes = @Notes
                WHERE TestID = @TestID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testID);
            command.Parameters.AddWithValue("@TestResult", testResult);
            command.Parameters.AddWithValue("@Notes", notes);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the test: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Tests";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving tests: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;

        }




    }
}
