using DrivingLicenseManagement.Properties;
using DVLDBusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrivingLicenseManagement
{
    public partial class ctrlAddPerson : UserControl
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
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
            txtEmail.Text = person.Email;
            txtAddress.Text = person.Address;
            rbtnFemale.Checked = person.Gender;
            rbtnMale.Checked = !person.Gender;
            cmbCountry.SelectedValue = person.CountryID;
            pbPersonImage.ImageLocation = person.ImagePath;
            lkblRemove.Visible = (person.ImagePath != null);
            
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
            txtEmail.Clear();
            txtAddress.Clear();

            rbtnMale.Checked = true;
            pbPersonImage.Image = Resources.Male_512;
            cmbCountry.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMode == PersonMode.AddNew)
                CurrentPerson = new clsPeople();

            // Fill data from UI
            if (string.IsNullOrWhiteSpace(txtNationalNo.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                cmbCountry.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
          
            CurrentPerson.NationalNo = txtNationalNo.Text;
            CurrentPerson.FirstName = txtFirstName.Text;
            CurrentPerson.SecondName = txtSecondName.Text;
            CurrentPerson.ThirdName = txtThirdName.Text;
            CurrentPerson.LastName = txtLastName.Text;
            CurrentPerson.DateOfBirth = dtpBirthDate.Value;
            CurrentPerson.Phone = mtxtPhone.Text;
            CurrentPerson.Email = txtEmail.Text;
            CurrentPerson.Address = txtAddress.Text;
            CurrentPerson.Gender = rbtnFemale.Checked;
           
            if (cmbCountry.SelectedIndex != -1)
                CurrentPerson.CountryID = (int)cmbCountry.SelectedValue;

            if(!string.IsNullOrEmpty(selectedPhotoPath))
            {
              string newPath=PhotoManager.SaveOrUpdatePersonPhoto(selectedPhotoPath, CurrentPerson.ImagePath);
               CurrentPerson.ImagePath = newPath;
                pbPersonImage.Image = Image.FromFile(newPath);


            }

            if (CurrentPerson.Save())
            {
         
                MessageBox.Show("Person saved successfully.");
                DataBack?.Invoke(this, CurrentPerson.PersonID);
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
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Please enter a valid email address");
                e.Cancel = true; 
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
                e.Cancel = false;
            }
        }

        private void ctrlAddPerson_Load(object sender, EventArgs e)
        {
            dtpBirthDate.MaxDate = DateTime.Now.AddYears(-18);
            dtpBirthDate.MinDate = DateTime.Now.AddYears(-100);

        }

        private string selectedPhotoPath = "";
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dlg.Title = "Select a Photo";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedPhotoPath = dlg.FileName;

                    // Show preview in PictureBox
                    pbPersonImage.Image = Image.FromFile(selectedPhotoPath);
                }
            }
        }

        private void txtNationalNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                errorProvider1.SetError(txtNationalNo, "National No is required.");
                e.Cancel = true;

            }
            else
            {

                if (clsPeople.IsNationalNoExists(txtNationalNo.Text))
                {
                    errorProvider1.SetError(txtNationalNo, "National No already exists.");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                    e.Cancel = false;
                }
            }
        }

        private void lkblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PhotoManager.DeletePersonPhoto(CurrentPerson.ImagePath);
            _SetDefaultPhoto();
        }

        private void _SetDefaultPhoto()
        {
            pbPersonImage.Image = rbtnMale.Checked ? Resources.Male_512 : Resources.Female_512;

        }
        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            _SetDefaultPhoto();
        }
    }
}
