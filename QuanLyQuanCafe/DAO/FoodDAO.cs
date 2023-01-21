using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {

        private static FoodDAO instance;
        public FoodDAO() { }

        public List<Food> GetListFoodByCategoryFood(int Id)
        {
            List<Food> listFood = new List<Food>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.Food f WHERE f.FoodCategory_Id = " + Id);

            foreach(DataRow row in dataTable.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }

            return listFood;
        }

        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.Food f ");

            foreach (DataRow row in dataTable.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }

            return listFood;
        }

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            set { instance = value; }
        }
        public bool InsertFood(string name,int idcategory, float price)
        {
            string query = string.Format("INSERT dbo.Food ( Food_Name, FoodCategory_Id, Price) VALUES ( N'{0}', {1}, {2} )", name, idcategory, price);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result >0;
        }
        public bool UpdateFood(int idfood,string name, int idcategory, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET dbo.Food.Food_Name = N'{0}' , dbo.Food.FoodCategory_Id = {1} , dbo.Food.Price = {2} WHERE dbo.Food.Food_ID = {3} ", name, idcategory, price,idfood);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idfood)
        {
            BillInfoDAO.Instance.DeleteBillinfoByIdfood(idfood);
            string query = string.Format(" DELETE dbo.Food WHERE dbo.Food.Food_ID = {0} ",idfood);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }

        public List<Food> SearchFoodbyName(string name)
        {
            List<Food> listFood = new List<Food>();

            string query = string.Format("SELECT * FROM dbo.Food where Food.Food_Name like N'%{0}%' ", name);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }

            return listFood;
        }
    }
}
