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

namespace DrivingLicenseManagement.Applications.LocalDrivingLicense
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }


        private clsApplication _LocalApplication;
        public int LoadLocalApplicationInfo(int LocalApplicationID)
        {
            _LocalApplication = clsLocalDLApplication.FindLocalApplication(LocalApplicationID);
            if (_LocalApplication == null)
            {
                ResetLocalApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + LocalApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            _LoadLocalApplicationInfo();
            return _LocalApplication.ApplicationID;

        }

        private void _LoadLocalApplicationInfo()
        {
            lblDLAppID.Text = "      " + (_LocalApplication as clsLocalDLApplication).LocalApplicationID.ToString();
            lblLicenseClass.Text = "      " + (_LocalApplication as clsLocalDLApplication).LicenseClass.ClassName;
            lblLicenseClass.Tag = "      " + (_LocalApplication as clsLocalDLApplication).LicenseClass.LicenseClassID.ToString();
            lblPassedTests.Text = "      " + (_LocalApplication as clsLocalDLApplication).PassedTests.ToString();
        }
        public void ResetLocalApplicationInfo()
        {
            lblDLAppID.Text = "      N/A";
            lblLicenseClass.Text = "      N/A";
            lblLicenseClass.Tag = "      -1";
            lblPassedTests.Text = "      N/A";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }



    }
}
      
       