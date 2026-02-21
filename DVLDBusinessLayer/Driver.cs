using System;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        clsPeople Person { get; set; }
        public int DriverID { get; set; }
        int CreatedByUserID { get; set; }
        DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
        }
        private clsDriver(int driverID, int personId, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            Person = clsPeople.FindPersonByID(personId);
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }
        public static clsDriver FindDriverByID(int driverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.Now;
            if (clsDriverData.GetDriverByID(driverID, ref personID, ref createdByUserID, ref createdDate))
            {
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
            }
            else
            {
                return null;
            }
        }
        public static DataTable ListAllDrivers()
        {
            DataTable dt = clsDriverData.GetAllDrivers();
            return dt;
        }


        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(Person.PersonID, CreatedByUserID, CreatedDate);
            return DriverID != -1;
        }

        public bool Save()
        {
            if (this.DriverID == -1)
            {
                return _AddNewDriver();
            }
            else
            {
                return false;
            }
        }

    }
}
