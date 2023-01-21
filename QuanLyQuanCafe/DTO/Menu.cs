using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Menu
    {
        public Menu(string FoodName,int Count,float Price, float TotalPrice )
        {
            this.FoodName = FoodName;
            this.Count = Count;
            this.Price = Price;
            this.TotalPrice = TotalPrice;
        }

        public Menu(DataRow row)
        {
            this.FoodName = (string)row["FooD_Name"];
            this.Count = (int)row["Count"];
            this.Price = (float)(Convert.ToDouble(row["Price"].ToString()));
            this.TotalPrice = (float)(Convert.ToDouble(row["TotalPrice"].ToString()));
        }

        private float price;

        private float totalPrice;

        private int count;

        private string foodName;

        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Count { get => count; set => count = value; }
        public string FoodName { get => foodName; set => foodName = value; }
    }
}
