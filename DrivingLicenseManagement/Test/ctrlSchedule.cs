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

namespace DrivingLicenseManagement.Test
{
    public partial class ctrlSchedule : UserControl
    {
        private clsLocalDLApplication LDLApplication;
        public ctrlSchedule()
        {
            InitializeComponent();
        }

        
        public void LoadScheduleInfo(int LocalAppID)
        {
            LDLApplication=clsLocalDLApplication.FindLocalApplication(LocalAppID);
            if (LDLApplication != null)
            {
                _LoadInfos();
            }
            else
            {
                _ResetInfos();
            }

        }
        private void _LoadInfos()
        {
            lblDLAppID.Text = "      " + LDLApplication.LocalApplicationID.ToString();
            lblApplicantName.Text = "      " + LDLApplication.Applicant.FullName;
            lblClassName.Text = "      " + LDLApplication.LicenseClass.ClassName;
            lblTrial.Text = "      " + "0";
            lblFees.Text = "      " + LDLApplication.ApplicationFees.ToString();
            dateTimePicker1.Text =  DateTime.Now.ToString();
        }
        private void _ResetInfos()
        {
            lblDLAppID.Text = "      N/A";
            lblApplicantName.Text = "      N/A";
            lblClassName.Text = "      N/A";
            lblTrial.Text = "      N/A";
            lblFees.Text = "      N/A";
            dateTimePicker1.Text = DateTime.Now.ToString();
        }

    }
}
