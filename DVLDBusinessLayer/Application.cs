using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum eAppStatus { New = 1, Completed = 3, Canceled = 2 }
        public int ApplicationID { get; set; }
        public clsPeople Applicant { get; set; }
        public DateTime ApplicationDate { get; set; }
        public clsApplicationType ApplicationType { get; set; }
        public eAppStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal ApplicationFees { get; set; }
        public clsUser CreatedBy { get; set; }

        private enum eOpType { AddNewApplication, updateApplication }
        private eOpType OperationType = eOpType.AddNewApplication;
        public clsApplication()
        {
            ApplicationID = -1;
            Applicant = new clsPeople();
            ApplicationDate = DateTime.Now;
            ApplicationType = new clsApplicationType();
            ApplicationStatus = eAppStatus.New;
            LastStatusDate = DateTime.Now;
            ApplicationFees = 0;
            CreatedBy = clsUser.LoggedInUser;

        }

        private clsApplication(int appID, clsPeople applicant, DateTime appDate, clsApplicationType appType,
            eAppStatus appStatus, DateTime lastStatusDate, decimal appFees, clsUser createdBy)
        {
            ApplicationID = appID;
            Applicant = applicant;
            ApplicationDate = appDate;
            ApplicationType = appType;
            ApplicationStatus = appStatus;
            LastStatusDate = lastStatusDate;
            ApplicationFees = appFees;
            CreatedBy = createdBy;
        }
        //public clsApplication(int AppID) { }

        public static DataTable ListAllLocalApps()
        {
            return clsApplicationData.ListAllLocalDLApps();
        }

        public static bool IsApplicationExists(int AppID)
        {
            return clsApplicationData.IsApplicationExists(AppID);
        }

        public static bool CancelApplication(int AppID)
        {
            return clsApplicationData.CancelApplication(AppID);
        }

        public static clsApplication FindApplication(int AppID)
        {
            int Applicant = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            Byte ApplicationStatus = ((byte)eAppStatus.New);
            DateTime LastStatusDate = DateTime.Now;
            decimal ApplicationFees = 0;
            int CreatedBy = -1;

            Convert.ToByte(ApplicationStatus).ToString();

            if (clsApplicationData.GetApplicationByID(AppID, ref Applicant, ref ApplicationDate, ref ApplicationTypeID
                , ref ApplicationStatus, ref LastStatusDate, ref ApplicationFees, ref CreatedBy))
            {
                return new clsApplication(AppID, clsPeople.FindPersonByID(Applicant), ApplicationDate,
                    clsApplicationType.FindApplicationType(ApplicationTypeID), (eAppStatus)ApplicationStatus, LastStatusDate, ApplicationFees, clsUser.GetUserByID(CreatedBy));

            }
            else
            {
                return null;
            }
        }


        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddApplication(ApplicationID, ApplicationDate, (int)ApplicationType.ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate, ApplicationFees, CreatedBy.UserID);
            return (this.ApplicationID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.Applicant.PersonID, this.ApplicationDate,
                this.ApplicationType.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.ApplicationFees);
        }
        public bool Save()
        {
            switch (OperationType)
            {
                case eOpType.AddNewApplication:
                    if (_AddNewApplication())
                    {

                        OperationType = eOpType.updateApplication;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eOpType.updateApplication:


                    return _UpdatePerson();

            }




            return false;
        }

        public static bool DeleteApplication(int ApplicationID)
        {

            return clsApplicationData.DeleteApplication(ApplicationID);

        }

    }
}
