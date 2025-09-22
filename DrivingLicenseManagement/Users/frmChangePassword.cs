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
    public partial class frmChangePassword : Form
    {
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _User = clsUser.GetUserByID(UserID);
            ctrlUserInfo1.LoadUserInfo(UserID);
            ctrlPersonInfo1.LoadPersonInfo(_User.Person.PersonID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCurrentPsw.Text) && !string.IsNullOrWhiteSpace(txtNewPsw.Text) && !string.IsNullOrWhiteSpace(txtConfirmPsw.Text))
            {
                if (_User.ChangePassword(txtNewPsw.Text))
                {
                    MessageBox.Show("Password changed successfuly");
                }
                else
                {
                    MessageBox.Show("Faild to change the Password ");

                }

            }

        }
        private bool _MatchPassword(string password, string confirmPassword)
        {
            return string.Equals(password, confirmPassword);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }
        private void txtConfirmPsw_Validating(object sender, CancelEventArgs e)
        {
            if (!_MatchPassword(txtNewPsw.Text, txtConfirmPsw.Text))
            {
                errorProvider1.SetError(txtConfirmPsw, "incorect password!");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void txtCurrentPsw_Validating(object sender, CancelEventArgs e)
        {
            if (_User.Password != txtCurrentPsw.Text)
            {
                errorProvider1.SetError(txtConfirmPsw, "incorect password!");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

    }
}
