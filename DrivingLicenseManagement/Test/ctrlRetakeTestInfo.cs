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
    public partial class ctrlRetakeTestInfo : UserControl
    {
        public int LastFailedAppID { get; private set; } = -1;
        public decimal TotalFees { get; private set; }
        public ctrlRetakeTestInfo()
        {
            InitializeComponent();
        }

        public void LoadRetakeInfo(int LocalAppID, int testTypeID)
        {
            clsLocalDLApplication app = clsLocalDLApplication.FindLocalApplication(LocalAppID);
            clsTestType testType = clsTestType.FindTestType(testTypeID);

            decimal retakeAppFees = app?.ApplicationFees ?? 0;
            decimal totalFees = retakeAppFees + (testType?.Fee ?? 0);

            LastFailedAppID = app.ApplicationID;
            TotalFees = (int)totalFees;
            lblRAppFees.Text = retakeAppFees.ToString("F2");
            lblRTestAppID.Text = LastFailedAppID.ToString();
            lblTotalFees.Text = totalFees.ToString("F2");
        }
        public void Reset()
        {
            lblRAppFees.Text = "[????]";
            lblRTestAppID.Text = "N/A";
            lblTotalFees.Text = "[????]";
        }
    }
}
