using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            set { instance = value; }
        }

        public MenuDAO() { }

        public List<Menu> GetListMenuByTableId(int TableId)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "SELECT f.Food_Name, bi.Count, f.Price, f.Price*bi.Count AS TotalPrice  FROM dbo.Bill b ,dbo.BillInfo bi, dbo.Food f WHERE bi.Bill_Id = b.Bill_Id AND bi.Food_Id = f.Food_ID And b.Bill_Status = N'Chưa thanh toán' AND b.Table_Id = " + TableId;

            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                Menu menu = new Menu(row);

                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
