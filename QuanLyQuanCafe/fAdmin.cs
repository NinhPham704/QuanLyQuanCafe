using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        private Account account;
        #region property
        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account Account
        {
            get => account;
            set => account = value;
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }
        public event EventHandler insertTable;
        private event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        public event EventHandler deleteTable;
        private event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
        #endregion
        public fAdmin(Account acc)
        {
            this.Account= acc;
            InitializeComponent();
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;
            dtgvAccount.DataSource = accountList;
            LoadListBillDefault();
            LoadListFood();
            LoadListCategory();
            LoadListTable();
            LoadListAccount();
            LoadcategoryFood(cbFoodCategory);
            Databinding();
        }
        #region methods
        void LoadListBillDefault()
        {
            DateTime today = DateTime.Today;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryFoodDAO.Instance.GetCategoryFood();
        }
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
        }
        void LoadListAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void Databinding()
        {
            txbFoodName.DataBindings.Add(new Binding( "Text", dtgvFood.DataSource, "Food_Name",true,DataSourceUpdateMode.Never));
            nmPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Food_Id", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryId.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "userName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "userDisplayName", true, DataSourceUpdateMode.Never));
            nmAccountype.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "userType", true, DataSourceUpdateMode.Never));
        }
        void LoadcategoryFood(ComboBox cb)
        {
            List<CategoryFood> categoryFoods = CategoryFoodDAO.Instance.GetCategoryFood();
            cb.DataSource = categoryFoods;
            cb.DisplayMember= "Name";
        }
        private List<string> GetListNameCategory()
        {
            List<string> result = new List<string>();

            List<CategoryFood> categoryFoods = CategoryFoodDAO.Instance.GetCategoryFood();

            foreach(var item in categoryFoods)
            {
                result.Add(item.Name);
            }

            return result;
        }
        private List<Food> SearchFoodbyName(string name)
        {
            return FoodDAO.Instance.SearchFoodbyName(name);
        }
        #endregion
        #region events
        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void LoadListBillByDate(DateTime checkin, DateTime checkout)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(checkin, checkout);
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(dtgvFood.SelectedCells.Count> 0 && (dtgvFood.SelectedCells[0].OwningRow.Cells["FoodCategory_Id"].Value)!=null)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["FoodCategory_Id"].Value;
                    cbFoodCategory.SelectedIndex = id-1;
                }
            }
            catch { }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int id = (cbFoodCategory.SelectedItem as CategoryFood).Id;
            float price = (float)nmPrice.Value;
            if (FoodDAO.Instance.InsertFood(name, id, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                deleteFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm món thất bại");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            string name = txbFoodName.Text;
            int id = (cbFoodCategory.SelectedItem as CategoryFood).Id;
            float price = (float)nmPrice.Value;
            int idfood = Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.UpdateFood(idfood,name, id, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                updateFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa món thất bại");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idfood = Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.DeleteFood(idfood))
            {
                MessageBox.Show("Xoá món thành công");
                LoadListFood();
                deleteFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá món thất bại");
            }
        }
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadcategoryFood(cbFoodCategory);
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            if (GetListNameCategory().Contains(name))
            {
                MessageBox.Show("Danh mục này đã tồn tại");
                return;
            }
            else
            {
                if (CategoryFoodDAO.Instance.InsertCategoryFood(name))
                {
                    MessageBox.Show("Thêm thành công danh mục đồ uống mới.");
                    LoadListCategory();
                    insertCategory?.Invoke(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Không thể thêm đồ uống mới.");
                }
            }
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int idcategory = Convert.ToInt32(txbCategoryId.Text);
            if (CategoryFoodDAO.Instance.DeleteCategoryFood(idcategory))
            {
                MessageBox.Show("Xoá thành công danh mục đồ uống.");
                LoadListCategory();
                deleteCategory?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Không thể xoá đồ uống mới.");
            }
        }
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int idcategory = Convert.ToInt32(txbCategoryId.Text);
            string name = txbCategoryName.Text;
            if (GetListNameCategory().Contains(name))
            {
                MessageBox.Show("Danh mục này đã tồn tại");
                return;
            }
            else
            {
                if (CategoryFoodDAO.Instance.UpdateCategoryFood(idcategory, name))
                {
                    MessageBox.Show("Sửa thành công danh mục đồ uống mới.");
                    LoadListCategory();
                    updateCategory?.Invoke(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Không thể sửa đồ uống này.");
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodbyName(txbSearchFood.Text);
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (TableDAO.Instance.InsertTable())
            {
                MessageBox.Show("thêm bàn thành công");
                TableDAO.Instance.UpdateNameTable();
                LoadListTable();
                insertTable.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bai");
            }
        }
        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);
            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xoá bàn thành công");
                TableDAO.Instance.UpdateNameTable();
                LoadListTable();
                deleteTable?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá bàn thất bại");
            }
        }
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();

        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (int)nmAccountype.Value;
            if (AccountDAO.Instance.InsertAccount(username, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            if (AccountDAO.Instance.DeleteAccount(username))
            {
                MessageBox.Show("Xoá tài khoản thành công");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại");
            }
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int type = (int)nmAccountype.Value;
            if (AccountDAO.Instance.UpdateAccount(username, displayname, type))
            {
                MessageBox.Show("Sửa tài khoản thành công");
                LoadListAccount();
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại");
            }
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            if(AccountDAO.Instance.ResetAccount(username))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            txbPage.Text = "";
            txbPage.Text = "1";
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (txbPage.Text == "1")
            {
                MessageBox.Show("Đã là trang đầu tiên");
            }
            else
            {
                txbPage.Text = (Convert.ToInt32(txbPage.Text) - 1).ToString();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {

            txbPage.Text = (Convert.ToInt32(txbPage.Text) + 1).ToString();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int lastpage = 0;
            if (BillDAO.Instance.GetNumBillByDate(dtpkFromDate.Value, dtpkToDate.Value) % 10 == 0)
            {
                lastpage = BillDAO.Instance.GetNumBillByDate(dtpkFromDate.Value, dtpkToDate.Value) / 10;
            }
            else
            {
                lastpage = BillDAO.Instance.GetNumBillByDate(dtpkFromDate.Value, dtpkToDate.Value) / 10 + 1;
            }
            txbPage.Text = lastpage.ToString();
        }

        private void txbPage_TextChanged(object sender, EventArgs e)
        {
            if (txbPage.Text == "0" || txbPage.Text == "")
            {
                return;
            }
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDateandPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPage.Text));
        }
        #endregion
    }
}
