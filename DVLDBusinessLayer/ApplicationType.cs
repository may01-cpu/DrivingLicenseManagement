using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {
        public int ApplicationTypeID { get; set; }  
        public string ApplicationTypeName { get; set; }
        public decimal Fee { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeName = "";
            Fee = 0;
        }
        private clsApplicationType(int id, string title, decimal fee)
        {
            ApplicationTypeID = id;
            ApplicationTypeName = title;
            Fee = fee;
        }
        public static DataTable ListAllApplicationTypes()
        {
            return clsApplicationTypesData.ListAllAppTypes();
        }

        public static clsApplicationType FindApplicationType(int ID)
        {
            string title = "";
            decimal fee = 0;
            if (clsApplicationTypesData.GetAppTypeByID(ID,ref title,ref fee))
            {
                return new clsApplicationType(ID, title, fee);
            }
            else {                
                return null;
            }
               
        }
    
        public bool UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateAppType(this.ApplicationTypeID, this.ApplicationTypeName, this.Fee);
        }


    }
}
