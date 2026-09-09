using System;
using System.Data;
using System.Xml.Linq;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContacts
    {

        public enum enMode { Update , AddNew};

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { set; get; }
        public enMode Mode { set; get; }


        private clsContacts(int ID , string FirstName , string LastName , string Email , string Phone , string Address ,
            DateTime DateOfBirth , int CountryID , string ImagePath)
        {
            // This Constructor is Private , can only be called inside the class "clsContacts" used for FIND methed.
            // it has (ID) field given as a parameter , this allows searching for the Contact from the DB .
            // we can not use it for Add new or Update methods .
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            this.Mode = enMode.Update;

        }
       

        private bool _AddNewContact()
        {
            // call Data base layer 

            this.ID = clsContactDataAccess.AddNewContact(this.FirstName , this.LastName , this.Email , this.Phone , 
                this.Address , this.ImagePath , this.DateOfBirth , this.CountryID);

            if (this.ID != -1)
                return true;
            else
                return false;
        }

        private bool _UpdateContact()
        {
            // call data base layer 

            return clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.ImagePath, this.DateOfBirth, this.CountryID);
        }

        public clsContacts()
        {
            // This constuctor can be used insede & out the class , can be called for Add , Update method .
            // creates new object & initialization for entities with default values .

            this.ID = -1; // ID is Not tuely exists
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1 ;
            this.ImagePath = "";

            this.Mode = enMode.AddNew;
        }
      
        public static clsContacts Find (int ID)
       {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "",
                ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.FindContactByID(ID , ref FirstName , ref LastName ,ref Phone , ref Email , ref Address 
                , ref ImagePath , ref DateOfBirth ,ref CountryID))
            {
                return new clsContacts(ID, FirstName, LastName, Email, Phone,
                    Address, DateOfBirth, CountryID, ImagePath);
                   
            }
            else
            {
                return null;
            }


       }
        
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateContact();
                        

            }

            return true;
        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }
        
        static public bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }
    
        static public bool IsContactExist(int ID)
        {
            return clsContactDataAccess.IsContactExist(ID);

        }
          
    
    }
}
