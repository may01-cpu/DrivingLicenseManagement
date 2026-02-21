using DVLDDataAccessLayer;
using System;
using System.Data;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;
using static DVLDBusinessLayer.clsTestAppointment;


namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { get; set; }
        public clsLocalDLApplication DrivingLicenseApplication { get; set; }

        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public int CreatedByUserID { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public int TestTypeID { get; set; }

        public enum eOpType { Add, Update };

        eOpType OperationType;

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            DrivingLicenseApplication = new clsLocalDLApplication();
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            IsLocked = false;
            CreatedByUserID = -1;
            RetakeTestApplicationID = -1;
            OperationType = eOpType.Add;

        }

        public clsTestAppointment(int testAppointmentID, int drivingLicenseApplicationID, DateTime appointmentDate, decimal paidFees, bool isLocked, int createdByUserID, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            DrivingLicenseApplication = clsLocalDLApplication.FindLocalApplication(drivingLicenseApplicationID);
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            IsLocked = isLocked;
            CreatedByUserID = createdByUserID;
            RetakeTestApplicationID = retakeTestApplicationID;
            OperationType = eOpType.Update;
        }

        public static DataTable ListAllTestAppointments()
        {
            DataTable dt = clsTestAppointmentData.GetAllTestAppointments();
            return dt;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID =
              clsTestAppointmentData.AddNewTestAppointment(
                this.TestTypeID,
                this.DrivingLicenseApplication.LocalApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked,
                this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.AppointmentDate);
        }

        public bool Save()
        {
            switch (OperationType)
            {
                case eOpType.Add:
                    if (_AddNewTestAppointment())
                    {

                        OperationType = eOpType.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eOpType.Update:


                    return _UpdateTestAppointment();

            }
            return false;
        }

        public static bool MarkTestAppointmentAsLocked(int testAppointmentID)
        {
            return clsTestAppointmentData.MarkTestAppointmentAsLocked(testAppointmentID);
        }
    }
}
