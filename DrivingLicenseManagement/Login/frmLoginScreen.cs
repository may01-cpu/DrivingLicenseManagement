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
using System.Windows.Forms.VisualStyles;
using System.IO;
namespace DrivingLicenseManagement
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private string username = "";
        private string password = "";
        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsUser user = clsUser.AuthenticateUser(username, password);

            if (user != null)
            {
                if (user.IsActive)
                {
                    // Save credentials if Remember Me is checked
                    if (cbRemember.Checked)
                    {
                        Properties.Settings.Default.UserName = username;
                        Properties.Settings.Default.Password = password;
                    }
                    else
                    {

                        Properties.Settings.Default.UserName = "";
                        Properties.Settings.Default.Password = "";

                    }
                    Properties.Settings.Default.Save();
                    clsUser.LoggedInUser = user;
                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.ShowDialog();
                    this.Show();
                    if (string.IsNullOrEmpty(Properties.Settings.Default.UserName) ||
                    string.IsNullOrEmpty(Properties.Settings.Default.Password))
                    {
                        txtUsername.Clear();
                        txtPassword.Clear();
                        cbRemember.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show("You're not an active user. Please contact your admin.",
                                    "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Wrong username or password.", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
        private void frmLoginScreen_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UserName != "" &&
                Properties.Settings.Default.Password != "")
            {
                txtUsername.Text = Properties.Settings.Default.UserName;
                txtPassword.Text = Properties.Settings.Default.Password;
                cbRemember.Checked = true;
            }
            else
            {
                
                txtUsername.Clear();
                txtPassword.Clear();
                cbRemember.Checked = false;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        }
    }

