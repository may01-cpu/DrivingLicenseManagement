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

        public frmTakeTest(int testAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _appointment = clsTestAppointment.FindTestAppointment(testAppointmentID);
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            if (_appointment == null)
            {
                MessageBox.Show("Appointment not found!");
                this.Close();
                return;
            }

            // block if already locked (test already taken)
            if (_appointment.IsLocked)
            {
                MessageBox.Show("This test has already been taken and cannot be modified.",
                                "Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_appointment == null) return;
        
            bool passed = radPass.Checked; 

            clsTest test = new clsTest();
            test.TestAppointment.TestAppointmentID = _TestAppointmentID;
            test.TestResult = passed ? clsTest.eTestResult.Passed : clsTest.eTestResult.Failed;
            test.Notes = txtNotes.Text.Trim(); 
            test.CreatedBy = clsUser.LoggedInUser.UserID;

            if (test.Save())
            {
                bool locked = clsTestAppointment.MarkTestAppointmentAsLocked(_TestAppointmentID);
                if (!locked)
                    MessageBox.Show("Warning: Failed to lock appointment!");

                MessageBox.Show(passed ? "Passed!" : "Failed.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save test result.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
