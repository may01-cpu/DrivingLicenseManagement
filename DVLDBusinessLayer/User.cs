using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DVLDBusinessLayer
{
    public class clsUser : clsPeople
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static string UserPath = @"Remember-User.txt";
        public static void RememberUser(string username, string password)
        {

            string userData = $"{username}:{password}";
            File.WriteAllText(UserPath, userData + Environment.NewLine);
    
                
         }
        private clsUser(int userID, int personID, string username, string password, bool isActive)
        {
            UserID = userID;
            Password = password;
            Username = username;
            IsActive = isActive;
            PersonID = personID;
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
  
    
    
    }
}
