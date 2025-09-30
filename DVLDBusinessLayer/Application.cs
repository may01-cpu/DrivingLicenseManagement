using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum eAppStatus {New=1 ,Completed=3 , Canceled=2}
        public int ApplicationID { get; set; }  
        public clsPeople Applicant { get; set; }
        public DateTime ApplicationDate { get; set; }
        public clsApplicationType ApplicationType { get; set; }
        public eAppStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal ApplicationFees { get; set; }
        public clsUser CreatedBy { get; set; }

    
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

    }
}
