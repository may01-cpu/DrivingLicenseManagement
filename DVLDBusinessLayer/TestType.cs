using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeName { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal Fee { get; set; }      


        private clsTestType(int id, string title,string description, decimal fee)
        {
            TestTypeID = id;
            TestTypeName = title;
            Fee = fee;
            TestTypeDescription = description;
        }
        public static DataTable ListAllTestTypes()
        {
            return clsTestTypesData.ListAllTestTypes();
        }

        public static clsTestType FindTestType(int ID)
        {
            string title = "";
            string description = "";
            decimal fee = 0;
            if (clsTestTypesData.GetTestTypeByID(ID,ref title,ref description,ref fee))
            {
                return new clsTestType(ID, title,description, fee);
            }
            else
            {
                return null;
            }

        }

        public bool UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType(this.TestTypeID, this.TestTypeName, this.TestTypeDescription, this.Fee);
        }
    }
}
