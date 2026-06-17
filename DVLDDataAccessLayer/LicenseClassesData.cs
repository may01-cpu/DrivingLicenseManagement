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
        public static bool GetLicenseClassByID(int ID, ref string ClassName, ref string Description, ref short MinAllowedAge, ref short ValidityLength, ref decimal ClassFees)
        {
            bool found = false;
            string query = "Select * from LicenseClasses where LicenseClassID=@ID";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ClassName = reader["ClassName"].ToString();
                    Description = reader["ClassDescription"].ToString();
                    MinAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    ValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToDecimal(reader["ClassFees"]);
                    found = true;
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
            return found;
        }
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
