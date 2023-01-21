using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        private string userName;
        private string password;
        private string userDisplayName;
        private int userType;

        public Account(string userName,string userDisplayName , string password, int userType)
        {
            this.UserName= userName;
            this.UserDisplayName= userDisplayName;
            this.Password= password;
            this.UserType= userType;
        }

        public Account(DataRow dataRow)
        {
            this.UserName = dataRow["UserName"].ToString();
            this.UserDisplayName = dataRow["UserDisplayName"].ToString();
            this.Password = dataRow["UserPassword"].ToString();
            this.UserType = (int)dataRow["userType"];
        }

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string UserDisplayName { get => userDisplayName; set => userDisplayName = value; }
        public int UserType { get => userType; set => userType = value; }
    }
}
