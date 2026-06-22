using DVLDDataAccessLayer;
using System;

namespace DVLDBusinessLayer
{
    public class clsLocalDLApplication : clsApplication
    {
        public int LocalApplicationID { get; set; }
        public clsLicenseClasses LicenseClass { get; set; }

        public int PassedTests { get; set; }
        private enum eOperation { Add, Update }
        private eOperation _operation;
        public clsLocalDLApplication()
        {
            LocalApplicationID = -1;
            LicenseClass = new clsLicenseClasses();
            PassedTests= 0;
            _operation = eOperation.Add;

        }
        public clsLocalDLApplication(int LicenseClass)
        {

            //this.LicenseClass=clsLicenseClasses.;
            _operation = eOperation.Update;

        }

        private clsLocalDLApplication(int LocalAppID, int ApplicationID, clsLicenseClasses licenseClass,int PassedTests)
        {
            LocalApplicationID = LocalAppID;
            LicenseClass = licenseClass;
            clsApplication Application = FindApplication(ApplicationID);
            this.ApplicationID = ApplicationID;
            Applicant = Application.Applicant;
            ApplicationDate = Application.ApplicationDate;
            ApplicationType = Application.ApplicationType;
            ApplicationStatus = Application.ApplicationStatus;
            LastStatusDate = Application.LastStatusDate;
            ApplicationFees = Application.ApplicationFees;
            CreatedBy = Application.CreatedBy;
            this.PassedTests = PassedTests;

            _operation = eOperation.Update;
        }
        public static clsLocalDLApplication FindLocalApplication(int LocalAppID)
        {

            int LicenseClassID = -1;
            int AppID = -1;
            int PassedTests=clsApplicationData.GetPassedTests(LocalAppID);

            if (clsApplicationData.GetLocalApplicationByID(LocalAppID, ref AppID, ref LicenseClassID) && PassedTests !=-1)
            {
                return new clsLocalDLApplication(LocalAppID, AppID, clsLicenseClasses.FindLicenseClass(LicenseClassID),PassedTests);
            }
            else
            {
                return null;

            }
        }


        private bool _AddLocalDLApplication()
        {
            ApplicationID = clsApplicationData.AddApplication(Applicant.PersonID, ApplicationDate, ApplicationType.ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate, ApplicationFees, CreatedBy.UserID);
            if (ApplicationID != -1)
            {
                LocalApplicationID = clsApplicationData.AddLocalApplication(ApplicationID, LicenseClass.LicenseClassID);
                return LocalApplicationID != -1;
            }

            return false;

        }

        private bool _UpdateLocalDlApplication()
        {
            return false;
        }

        public bool Save()
        {
            switch (_operation)
            {
                case eOperation.Add:
                    if (_AddLocalDLApplication())
                    {
                        _operation = eOperation.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case eOperation.Update:
                    return _UpdateLocalDlApplication();
            }


            return false;
        }

        public static bool IsApplicationExistByLicenseClass(int PersonID, int LicenseClassID)
        {
            return clsApplicationData.IsApplicationExists(PersonID, LicenseClassID);
        }

        public static bool CancelLocalApplication(int LocalAppID)
        {
            return clsApplicationData.CancelLocalApplication(LocalAppID);

        }

        public static bool IsThereAnActiveScheduledTest(int LocalAppID, int TestTypeID)
        {
            return clsLocalDLApplicationData.IsThereAnActiveScheduledTest(LocalAppID, TestTypeID);
        }

        public static bool DoesPassTestType(int LocalAppID, int TestTypeID)
        {
            return clsLocalDLApplicationData.DoesPassTestType(LocalAppID, TestTypeID);
        }

        public static bool DoesAttendTestType(int LocalAppID, int TestTypeID)
        {
            return clsLocalDLApplicationData.DoesAttendTestType(LocalAppID, TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalAppID, int TestTypeID)
        {
            return clsLocalDLApplicationData.TotalTrialsPerTest(LocalAppID, TestTypeID);
        }
      
    }
}
