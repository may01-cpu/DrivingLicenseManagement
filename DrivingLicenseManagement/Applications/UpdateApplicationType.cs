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
    public partial class UpdateApplicationType : Form
    {
        clsApplicationType _ApplicationType;
        public UpdateApplicationType(int ID)
        {
            InitializeComponent();
            _ApplicationType = clsApplicationType.FindApplicationType(ID);


        }
        private void UpdateApplicationType_Load(object sender, EventArgs e)
        {
            lblAppTypeID.Text=_ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text=_ApplicationType.ApplicationTypeName;
            txtFees.Text=_ApplicationType.Fee.ToString("F2");
        }
        private void Validate(CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This field is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            Validate(e);
        }
        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            Validate(e);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            _ApplicationType.ApplicationTypeName = txtTitle.Text;
            _ApplicationType.Fee=txtFees.Text==string.Empty?0:Convert.ToDecimal(txtFees.Text);
            if(_ApplicationType.UpdateApplicationType())
            {
                MessageBox.Show("Application Type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update Application Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
