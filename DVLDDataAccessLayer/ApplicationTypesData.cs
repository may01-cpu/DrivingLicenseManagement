using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DVLDDataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static DataTable ListAllAppTypes()
        {
           DataTable dt = new DataTable();
            string query ="Select * from ApplicationTypes";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
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
    
        public static bool GetAppTypeByID(int ID,ref string Title,ref decimal Fees)
        {
            bool found = false;
            string query = "Select * from ApplicationTypes where ApplicationTypeID=@ID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fees = Convert.ToDecimal(reader["ApplicationFees"]);
                    found = true;
                }
                reader.Close();
            }
            catch
            {
                found = false;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public static bool UpdateAppType(int ID, string Title,decimal Fees)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE ApplicationTypes SET 
                    ApplicationTypeTitle = @Title,
                    ApplicationFees = @Fees
                WHERE ApplicationTypeID = @ID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees",Fees);
          
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
