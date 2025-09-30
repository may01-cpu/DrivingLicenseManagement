using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDLApplication:clsApplication
    {
        public int LocalApplicationID {  get; set; }
        public clsLicenseClasses LicenseClass {  get; set; }

        private enum eOperation { Add, Update }
        private eOperation _operation;
        public clsLocalDLApplication()
        {
            LocalApplicationID = -1;
            LicenseClass= new clsLicenseClasses();
            _operation = eOperation.Add;
         
        }
        public clsLocalDLApplication(int LicenseClass)
        {
            
            //this.LicenseClass=clsLicenseClasses.;
            _operation = eOperation.Update;
         
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


    }
}
