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
    public partial class ctrlUserFilter : UserControl
    {
        public ctrlUserFilter()
        {
            InitializeComponent();
        }
        private string FilterColumn = "";
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (FilterColumn == "PersonID")
            {
                // Allow digits, backspace, and control keys
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // block the key
                }
            }
        }
        private void cmbFilterUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem == null || cmbFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
            }
            switch (cmbFilter.SelectedItem.ToString())
            {
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                default:
                    FilterColumn = "";
                    break;


            }
        }

        private void pbFilter_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(FilterColumn) && !string.IsNullOrEmpty(txtFilter.Text))
            {
                if (FilterColumn == "NationalNo")
                {
                    ctrlPersonInfo1.LoadPersonInfo(txtFilter.Text);
                }
                else if (FilterColumn == "PersonID")
                {
                    if (int.TryParse(txtFilter.Text, out int personId))
                    {
                        ;
                        ctrlPersonInfo1.LoadPersonInfo(personId);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Person ID. Please enter a valid number.", "Input Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void ctrlUserFilter_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
            txtFilter.Focus();
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                errorProvider1.SetError(txtFilter, "This field cannot be empty.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFilter, string.Empty);
            }
        }

        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            AddNewPerson addNewPerson = new AddNewPerson();
            addNewPerson.ctrlAddPerson1.DataBack += DataBackEvent;
            addNewPerson.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
           
            cmbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            ctrlPersonInfo1.LoadPersonInfo(PersonID);
        }
    }
}
