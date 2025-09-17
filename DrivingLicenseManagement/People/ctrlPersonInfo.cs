using DrivingLicenseManagement.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
   
       
        private clsPeople _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }

      

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPeople.FindPersonByID(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No _Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPeople.FindPersonByNationalNo(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No _Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == false)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
     
            linkLabel1.Enabled = true;
            _PersonID = _Person.PersonID;
            
            lblAddress.Text = "      " + _Person.Address;
            lblCountry.Text = clsCountry.Find(_Person.CountryID).CountryName;
            lblDate.Text = "      " + _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblEmail.Text = "      " + _Person.Email;
            lblPhone.Text = "      " + _Person.Phone;
            lblGender.Text = "      " + (_Person.Gender == false ? "Male" : "Female");
            lblNumber.Text = "      " + _Person.NationalNo;
            lblPersonID.Text = "      " + _Person.PersonID.ToString();
            lblName.Text = "      " + _Person.FullName;

            _LoadPersonImage();

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "      [????]";
            lblNumber.Text = "      [????]";
            lblName.Text = "      [????]";
            lblGender.Image = Resources.Man_32;
            lblGender.Text = "[      ????]";
            lblEmail.Text = "[      ????]";
            lblPhone.Text = "[      ????]";
            lblDate.Text = "[      ????]";
            lblCountry.Text = "[      ????]";
            lblAddress.Text = "[      ????]";
            pictureBox1.Image = Resources.Male_512;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdatePerson frm = new UpdatePerson(_PersonID);
            frm.ShowDialog();

       
            LoadPersonInfo(_PersonID);
        }

    }
}
