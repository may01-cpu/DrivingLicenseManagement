using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLDDataAccessLayer
{
    public class clsLicenseClassesData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            string query = "Select * from LicenseClasses";
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
    }
}
