using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        private int id;

        private DateTime? datetimecheckIn;

        private DateTime? datetimecheckOut;

        private string status;

        private int discount;
        public int Id { get => id; set => id = value; }
        public DateTime? DatetimecheckIn { get => datetimecheckIn; set => datetimecheckIn = value; }
        public DateTime? DatetimecheckOut { get => datetimecheckOut; set => datetimecheckOut = value; }
        public string Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime? datetimecheckIn, DateTime? datetimecheckOut, string status, int discount)
        {
            this.Id = id;
            this.DatetimecheckIn = datetimecheckIn;
            this.DatetimecheckOut = datetimecheckOut;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row) 
        {
            this.Id = (int)row["Bill_Id"];
            this.DatetimecheckIn = (DateTime?)row["DateCheckIn"];

            var datetimecheckouttemp = row["DateCheckOut"];
            if(datetimecheckouttemp.ToString() !="")
                this.DatetimecheckOut = (DateTime?)datetimecheckouttemp;

            this.Status = (string)row["Bill_Status"];
            this.Discount = (int)row["Discount"];
        }
    }
}
