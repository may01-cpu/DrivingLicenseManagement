using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPeople
    {
        public int PersonID {  get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName {  get; set; }
        public string ThirdName {  get; set; }
        public DateTime DateOfBirth { get; set; }
       //is it a female?
        public bool Gender {  get; set; }
        public string Address {  get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }
        public string ImagePath { get; set; }

        public int CountryID {  get; set; }
        public clsPeople() {
            PersonID = -1;
            NationalNo = "";
            Gender = false;
            Address = "";
            Phone = "";
            Email = "";
            ImagePath = "";
            DateOfBirth = DateTime.Now;
            FirstName= "";
            LastName = "";
            SecondName= "";
            ThirdName = "";
            CountryID = -1;
        }

        public clsPeople(int personID,string nationalNo,  string firstName, string lastName, string secondName, string thirdName, DateTime dateOfBirth, bool gender, string address, string phone, string email, string imagePath, int country)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            ThirdName = thirdName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            ImagePath = imagePath;
            this.CountryID = country;
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }

        public static clsPeople FindPersonByID(int PersonID)
        {
            bool gender = false;
            string address = "";
            string phone = "";
            string email = "";
            string imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            string firstName = "";
            string lastName = "";
            string secondName = "";
            string thirdName = "";
            int countryID = -1;
            string nationalNo = "";
            if (clsPeopleDataAccess.GetPersonByID(PersonID,ref nationalNo,ref firstName, ref lastName, ref secondName, ref thirdName, ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref imagePath, ref countryID))
            {
              
                return new clsPeople(PersonID,nationalNo,firstName,lastName,secondName,thirdName,dateOfBirth,gender,address,phone,email,imagePath, countryID);
              ;

            }
            else
                return null;
        }
    }
}
