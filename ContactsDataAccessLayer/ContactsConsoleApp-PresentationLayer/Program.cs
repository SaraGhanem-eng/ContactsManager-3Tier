using System;
using System.Data;
using ContactsBusinessLayer;


namespace ContactsConsoleApp_PresentationLayer
{
    internal class Program   
    {

        static void testFindContact(int ID)
        {
            clsContacts Contact1 = clsContacts.Find(ID);

            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName + " " + Contact1.LastName );

                Console.WriteLine(Contact1.Email);

                Console.WriteLine(Contact1.Phone);

                Console.WriteLine(Contact1.Address);

                Console.WriteLine(Contact1.DateOfBirth);

                Console.WriteLine(Contact1.CountryID);

                Console.WriteLine(Contact1.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [ " + ID + " ] Is Not Found !");
            }
        }
        
        static void testFindCountryByID(int ID)
        {
            clsCountries Country1 = clsCountries.FindByCountryID(ID);

            if (Country1 != null)
            {
                Console.WriteLine(Country1.CountryID);

                Console.WriteLine(Country1.CountryName);

                Console.WriteLine(Country1.Code);

                Console.WriteLine(Country1.PhoneCode);

            }
            else
            {
                Console.WriteLine("Country [ " + ID + " ] Is Not Found !");
            }
        }

        static void testFindCountryByCountryName(string CountryName)
        {
            clsCountries Country1 = clsCountries.FindByCountryName(CountryName);

            if (Country1 != null)
            {
                Console.WriteLine(Country1.CountryID);

                Console.WriteLine(Country1.CountryName);

                Console.WriteLine(Country1.Code);

                Console.WriteLine(Country1.PhoneCode);

            }
            else
            {
                Console.WriteLine("Country [ " + CountryName + " ] Is Not Found !");
            }
        }
        
        static void testAddNewContact()
        {
            clsContacts Contact1 = new clsContacts();

            Contact1.FirstName = "Fadi";
            Contact1.LastName = "Maher";
            Contact1.Phone = "0293829";
            Contact1.Email = "fadi@gmail.com";
            Contact1.Address = "78 street";
            Contact1.DateOfBirth = DateTime.Now;
            Contact1.ImagePath = "";
            Contact1.CountryID = 2;

            if (Contact1.Save())
            {
                Console.WriteLine("Contact Added Succesfully with ID = " + Contact1.ID);
            }
            else
            {
                Console.WriteLine("Failed to Add Contact with First Name : " + Contact1.FirstName);
            }
        }
        
        static void testAddNewCountry()
        {
            clsCountries Country1 = new clsCountries();

            Country1.CountryName = "Etaly";
            Country1.Code = "789";
            Country1.PhoneCode = "999";


            if (Country1.Save())
            {
                Console.WriteLine("Country Added Succesfully with ID = " + Country1.CountryID);
            }
            else
            {
                Console.WriteLine("Failed to Add Country with Name : " + Country1.CountryName);
            }
        }
       
        static void testUpdateContact(int ID)
        {
            clsContacts Contact1 = clsContacts.Find(ID);


            if (Contact1 != null)
            {
                Contact1.FirstName = "Lina";
                Contact1.LastName = "Maher";
                Contact1.Phone = "0293829";
                Contact1.Email = "fa@gmail.com";
                Contact1.Address = "77 street";
                Contact1.DateOfBirth = DateTime.Now;
                Contact1.ImagePath = "";
                Contact1.CountryID = 1;

                if (Contact1.Save())
                {
                    Console.WriteLine("Contact Updated Succesfully   " );
                }
                else
                {
                    Console.WriteLine("Failed to Updated Contact ! " );
                }
            }
            else
            {
                Console.WriteLine("Contact is not Exist");
            }
            }
        
        static void testUpdateCountry(int ID)
        {
            clsCountries Country = clsCountries.FindByCountryID(9);


            Country.CountryName = "Indonisia";
            Country.Code = "123";

            if( Country.Save())
            {
                Console.WriteLine("Country Updated Succesfully ");
            }
            else
            {
                Console.WriteLine("Failed to Update Country !");
            }
        }
        
        static void testDeleteContact(int ID )
        {

            if (clsContacts.IsContactExist(ID))
            {
                if (clsContacts.DeleteContact(ID))
                {
                    Console.WriteLine("Contact Deleted Successfully ! ");
                }
                else
                {
                    Console.WriteLine("Failed to Delete Contact ! ");
                }
            }
            else
            {
                Console.WriteLine("Contact is not exist there !");
            }
        }
        
        static void testDeleteCountry (int ID)
        {
            if (clsCountries.IsCountryExist(ID))
            {
                if (clsCountries.DeleteCountry(ID))
                {
                    Console.WriteLine("Country Deleted Successfully ! ");
                }
                else
                {
                    Console.WriteLine("Failed to Delete Contact ! ");
                }
            }
            else
            {
                Console.WriteLine("Country is not exist there !");
            }
        }
       
        static void ListContacts()
        {
            DataTable dt = clsContacts.GetAllContacts();

            Console.WriteLine("Data Contacts");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactID"]} , {row["FirstName"]}  {row["LastName"]} ");
            }
        }
        
        static void ListCountries()
        {
            DataTable dt = clsCountries.GetAllCountries();

            Console.WriteLine("Data Countries");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["CountryID"]} , {row["CountryName"]}  {row["Code"]}  {row["PhoneCode"]}");
            }
        }
        
        static void testIsContactExist(int ID)
        {
        
            if (clsContacts.IsContactExist(ID))

                Console.WriteLine("Contact is Exist ! ");
            else
                Console.WriteLine("Contact is not exist !");

        }

        static void testIsCountryExistByID(int ID)
        {

            if (clsCountries.IsCountryExist(ID))

                Console.WriteLine("Country is Exist ! ");
            else
                Console.WriteLine("Country is not exist !");
        }

        static void testIsCountryExistByCountryName(string CountryName)
        {

            if (clsCountries.IsCountryExist(CountryName))

                Console.WriteLine("Country is Exist ! ");
            else
                Console.WriteLine("Country is not exist !");
        }
       
        
        static void Main(string[] args)
        {

            // testFindCountryByID(9);
            //testFindCountryByCountryName("Egypt");
            //testAddNewCountry();
            // testUpdateCountry(7);
            //testDeleteCountry(8);
            //ListCountries();
            //testIsCountryExistByID(10);
            //testIsCountryExistByCountryName("Etaly");
        }
    }
}
