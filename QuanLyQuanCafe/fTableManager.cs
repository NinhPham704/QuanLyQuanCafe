using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyQuanCafe.DTO.Menu;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        #region property
        private Account account;

        public Account Account 
        { 
            get => account;
            set { account = value; ChangeAccount(Account.UserType); }
        }

        #endregion
        #region initialize
        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.Account = acc;
            LoadTable();
            LoadCategoryFood();
            loadcomboboxtable(cbSwitchTable);
        }
        #endregion
        #region Methods

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type ==1;
            accountToolStripMenuItem.Text = "Thông tin tài khoản" + "(" + account.UserDisplayName + ")";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach(Table table in tableList)
            {
                Button button = new Button() { Width = table.TableWith, Height = table.TableHeight };
                button.Click += Button_Click;
                button.Tag = table;
                button.Text = table.Name + Environment.NewLine + table.Status;
                if(table.Status == "Bàn trống")
                {
                    button.BackColor = Color.AliceBlue;
                }
                else
                {
                    button.BackColor = Color.BlueViolet;
                }
                flpTable.Controls.Add(button);
            }
        }
        void loadcomboboxtable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember= "Name";
        }
        void LoadCategoryFood()
        {
            List<CategoryFood> categoryFoods= CategoryFoodDAO.Instance.GetCategoryFood();            

            cbCategoryFood.DataSource= categoryFoods;
            cbCategoryFood.DisplayMember = "Name";
            
        }
        void LoadFoodByCategoryId(int Id)
        {
            var listFood = FoodDAO.Instance.GetListFoodByCategoryFood(Id);

            cbFood.DataSource= listFood;
            cbFood.DisplayMember = "Food_Name";
        }
        void showBill(int tableId)
        {
            lsvBill.Items.Clear();

            txbTotalPrice.Clear();

            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTableId(tableId);

            float totalPrice = 0;

            foreach(var item in listBillInfo)
            {
                ListViewItem listViewItem= new ListViewItem(item.FoodName.ToString());

                listViewItem.SubItems.Add(item.Count.ToString());

                listViewItem.SubItems.Add(item.Price.ToString());

                listViewItem.SubItems.Add(item.TotalPrice.ToString());
                lsvBill.Items.Add(listViewItem);

                totalPrice+= item.TotalPrice;

                CultureInfo culture = new CultureInfo("vi-VN");

                txbTotalPrice.Text = totalPrice.ToString("c",culture);
            }
        }
        private void thôngTinPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Người tạo PhamNinh");
        }
        #endregion
        #region Events
        private void Button_Click(object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).Id;
            lsvBill.Tag = (sender as Button).Tag;
            showBill(tableId);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile fAccount = new fAccountProfile(Account);
            fAccount.UpdateAccount += FAccount_UpdateAccount;
            //this.Hide();
            fAccount.ShowDialog();
            this.Show();
        }

        private void FAccount_UpdateAccount(object sender, AccountEvent e)
        {
            accountToolStripMenuItem.Text = "Thông tin tài khoản" + "(" + e.Acc.UserDisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin fAdmin = new fAdmin(Account);
            fAdmin.InsertFood += FAdmin_InsertFood;
            fAdmin.UpdateFood += FAdmin_UpdateFood;
            fAdmin.DeleteFood += FAdmin_DeleteFood;
            fAdmin.InsertCategory += FAdmin_InsertCategory;
            fAdmin.UpdateCategory += FAdmin_UpdateCategory;
            fAdmin.DeleteCategory += FAdmin_DeleteCategory;
            fAdmin.insertTable += FAdmin_insertTable;
            fAdmin.deleteTable += FAdmin_deleteTable;
            fAdmin.ShowDialog();
        }

        private void FAdmin_deleteTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void FAdmin_insertTable(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void FAdmin_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategoryFood();
        }

        private void FAdmin_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategoryFood();
        }

        private void FAdmin_InsertCategory(object sender, EventArgs e)
        {
            LoadCategoryFood();
        }

        private void FAdmin_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategoryFood.SelectedItem as CategoryFood).Id);
            if(lsvBill.Tag != null)
            {
                showBill((lsvBill.Tag as Table).Id);
                LoadTable();
            }
        }

        private void FAdmin_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategoryFood.SelectedItem as CategoryFood).Id);
            if (lsvBill.Tag != null)
            {
                showBill((lsvBill.Tag as Table).Id);
            }
        }

        private void FAdmin_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryId((cbCategoryFood.SelectedItem as CategoryFood).Id);
            if (lsvBill.Tag != null)
            {
                showBill((lsvBill.Tag as Table).Id);
            }
        }

        private void cbCategoryFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            CategoryFood categoryFood = cb.SelectedItem as CategoryFood;

            id = categoryFood.Id;

            LoadFoodByCategoryId(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Bạn cần phải chọn bàn trước.");
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableId(table.Id);

            int idFood = (cbFood.SelectedItem as Food).Food_Id;

            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            showBill(table.Id);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int discount = (int)nmDiscount.Value;

            int billId = BillDAO.Instance.GetUnCheckBillIdByTableId(table.Id);

            float totalPrice = float.Parse(txbTotalPrice.Text.Split(',')[0]);

            if (billId != -1)
            {
                if (MessageBox.Show("Bạn có muốn thanh toán hoá đơn bàn " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(billId, discount, totalPrice * 1000);
                    showBill(table.Id);
                    LoadTable();
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).Id;
            int id2 = (cbSwitchTable.SelectedItem as Table).Id;
            if (MessageBox.Show("bạn có muôn chuyển bàn hay không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                LoadTable();
            }

        }
        #endregion
    }
}
