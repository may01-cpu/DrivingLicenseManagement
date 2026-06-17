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
using static System.Net.Mime.MediaTypeNames;

namespace DrivingLicenseManagement.Applications.Controls
{
    public partial class ctrlApplicationInfo : UserControl
    {
        private clsApplication _Application;

        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonDetails frm = new ShowPersonDetails(_Application.Applicant.PersonID);
            frm.ShowDialog();

        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindApplication(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadApplicationInfo();

        }

        private void _LoadApplicationInfo()
        {
            lblApplicationID.Text = "      " + _Application.ApplicationID.ToString();
            lblType.Text = "      " + _Application.ApplicationType.ApplicationTypeName;
            lblApplicant.Text = "      " + _Application.Applicant.FullName;
            lblDate.Text = "      " + _Application.ApplicationDate.ToShortDateString();
            lblStatus.Text = "      " + _Application.ApplicationStatus.ToString();
            lblStatusDate.Text = "      " + _Application.LastStatusDate.ToShortDateString();
            lblFees.Text = "      " + _Application.ApplicationFees.ToString("F2");
            lblCreatedBy.Text = "      " + _Application.CreatedBy.Username;

        }
        public void ResetApplicationInfo()
        {
            lblApplicationID.Text = "      [????]";
            lblType.Text = "      [????]";
            lblApplicant.Text = "      [????]";
            lblDate.Text = "      [????]";
            lblStatus.Text = "      [????]";
            lblStatusDate.Text = "      [????]";

        }

    }
}
