using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryFoodDAO
    {
        private static CategoryFoodDAO instance;

        public static CategoryFoodDAO Instance 
        {
            get { if (instance == null) instance = new CategoryFoodDAO();return instance; }
            set { instance = value; }
        }
        public CategoryFoodDAO() { }

        public List<CategoryFood> GetCategoryFood()
        {
            List<CategoryFood> categoryFoods= new List<CategoryFood>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.FoodCategory fc");

            foreach(DataRow row in dataTable.Rows)
            {
                CategoryFood cate = new CategoryFood(row);
                categoryFoods.Add(cate);
            }

            return categoryFoods; ;
        }
        public bool InsertCategoryFood(string name)
        {
            string query = string.Format("INSERT dbo.FoodCategory ( FoodCategory_Name) VALUES ( N'{0}')", name);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteCategoryFood(int id)
        {
            string query = string.Format("DELETE dbo.FoodCategory WHERE dbo.FoodCategory.FoodCategory_Id = {0} ", id);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool UpdateCategoryFood(int id, string name)
        {
            string query = string.Format("UPDATE dbo.FoodCategory SET dbo.FoodCategory.FoodCategory_Name = N'{0}' WHERE dbo.FoodCategory.FoodCategory_Id = {1} ",name ,id);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }

    }
}
