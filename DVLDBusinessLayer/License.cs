using DVLDDataAccessLayer;
using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        protected enum enMode { Add, Edit }
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsDriver Driver { get; set; }
        public clsLicenseClasses LicenseClasses { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public string IssueReason { get; set; }
        public clsUser CreatedUser { get; set; }
        protected enMode _OperationMode;
        //add operation
        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            Driver = new clsDriver();
            LicenseClasses = new clsLicenseClasses();
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReason = "";
            CreatedUser = new clsUser();
            _OperationMode = enMode.Add;
        }
        private clsLicense(int licenseID, int applicationID, int driverId, clsLicenseClasses licenseClasses, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, string issueReason, int createdUser, enMode operationMode)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            Driver = clsDriver.FindDriverByID(driverId);
            LicenseClasses = licenseClasses;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedUser = clsUser.GetUserByID(createdUser);
            _OperationMode = operationMode;
        }
        //public static DataTable ListAllLicenses()
        //{
        //    return clsLicenseData.ListAllLicenses();
        //}

        public static clsLicense GetLicenseByID(int ID)
        {
            int AppID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            int UserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "", IssueReason = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            if (clsLicenseData.GetLicenseInfoByID(ID, ref AppID, ref DriverID, ref LicenseClassID, ref UserID, ref IssueDate, ref ExpirationDate, ref Notes, ref IssueReason, ref PaidFees, ref IsActive))
            {
                return new clsLicense(
                    ID,
                    AppID,
                    DriverID,
                    clsLicenseClasses.FindLicenseClass(LicenseClassID),
                    IssueDate,
                    ExpirationDate,
                    Notes,
                    PaidFees,
                    IsActive,
                    IssueReason,
                    UserID,
                    enMode.Edit
                    );
            }
            else
            {
                return null;
            }
        }


        public bool AddNewLicense()
        {

            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.Driver.DriverID, this.LicenseClasses.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedUser.UserID);
            return this.LicenseID != -1;
        }
    }
}