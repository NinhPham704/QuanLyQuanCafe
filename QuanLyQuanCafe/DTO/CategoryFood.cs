using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class CategoryFood
    {
        private string name;

        private int id;

        public CategoryFood(string Name, int Id) 
        {
            this.Name = Name;
            this.Id = Id;
        }

        public CategoryFood(DataRow row)
        {
            this.Name = (string)row["FoodCategory_Name"];
            this.Id = (int)row["FoodCategory_Id"];
        }        

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}
