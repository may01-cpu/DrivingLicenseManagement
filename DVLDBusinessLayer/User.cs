using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPeople Person { get; set; }

        enum eOpType { AddUser, UpdateUser }
        private eOpType _OperationType;

        // Constructor for new user
        public clsUser()
        {
            this.UserID = -1;
            this.IsActive = true;
            this.Username = "";
            this.Password = "";
            _OperationType = eOpType.AddUser;
        }
        private clsUser(int userID, int personID, string username, string password, bool isActive)
        {
            UserID = userID;
            Password = password;
            Username = username;
            IsActive = isActive;
            Person = clsPeople.FindPersonByID(personID);
            _OperationType = eOpType.UpdateUser;

        }

        public static clsUser AuthenticateUser(string username, string password)
        {
            int userID = -1;
            int personID = -1;
            bool isActive = false;

            if (clsUsersData.FindUser(username, password, ref isActive, ref userID, ref personID))
            {
                return new clsUser(userID, personID, username, password, isActive);
            }
            else
            {
                return null;
            }

        }

        public static DataTable ListAllUsers()
        {
            DataTable dt = clsUsersData.GetAllUsers();
            dt.Columns.Add("FullName", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["FullName"] = row["FirstName"].ToString() + " " + row["SecondName"].ToString() + " " + row["ThirdName"].ToString() + " " + row["LastName"].ToString() + " ";
            }
            return dt;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(Username, Password, IsActive, Person.PersonID);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(UserID, Username, Password, IsActive);
        }
        public bool Save()
        {
            switch (_OperationType)
            {
                case eOpType.AddUser:
                    if (_AddNewUser())
                    {

                        _OperationType = eOpType.UpdateUser;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eOpType.UpdateUser:


                    return _UpdateUser();

            }




            return false;
        }

        public static bool DeleteUser(int UserID)
        {

            return clsUsersData.DeleteUser(UserID);

        }

        public static bool IsUserExists(int UserID)
        {
            return clsUsersData.IsUserExist(UserID);
        }

        public static bool IsUserExistsByPersonID(int PersonID)
        {
            return clsUsersData.IsUserExist(PersonID);
        }
        public static clsUser GetUserByID(int UserID)
        {
            int personID = -1;
            string username = "";
            string password = "";
            bool isActive = false;
            if (clsUsersData.GetUserByID(UserID, ref username, ref password, ref personID, ref isActive))
            {
                return new clsUser(UserID, personID, username, password, isActive);
            }
            else
            {
                return null;
            }
        }
   
        public bool ChangePassword(string Password)
        {
            return clsUsersData.ChangePassword(this.UserID, Password);
        }
    
    
    }
}
