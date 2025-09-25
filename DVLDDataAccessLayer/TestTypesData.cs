using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestTypesData
    {

            public static DataTable ListAllTestTypes()
            {
                DataTable dt = new DataTable();
                string query = "Select * from TestTypes";
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

            public static bool GetTestTypeByID(int ID, ref string Title,ref string description, ref decimal Fees)
            {
                bool found = false;
                string query = "Select * from TestTypes where TestTypeID=@ID";
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Title = reader["TestTypeTitle"].ToString();
                        description = reader["TestTypeDescription"].ToString();
                        Fees = Convert.ToDecimal(reader["TestTypeFees"]);
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

            public static bool UpdateTestType(int ID, string Title,string description, decimal Fees)
            {
                int rowsAffected = 0;
                string query = @"
                UPDATE TestTypes SET 
                    TestTypeTitle = @Title,
                    TestTypeFees = @Fees,
                    TestTypeDescription = @Description
                WHERE TestTypeID = @ID";
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@Fees", Fees);
                command.Parameters.AddWithValue("@Description", description);

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

