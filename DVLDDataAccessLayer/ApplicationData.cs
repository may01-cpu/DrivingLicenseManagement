using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsApplicationData
    {

        public static DataTable ListAllLocalDLApps()
        {
            DataTable dt = new DataTable();
            string query = "Select * from LocalDrivingLicenseApplications_View";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
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
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }



        public static bool GetApplicationByID(int AppID, ref int PersonID,
            ref DateTime AppDate,
            ref int AppTypeID,
            ref Byte AppStatus, ref DateTime LastStatusDate, ref decimal AppFees, ref int UserID)
        {
            bool isFound = false;
            string query = @"
                SELECT ApplicantPersonID
                      ,ApplicationDate
                      ,ApplicationTypeID
                      ,ApplicationStatus
                      ,LastStatusDate
                      ,PaidFees
                      ,CreatedByUserID
                FROM Applications
                WHERE ApplicationID=@AppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    AppDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    AppTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    AppStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    AppFees = Convert.ToDecimal(reader["PaidFees"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isFound = true;
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

        public static bool GetLocalApplicationByID(int LocalAppID, ref int AppID,
            ref int LicenseClassID)
        {
            bool isFound = false;
            string query = @"
                SELECT ApplicationID
                      ,LicenseClassID
                FROM LocalDrivingLicenseApplications
                WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    AppID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    isFound = true;
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


        public static int GetPassedTests(int LocalAppID)
        {
            int passedTests = -1;
            string query = @"
                SELECT PassedTestCount
                FROM LocalDrivingLicenseApplications_View
                WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    passedTests = Convert.ToInt32(reader["PassedTestCount"]);

                }
                reader.Close();
            }
            catch
            {
                passedTests = -1;
            }
            finally
            {
                connection.Close();
            }
            return passedTests;
        }

        public static int AddApplication(int PersonID, DateTime AppDate, int AppTypeID,
     Byte AppStatus, DateTime LastStatusDate, decimal AppFees, int UserID)
        {
            int newAppID = -1;

            string query = @"
                INSERT INTO Applications
                            (ApplicantPersonID
                            ,ApplicationDate
                            ,ApplicationTypeID
                            ,ApplicationStatus
                            ,LastStatusDate
                            ,PaidFees
                            ,CreatedByUserID)
                VALUES
                      (@PersonID
                      ,@AppDate
                      ,@AppTypeID
                      ,@AppStatus
                      ,@LastDateStatus
                      ,@AppFees
                      ,@UserID)
                           SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@AppDate", AppDate);
            command.Parameters.AddWithValue("@AppTypeID", AppTypeID);
            command.Parameters.AddWithValue("@AppStatus", AppStatus);
            command.Parameters.AddWithValue("@LastDateStatus", LastStatusDate);
            command.Parameters.AddWithValue("@AppFees", AppFees);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newAppID = insertedID;
                }
            }
            catch
            {
                newAppID = -1;
            }
            finally
            {
                connection.Close();
            }
            return newAppID;
        }

        public static int AddLocalApplication(int AppID,
     int LicenseClassID)
        {
            int newLocalAppID = -1;

            string query = @"
               INSERT INTO LocalDrivingLicenseApplications
                           (ApplicationID,LicenseClassID)
                      VALUES
                            (@ApplicationID,@LicenseClassID)
                           SELECT SCOPE_IDENTITY();";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", AppID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    newLocalAppID = insertedID;
                }
            }
            catch
            {
                newLocalAppID = -1;
            }
            finally
            {
                connection.Close();
            }
            return newLocalAppID;
        }


        public static bool IsApplicationExists(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                isFound = Convert.ToInt16(result) == 1;
            }
            catch (Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsApplicationExists(int PersonID, int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found=1 FROM [dbo].[LocalDrivingLicenseFullApplications_View] 
                              WHERE ApplicantPersonID=@PersonID AND LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                isFound = Convert.ToInt16(result) == 1;
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



        public static bool CancelApplication(int AppID)
        {
            bool isCanceled = false;
            string query = @"
                UPDATE Applications
                SET ApplicationStatus=2,LastStatusDate=GETDATE()
                WHERE ApplicationID=@AppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isCanceled = rowsAffected > 0;
            }
            catch
            {
                isCanceled = false;
            }
            finally
            {
                connection.Close();
            }
            return isCanceled;
        }
        public static bool CancelLocalApplication(int LocalAppID)
        {
            bool isCanceled = false;
            string query = @"
                UPDATE A
                SET A.ApplicationStatus=2,A.LastStatusDate=GETDATE()
                FROM Applications A
                INNER JOIN LocalDrivingLicenseApplications LDA ON A.ApplicationID=LDA.ApplicationID
                WHERE LDA.LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isCanceled = rowsAffected > 0;
            }
            catch
            {
                isCanceled = false;
            }
            finally
            {
                connection.Close();
            }
            return isCanceled;
        }



        public static bool UpdateApplication(int ApplicationID, int PersonID, DateTime AppDate,
            int AppTypeID, Byte AppStatus, DateTime LastStatusDate, decimal AppFees)
        {
            bool isUpdated = false;
            string query = @"
                UPDATE Applications
                SET ApplicantPersonID=@PersonID
                   ,ApplicationDate=@AppDate
                   ,ApplicationTypeID=@AppTypeID
                   ,ApplicationStatus=@AppStatus
                   ,LastStatusDate=@LastStatusDate
                   ,PaidFees=@AppFees
                WHERE ApplicationID=@ApplicationID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@AppDate", AppDate);
            command.Parameters.AddWithValue("@AppTypeID", AppTypeID);
            command.Parameters.AddWithValue("@AppStatus", AppStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@AppFees", AppFees);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch
            {
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;
        }

        public static bool UpdateLocalApplication(int LocalAppID, int LicenseClassID)
        {
            bool isUpdated = false;
            string query = @"
                UPDATE LocalDrivingLicenseApplications
                SET LicenseClassID=@LicenseClassID
                WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch
            {
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdated;


        }



        public static bool DeleteApplication(int ApplicationID)
        {
            bool isDeleted = false;
            string query = @"
                DELETE FROM Applications
                WHERE ApplicationID=@ApplicationID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch
            {
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;


        }

        public static bool DeleteLocalApplication(int LocalAppID)
        {
            bool isDeleted = false;
            string query = @"
                DELETE FROM LocalDrivingLicenseApplications
                WHERE LocalDrivingLicenseApplicationID=@LocalAppID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalAppID", LocalAppID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch
            {
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return isDeleted;
        }
    }
}
