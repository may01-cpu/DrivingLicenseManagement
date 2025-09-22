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

namespace DrivingLicenseManagement.Users
{
    public partial class frmAddEditUser : Form
    {
        enum eMode { Add, Edit }

        private eMode FormMode;
        private clsUser _User;

        public frmAddEditUser()
        {

            InitializeComponent();
            lblTitle.Text = "Add User";
            FormMode = eMode.Add;
            _User = new clsUser();

        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            lblTitle.Text = "Update User";
            FormMode = eMode.Edit;

           _User= clsUser.GetUserByID(UserID);
            ctrlUserFilter1.UpdatePersonInfo(_User.Person);
        }

     
        private void  _ResetUserInfo()
        {
            lblUserID.Text = "????";
            txtPsw.Text= string.Empty;
            txtPswConfirm.Text= string.Empty;
            txtUsername.Text = string.Empty;
            chkIsActive.Checked = false;
        }

        private void _LoadUserInfo()
        {
            lblUserID.Text = _User.UserID.ToString();
            txtPsw.Text = _User.Password;
            txtPswConfirm.Text = _User.Password;
            txtUsername.Text = _User.Username;
            chkIsActive.Checked = _User.IsActive;

        }
        private bool _MatchPassword(string password,string confirmPassword)
        {
            return string.Equals(password, confirmPassword);
        }
       
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlUserFilter1.SelectedPersonInfo != null)
            {
                tabPage2.Enabled = true;
                tabControl1.SelectedTab = tabPage2;

                if (FormMode == eMode.Add && !clsUser.IsUserExistsByPersonID(ctrlUserFilter1.SelectedPersonInfo.PersonID))
                {
                    _User.Person = ctrlUserFilter1.SelectedPersonInfo;
                    _ResetUserInfo();

                }
                else if (FormMode == eMode.Edit)
                {
                    _LoadUserInfo();
                }
                else
                {
                    tabPage2.Enabled = false;
                    MessageBox.Show("This person is already a user.");
                }

            }
            else
            {
                tabPage2.Enabled = false;
                MessageBox.Show("You have to choose a Person");

            }

        
        
        }
               
        

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            tabPage2.Enabled = false;
            ctrlUserFilter1.Enabled = (FormMode == eMode.Add);

                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPsw.Text) && !string.IsNullOrEmpty(txtPswConfirm.Text))
            {
                _User.Username = txtUsername.Text;
                _User.Password = txtPsw.Text;
                _User.IsActive = chkIsActive.Checked;
                if (_User.Save())
                {
                    string message = FormMode == eMode.Add ? "User Added Successfuly" : "User Updated Successfuly";
                    
                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Faild to save User","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("You must fill in all the fields.");

                return;
            }
        }
           
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPswConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (!_MatchPassword(txtPsw.Text, txtPswConfirm.Text))
            {
                errorProvider1.SetError(txtPswConfirm, "incorect password!");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void txtPsw_Validating(object sender, CancelEventArgs e)
        {
            if (txtPsw.Text.Length < 6) {
                errorProvider1.SetError(txtPsw, "Password must be at least 6 characters");
                e.Cancel= true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username required");
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
