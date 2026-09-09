using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;



namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        static string ConnectionString = clsDataAccessSettings.ConnectionString;
        

        public static bool FindContactByID(int ContactID, ref string FirstName , ref string LastName , ref string Phone ,
           ref string Email , ref string Address , ref string ImagePath , ref DateTime DateOfBirth  , ref int CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ContactID = (int)reader["ContactID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    CountryID = (int)reader["CountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];

                    if (reader ["ImagePath"] != DBNull.Value)
                    {
                        ImagePath =(string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex )
            {
                string Error = ex.Message;
                isFound = false;
            }
            finally // it will be executed rater than try happens of catch .
            {
                connection.Close();
            }


            return isFound;

        }


        public static int AddNewContact( string FirstName, string LastName,  string Phone,
           string Email, string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {

            int ContactID = -1;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"Insert into contacts (FirstName , LastName , Email , Phone , Address , CountryID , DateOfBirth , ImagePath)
            values (@FirstName , @LastName , @Email , @Phone , @Address , @CountryID , @DateOfBirth , @ImagePath);
            Select SCOPE_IDENTITY();";
            
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email",Email);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);


            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath",System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ContactID = insertedID;

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

            return ContactID;

        }


        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Phone,
           string Email, string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {

            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"UPDATE Contacts SET FirstName = @FirstName,
                                             LastName = @LastName,  
                                            Email = @Email, 
                                            Phone = @Phone, 
                                            Address = @Address, 
                                            CountryID = @CountryID,
                                            ImagePath = @ImagePath,
                                            DateOfBirth = @DateOfBirth
                                            WHERE ContactID = @ContactID";
            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue("@ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone",Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

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
    
       
        public static bool DeleteContact (int ContactID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = @"Delete from Contacts Where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ContactID", ContactID);


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

        public static DataTable GetAllContacts()
        {
            DataTable DT = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select * from Contacts ";
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
            catch(Exception ex)
            {
                string Error = ex.Message;
            }
            finally
            {
                connection.Close(); 
            }
            return DT;
        }
     
        public static bool IsContactExist(int ID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);
            string Query = "Select Found =1 from Contacts Where ContactID = @ContactID ";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;

                reader.Close();
            }
            catch(Exception ex )
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
