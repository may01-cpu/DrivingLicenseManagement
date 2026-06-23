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
    public partial class frmScheduleTest : Form
    {
        private enum eMode { Add, Edit }
        private eMode _FormMode;
        private int _AppointmentID = -1;

        public frmScheduleTest(int LocalDLApp, int testTypeID)
        {
            InitializeComponent();
            _FormMode = eMode.Add;
            _LoadInfo(LocalDLApp, testTypeID);
        }

        public frmScheduleTest(int LocalDLApp, int testTypeID, int appointmentID)
        {
            InitializeComponent();
            _AppointmentID = appointmentID;
            _FormMode = eMode.Edit;
            _LoadInfo(LocalDLApp, testTypeID);

            clsTestAppointment existing = clsTestAppointment.FindTestAppointment(appointmentID);
            if (existing != null)
                ctrlSchedule1.LoadAppointmentDate(existing.AppointmentDate);
        }

        private void _LoadInfo(int LocalDLApp, int testTypeID)
        {
            ctrlSchedule1.LoadScheduleInfo(LocalDLApp, testTypeID);

            bool isRetake = clsLocalDLApplication.DoesAttendTestType(LocalDLApp, testTypeID);
            if (isRetake)
            {
                lblTitle.Text = "Schedule Retake Test";
                ctrlRetakeTestInfo1.Visible = true; // ← show it
                ctrlRetakeTestInfo1.LoadRetakeInfo(LocalDLApp, testTypeID);
            }
            else
            {
                lblTitle.Text =" Schedule Test";
                ctrlRetakeTestInfo1.Visible = false; // ← hide it
                ctrlRetakeTestInfo1.Reset();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_FormMode == eMode.Add)
            {
                clsTestAppointment appointment = new clsTestAppointment();
                appointment.DrivingLicenseApplication = ctrlSchedule1.LDLApplication;
                appointment.TestTypeID = ctrlSchedule1.TestTypeID;
                appointment.AppointmentDate = ctrlSchedule1.AppointmentDate;
                appointment.PaidFees = ctrlRetakeTestInfo1.Visible
                 ? ctrlRetakeTestInfo1.TotalFees  // ← retake: app fees + test fees
                    : ctrlSchedule1.TestFees;        // ← first time: just test fees
                appointment.IsLocked = false;
                appointment.CreatedByUserID = clsUser.LoggedInUser.UserID;
                appointment.RetakeTestApplicationID = ctrlRetakeTestInfo1.Enabled
                                                        ? ctrlRetakeTestInfo1.LastFailedAppID
                                                        : -1;
                if (appointment.Save())
                {
                    MessageBox.Show("Appointment Scheduled Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to Schedule Appointment");
                }
            }
            else // Edit — only date changes
            {
                clsTestAppointment appointment = clsTestAppointment.FindTestAppointment(_AppointmentID);
                if (appointment != null)
                {
                    appointment.AppointmentDate = ctrlSchedule1.AppointmentDate; // ← only this changes
                    if (appointment.Save())
                    {
                        MessageBox.Show("Appointment Updated Successfully");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Update Appointment");
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}