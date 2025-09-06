using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement
{
    public partial class ctrlPersonInfo : UserControl
    {
        
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        public int PersonID { get; set; }

        public void RefreshPersonInfo()
        {
            _ShowPersonInfo();
        }

   

        private void _ShowPersonInfo() {

            clsPeople Person = clsPeople.FindPersonByID(PersonID);
            if (Person != null)
            {
                lblAddress.Text = "      "+ Person.Address;
                clsCountry country =clsCountry.Find(Person.CountryID);
                lblCountry.Text = "      " + country.CountryName;
                lblDate.Text = "      " + Person.DateOfBirth.ToString("dd/MM/yyyy");
                lblEmail.Text = "      " + Person.Email;
                lblPhone.Text = "      " + Person.Phone;
                lblGender.Text = "      " +( Person.Gender == false ? "Male" : "Female");
                lblNumber.Text = "      " + Person.NationalNo;
                lblPersonID.Text = "      " + PersonID.ToString();
                lblName.Text = "      " + Person.FirstName + " " + Person.SecondName 
                                + " " + Person.ThirdName + " " + Person.LastName;



            }
        }
    }
}
