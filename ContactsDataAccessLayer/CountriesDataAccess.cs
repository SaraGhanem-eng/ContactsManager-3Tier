using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace ContactsDataAccessLayer
{
    public class clsCountriesDataAccess
    {
        static string ConnectionString = clsDataAccessSettings.ConnectionString;


        public static bool FindCountryByID(int CountryID, ref string CountryName ,ref string Code ,ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    CountryID = (int)reader["CountryID"];
                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                        Code = (string)reader["Code"];
                    else
                        Code = "";


                    if (reader["PhoneCode"] != DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                    else
                        PhoneCode = "";

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            finally // it will be executed rater than try happens of catch .
            {
                connection.Close();
            }


            return isFound;

        }

        public static bool FindCountryByCountryName(string CountryName, ref int CountryID , ref string Code , ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    CountryID = (int)reader["CountryID"];
                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                        Code = (string)reader["Code"];
                    else
                        Code = "";


                    if (reader["PhoneCode"] != DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                    else
                        PhoneCode = "";

                }

                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;

            }
            finally // it will be executed rater than try happens of catch .
            {
                connection.Close();
            }


            return isFound;

        }
        
        public static int AddNewCountry(string CountryName , string Code , string PhoneCode)
        {

            int CountryID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"Insert into Countries (CountryName , Code , PhoneCode ) values
            (@CountryName , @Code , @PhoneCode);
            Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            if (Code != "")
                command.Parameters.AddWithValue("@Code", Code);
            else
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);



            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;

                }

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return CountryID;

        }

        public static bool UpdateCountry (int CountryID, string CountryName , string Code , string PhoneCode )
        {

            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"UPDATE Countries SET CountryName = @CountryName , Code = @Code , PhoneCode = @PhoneCode
                  WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            if (Code != "")
                command.Parameters.AddWithValue("@Code", Code);
            else
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);



            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);


            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    IsUpdated = true;
                }


            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;

        }

        public static bool DeleteCountry(int CountryID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"Delete from Countries Where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllCountries()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select * from Countries ";
            SqlCommand command = new SqlCommand(Query, connection);


            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return DT;
        }

        public static bool IsCountryExistByCountryID(int CountryID)
        {
            bool IsFound = false;


            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select Found =1 from Countries Where CountryID = @CountryID ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool IsCountryExistByCountryName(string CountryName)
        {
            bool IsFound = false;


            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select Found =1 from Countries Where CountryName = @CountryName ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


    }
}
