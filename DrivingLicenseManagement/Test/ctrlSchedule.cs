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
        public void LoadScheduleInfo(int localAppID, int testTypeID)
        {
            LoadInfo(
                clsLocalDLApplication.FindLocalApplication(localAppID),
                testTypeID);
        }

        public void LoadTakeTestInfo(clsTestAppointment appointment, int testTypeID)
        {
            LoadInfo(
                appointment?.DrivingLicenseApplication,
                testTypeID,
                appointment?.AppointmentDate);
        }

        private void LoadInfo(clsLocalDLApplication application,
                      int testTypeID,
                      DateTime? appointmentDate = null)
        {
            _testTypeID = testTypeID;
            _testType = clsTestType.FindTestType(testTypeID);
            LDLApplication = application;

            if (LDLApplication == null)
            {
                _ResetInfos();
                return;
            }

            _LoadInfos(appointmentDate);
        }

        private void _LoadInfos(DateTime? date = null)
        {
            byte trials = clsLocalDLApplication.TotalTrialsPerTest(
                LDLApplication.LocalApplicationID,
                _testTypeID);

            lblDLAppID.Text = "      "+ LDLApplication.LocalApplicationID.ToString();
            lblApplicantName.Text = "      " + LDLApplication.Applicant.FullName;
            lblClassName.Text = "      " + LDLApplication.LicenseClass.ClassName;
            lblTrial.Text = "      " + trials.ToString();

            dateTimePicker1.Value = date ?? DateTime.Now;
            dateTimePicker1.Enabled = !date.HasValue;

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
