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
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestAppointment _appointment;
       
        public frmTakeTest(int testAppointmentID, int testTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _appointment = clsTestAppointment.FindTestAppointment(testAppointmentID);
            
            if (_appointment == null) return;

            ctrlSchedule1.LoadTakeTestInfo(_appointment, testTypeID); // ← just this
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            if (_appointment == null)
            {
                MessageBox.Show("Appointment not found!");
                this.Close();
                return;
            }

            if (_appointment.IsLocked)
            {
                MessageBox.Show("This test has already been taken.",
                                "Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_appointment == null) return;

            if (!radPass.Checked && !radFail.Checked)
            {
                MessageBox.Show("Please select Pass or Fail.", "Result Required",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsTest test = new clsTest();
            test.TestAppointment.TestAppointmentID = _TestAppointmentID;
            test.TestResult = radPass.Checked ? clsTest.eTestResult.Passed : clsTest.eTestResult.Failed;
            test.Notes = txtNotes.Text.Trim();
            test.CreatedBy = clsUser.LoggedInUser.UserID;

            if (!test.Save())
            {
                MessageBox.Show("Failed to save test result.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool appCompleted = false;
            if (radPass.Checked && _appointment.TestTypeID == 3)
            {
                clsApplication app = clsApplication.FindApplication(
                    _appointment.DrivingLicenseApplication.ApplicationID);
                if (app != null)
                {
                    app.ApplicationStatus = clsApplication.eAppStatus.Completed;
                    appCompleted = app.Save();
                }
            }

            clsTestAppointment.MarkTestAppointmentAsLocked(_TestAppointmentID);

            // One single final message
            string result = radPass.Checked ? "✔ Passed" : "✘ Failed";
            string extra = appCompleted ? "\nApplication marked as Completed." : "";
            MessageBox.Show(result + extra, "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
    }

}
