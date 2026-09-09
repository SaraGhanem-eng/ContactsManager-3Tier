using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using ContactsDataAccessLayer;


namespace ContactsBusinessLayer
{
    public class clsCountries
    {
        public enum enMode { Update, AddNew };
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
        public enMode Mode { set; get; }

       
        public clsCountries ()
        {
            this.CountryID = -1 ;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";
            this.Mode = enMode.AddNew;
        }

        private clsCountries(int CountryID , string CountryName , string Code , string PhoneCode)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
            this.Mode = enMode.Update;
        }


        private bool _AddNewCountry()
        {
            // call Data base layer 

            this.CountryID = clsCountriesDataAccess.AddNewCountry(this.CountryName , this.Code , this.PhoneCode);

            if (this.CountryID != -1)
                return true;
            else
                return false;
        }

        private bool _UpdateCountry()
        {
            // call data base layer 

            return clsCountriesDataAccess.UpdateCountry(this.CountryID, this.CountryName , this.Code , this.PhoneCode);
        }
    
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateCountry();


            }

            return true;
        }
    
        static public DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }
    
        static public bool DeleteCountry(int ID)
        {
            return clsCountriesDataAccess.DeleteCountry(ID);
        }

        static public bool IsCountryExist(int ID)
        {
            return clsCountriesDataAccess.IsCountryExistByCountryID(ID);
        }
        
        static public bool IsCountryExist(string CountryName)
        {
            return clsCountriesDataAccess.IsCountryExistByCountryName(CountryName);
        }

        static public clsCountries FindByCountryID(int ID)

        {

            string CountryName = "", Code = "", PhoneCode = "";

            if (clsCountriesDataAccess.FindCountryByID(ID, ref CountryName, ref Code, ref PhoneCode))

            {

                return new clsCountries(ID, CountryName, Code, PhoneCode);

            }

            else

            {

                return null;

            }

        }

        static public clsCountries FindByCountryName(string CountryName)

        {

            int ID = -1;

            string Code = "", PhoneCode = "";

            if (clsCountriesDataAccess.FindCountryByCountryName(CountryName, ref ID, ref Code, ref PhoneCode))

            {

                return new clsCountries(ID, CountryName, Code, PhoneCode);

            }

            else

            {

                return null;

            }

        }
    }
}
