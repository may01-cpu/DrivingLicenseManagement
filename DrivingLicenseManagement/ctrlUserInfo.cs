﻿using DVLDBusinessLayer;
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
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            clsUser TheUser = clsUser.GetUserByID(UserID);
            if (TheUser != null)
            {
                lblUsername.Text = TheUser.Username;
                lblIsActive.Text = TheUser.IsActive ? "Yes" : "No";
                lblUserID.Text = UserID.ToString();
             }
            else
            {
                ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void ResetPersonInfo()
        {
            lblUsername.Text = "???";
            lblIsActive.Text = "???";
            lblUserID.Text = "???";
        }
    }
}
