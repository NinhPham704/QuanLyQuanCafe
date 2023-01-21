using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        private Account account;
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }
        public Account Account
        {
            get => account;
            set { account = value; ChangeAccount(Account); }
        }
        public fAccountProfile(Account account)
        {
            InitializeComponent();
            this.Account = account;
        }

        void ChangeAccount(Account account)
        {
            txbUserName.Text = Account.UserName;
            txbDisplayName.Text = Account.UserDisplayName;
        }

        void UpdateAccountInfo()
        {
            string displayName = txbDisplayName.Text;
            string passWord = txbPassword.Text;
            string newpassWord = txbNewPasswrod.Text;
            string renewpassWord = txbRenewPasswrod.Text;
            string userName = txbUserName.Text;

            if (!renewpassWord.Equals(newpassWord))
            {
                MessageBox.Show("Bạn vui lòng nhập lại thông tin!");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(userName,displayName,passWord,newpassWord))
                {
                    MessageBox.Show("Cập nhật thành công");
                    updateAccount?.Invoke(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bai, xin vui lòng nhập lại");
                }
            }
        }

        private void btnExits_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }
        public AccountEvent(Account acc)
        {
            this.Acc= acc;
        }
    }
}
