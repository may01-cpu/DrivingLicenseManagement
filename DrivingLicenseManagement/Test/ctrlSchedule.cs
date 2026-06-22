using DVLDBusinessLayer;
using System;
using System.Windows.Forms;

namespace DrivingLicenseManagement.Test
{
    public partial class ctrlSchedule : UserControl
    {
        public clsLocalDLApplication LDLApplication { get; private set; }
        private clsTestType _testType;
        private int _testTypeID = -1;
        public DateTime AppointmentDate => dateTimePicker1.Value;
        public decimal TestFees => _testType?.Fee ?? 0;
        public int TestTypeID => _testTypeID;

        public ctrlSchedule()
        {
            InitializeComponent();
        }

        public void LoadAppointmentDate(DateTime date)
        {
            dateTimePicker1.Value = date;
        }
        public void LoadScheduleInfo(int LocalAppID, int testTypeID)
        {
            _testTypeID = testTypeID;
            LDLApplication = clsLocalDLApplication.FindLocalApplication(LocalAppID);
            if (LDLApplication != null)
                _LoadInfos();
            else
                _ResetInfos();
        }

        private void _LoadInfos()
        {
            lblDLAppID.Text = "      " + LDLApplication.LocalApplicationID.ToString();
            lblApplicantName.Text = "      " + LDLApplication.Applicant.FullName;
            lblClassName.Text = "      " + LDLApplication.LicenseClass.ClassName;
            lblTrial.Text = "      0";
            dateTimePicker1.Value = DateTime.Now;

            _testType = clsTestType.FindTestType(_testTypeID);
            lblFees.Text = _testType != null
                ? "      " + _testType.Fee.ToString("F2")
                : "      N/A";
        }

        private void _ResetInfos()
        {
            lblDLAppID.Text = "      N/A";
            lblApplicantName.Text = "      N/A";
            lblClassName.Text = "      N/A";
            lblTrial.Text = "      N/A";
            lblFees.Text = "      N/A";
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
