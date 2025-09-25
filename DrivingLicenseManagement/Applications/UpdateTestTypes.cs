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

namespace DrivingLicenseManagement.Applications
{
    public partial class UpdateTestTypes : Form
    {
        public UpdateTestTypes(int ID)
        {
            InitializeComponent();
            _TestType =clsTestType.FindTestType(ID);


        }

        clsTestType _TestType;
      
        private void Validate(CancelEventArgs e,TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "This field is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeName = txtTitle.Text;
            _TestType.TestTypeDescription= txtDescription.Text;
            _TestType.Fee = txtFees.Text == string.Empty ? 0 : Convert.ToDecimal(txtFees.Text);
            if (_TestType.UpdateTestType())
            {
                MessageBox.Show("Test Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update Test Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            Validate(e, txtTitle);
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            Validate(e,txtDescription);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            Validate(e, txtFees);
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow control keys (backspace, delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Allow digits
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Allow one dot if not already present
            else if (e.KeyChar == '.' && txt.Text.IndexOf('.') == -1)
            {
                e.Handled = false;
            }
            else
            {
                // Block everything else
                e.Handled = true;
            }

        }

        private void UpdateTestTypes_Load_1(object sender, EventArgs e)
        {
            lblAppTypeID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeName;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.Fee.ToString("F2");
        }
    }
}
