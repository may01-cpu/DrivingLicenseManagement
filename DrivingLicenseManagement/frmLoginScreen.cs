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
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                clsUser user = clsUser.AuthenticateUser(username, password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        MainForm mainForm= new MainForm();
                        mainForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("you're not an active user please contact your admin", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("wrong password or username","", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                if (cbRemember.Checked) {
                  clsUser.RememberUser(username,password);
                }
            }
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            if (File.Exists(clsUser.UserPath))
            {
                string firstLine = File.ReadLines(clsUser.UserPath).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(firstLine))
                {
                    string[] parts = firstLine.Split(':');

                    if (parts.Length >= 2) 
                    {
                        txtUsername.Text = parts[0];
                        txtPassword.Text = parts[1];

                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRemember_CheckedChanged(object sender, EventArgs e)
        {
           
            }
        }
    }

