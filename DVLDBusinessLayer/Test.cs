using System;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        public int TestID { get; set; }
        public clsTestAppointment TestAppointment { get; set; }
        public enum eTestResult { Passed, Failed }
        public eTestResult TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }

        public enum eOpType { Add, Update };

        eOpType OperationType;

        public clsTest()
        {
            TestID = -1;
            TestAppointment = new clsTestAppointment();
            TestResult = eTestResult.Failed;
            Notes = "";
            CreatedBy = -1;
        }

        private clsTest(int testID, int testAppointmentID, eTestResult testResult, string notes, int createdBy)
        {
            TestID = testID;
            TestAppointment = new clsTestAppointment(testAppointmentID, -1, DateTime.Now, 0, false, -1, -1);
            TestResult = testResult;
            Notes = notes;
            CreatedBy = createdBy;
        }

        private bool _AddNewTest()
        {
            this.TestID =
              clsTestData.AddNewTest(
                this.TestAppointment.TestAppointmentID,
                Convert.ToByte(this.TestResult),
                this.Notes,
                this.CreatedBy
               );
            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, Convert.ToByte(this.TestResult), this.Notes);
        }

        public bool Save()
        {
            switch (OperationType)
            {
                case eOpType.Add:
                    if (_AddNewTest())
                    {

                        OperationType = eOpType.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eOpType.Update:


                    return _UpdateTest();

            }
            return false;
        }


    }
}
