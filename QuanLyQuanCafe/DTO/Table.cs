using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        public int TableWith = 100;
        public int TableHeight = 100;
        private int id;
        private string name;
        private string status;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }

        public Table (int id, string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow dataRow)
        {
            this.Id = (int)dataRow["TableFood_ID"];
            this.Status = (string)dataRow["TableFood_Status"];
            this.Name = (string)dataRow["TableFood_Name"];
        }
    }
}
