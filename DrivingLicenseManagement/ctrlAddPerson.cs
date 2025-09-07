using DVLDBusinessLayer;
using System;
using System.Windows.Forms;

namespace DrivingLicenseManagement
{
    public partial class ctrlAddPerson : UserControl
    {
        enum PersonMode
        {
            AddNew,
            Update
        }

        private PersonMode CurrentMode;
        private clsPeople CurrentPerson;  // holds the person being updated

        public bool Success { get; private set; }

        public ctrlAddPerson()
        {
            InitializeComponent();

            // Fill countries combo
            cmbCountry.DataSource = clsCountry.GetAllCountries();
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";
        }

        // Public method to prepare control for AddNew
        public void PrepareForAdd()
        {
            ClearForm();
            CurrentMode = PersonMode.AddNew;
        }

        // Public method to prepare control for Update
        public void PrepareForUpdate(int personID)
        {
            CurrentPerson = clsPeople.FindPersonByID(personID);
            if (CurrentPerson == null)
            {
                MessageBox.Show("Person not found.");
                return;
            }

            LoadPersonData(CurrentPerson);
            CurrentMode = PersonMode.Update;
        }

        private void LoadPersonData(clsPeople person)
        {
            txtNationalNo.Text = person.NationalNo;
            txtFirstName.Text = person.FirstName;
            txtSecondName.Text = person.SecondName;
            txtThirdName.Text = person.ThirdName;
            txtLastName.Text = person.LastName;
            dtpBirthDate.Value = person.DateOfBirth;
            mtxtPhone.Text = person.Phone;
            mtxtEmail.Text = person.Email;
            txtAddress.Text = person.Address;

            rbtnFemale.Checked = person.Gender;
            rbtnMale.Checked = !person.Gender;

            cmbCountry.SelectedValue = person.CountryID;
        }

        private void ClearForm()
        {
            txtNationalNo.Clear();
            txtFirstName.Clear();
            txtSecondName.Clear();
            txtThirdName.Clear();
            txtLastName.Clear();
            dtpBirthDate.Value = DateTime.Now;
            mtxtPhone.Clear();
            mtxtEmail.Clear();
            txtAddress.Clear();

            rbtnMale.Checked = true;
            cmbCountry.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMode == PersonMode.AddNew)
                CurrentPerson = new clsPeople();

            // Fill data from UI
            CurrentPerson.NationalNo = txtNationalNo.Text;
            CurrentPerson.FirstName = txtFirstName.Text;
            CurrentPerson.SecondName = txtSecondName.Text;
            CurrentPerson.ThirdName = txtThirdName.Text;
            CurrentPerson.LastName = txtLastName.Text;
            CurrentPerson.DateOfBirth = dtpBirthDate.Value;
            CurrentPerson.Phone = mtxtPhone.Text;
            CurrentPerson.Email = mtxtEmail.Text;
            CurrentPerson.Address = txtAddress.Text;
            CurrentPerson.Gender = rbtnFemale.Checked;
            if (cmbCountry.SelectedIndex != -1)
                CurrentPerson.CountryID = (int)cmbCountry.SelectedValue;

            if (CurrentPerson.Save())
            {
                MessageBox.Show("Person saved successfully.");
                Success = true;
                Parent.FindForm()?.Close();
            }
            else
            {
                MessageBox.Show("Error saving person.");
                Success = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Parent.FindForm()?.Close();
        }
    }
}
