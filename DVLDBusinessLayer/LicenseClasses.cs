using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public short MinAllowedAge { get; set; }
        public short ValidityLenght { get; set; }
        public decimal ClassFees { get; set; }



        public static DataTable ListAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

        public static clsLicenseClasses FromDataRow(DataRow row)
        {
            return new clsLicenseClasses
            {
                LicenseClassID = Convert.ToInt32(row["LicenseClassID"]),
                ClassName = row["ClassName"].ToString(),
                Description = row["ClassDescription"].ToString(),
                MinAllowedAge = Convert.ToInt16(row["MinimumAllowedAge"]),
                ValidityLenght = Convert.ToInt16(row["DefaultValidityLength"]),
                ClassFees = Convert.ToDecimal(row["ClassFees"])
            };
        }


        public static clsLicenseClasses FindLicenseClass(int ID)
        {
            string className = "";
            string description = "";
            short minAge = 0;
            short validityLength = 0;
            decimal classFees = 0;
            if (clsLicenseClassesData.GetLicenseClassByID(ID, ref className, ref description, ref minAge, ref validityLength, ref classFees))
            {
                return new clsLicenseClasses
                {
                    LicenseClassID = ID,
                    ClassName = className,
                    Description = description,
                    MinAllowedAge = minAge,
                    ValidityLenght = validityLength,
                    ClassFees = classFees
                };
            }
            else
            {
                return null;
            }
        }
    }
}
