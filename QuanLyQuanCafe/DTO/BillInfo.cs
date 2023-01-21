using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfo
    {

        public BillInfo(int id,int billId, int foodId, int count)
        {
            this.Id = id;
            this.BillId = billId;
            this.FoodId = foodId;
            this.Count = count;
        }

        public BillInfo(DataRow row)
        {
            this.Id = (int)row["BillInfo_Id"];
            this.BillId = (int)row["Bill_Id"];
            this.FoodId = (int)row["Food_Id"];
            this.Count = (int)row["Count"];
        }

        private int id;

        private int billId;

        private int foodId;

        private int count;

        public int Id { get => id; set => id = value; }
        public int BillId { get => billId; set => billId = value; }
        public int FoodId { get => foodId; set => foodId = value; }
        public int Count { get => count; set => count = value; }
    }
}
