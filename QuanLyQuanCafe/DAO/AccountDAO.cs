using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public AccountDAO() { }

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        public bool UpdateAccount(string username, string displayname, string password, string newpassword)
        {
            int result = DataProvider.Instance.ExcuteNoneQuery("USP_UPDATEACCOUNT @USERNAME , @USERDISPLAYNAME , @USERPASSWORD , @NEWPASSWORD ", new object[] {username,displayname,password,newpassword});
            return result > 0;
        }

        public bool Login(string userName, string passWord)
        {
            string query = "EXEC USP_LOGIN @userName , @password ";

            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] {userName,passWord});
            
            return result.Rows.Count > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.Account a\tWHERE a.UserName = '" + userName + "'");
            foreach(DataRow item in dataTable.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("SELECT a.UserName, a.UserDisplayName, a.UserType FROM dbo.Account a ");
        }
        public bool InsertAccount(string UserName, string UserDisplayName, int type)
        {
            string query = string.Format(" INSERT dbo.Account( UserName, UserDisplayName, UserType)VALUES( N'{0}', N'{1}', {2}) ", UserName, UserDisplayName, type);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteAccount(string UserName)
        {
            string query = string.Format(" DELETE dbo.Account WHERE dbo.Account.UserName = N'{0}' ", UserName);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool UpdateAccount(string UserName, string UserDisplayName, int type)
        {
            string query = string.Format(" UPDATE dbo.Account SET dbo.Account.UserDisplayName = N'{1}', dbo.Account.UserType = {2} where dbo.Account.UserName = N'{0}' ", UserName, UserDisplayName, type);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool ResetAccount(string UserName)
        {
            string query = string.Format(" UPDATE dbo.Account SET dbo.Account.UserPassword = N'0' where dbo.Account.UserName = N'{0}' ", UserName);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
    }
}
