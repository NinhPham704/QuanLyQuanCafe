using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private string food_Name;

        private int food_Id;

        private int foodcategory_Id;

        private float price;

        public Food(string food_Name, int food_Id, int foodcategory_Id, float price)
        {
            this.Food_Name = food_Name;
            this.Food_Id = food_Id;
            this.Foodcategory_Id = foodcategory_Id;
            this.Price = price;
        }

        public Food(DataRow row)
        {
            this.Food_Name = (string)row["Food_Name"];
            this.Food_Id = (int)row["Food_Id"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
            this.Foodcategory_Id = (int)row["Foodcategory_Id"];
        }

        public string Food_Name { get => food_Name; set => food_Name = value; }
        public int Food_Id { get => food_Id; set => food_Id = value; }
        public int Foodcategory_Id { get => foodcategory_Id; set => foodcategory_Id = value; }
        public float Price { get => price; set => price = value; }
    }
}
