using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        public BillDAO() { }

        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if(instance==null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }
        /// <summary>
        /// thành công trả về Id
        /// thất bại trả về -1
        /// </summary>

        public int GetUnCheckBillIdByTableId(int TableId)
        {

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.Bill WHERE dbo.Bill.Table_Id = " + TableId + " AND dbo.Bill.Bill_Status = N'Chưa thanh toán'");
            if(dataTable.Rows.Count > 0) 
            {
                Bill bill = new Bill(dataTable.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

        public void CheckOut(int billId, int discount, float totalPrice)
        {
            string query = " UPDATE  dbo.Bill SET DateCheckOut = GETDATE() , dbo.Bill.Bill_Status = N'Đã thanh toán ' , " + " TotalPrice = " + totalPrice + " , dbo.Bill.discount = " + discount + " WHERE dbo.Bill.Bill_Id = " + billId;
            DataProvider.Instance.ExcuteNoneQuery(query);
        }

        public void InsertBill(int IdTable)
        {
            DataProvider.Instance.ExcuteNoneQuery("EXEC USP_INSERT_BILL @IDTABLE", new object[] {IdTable});
        }
        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("SELECT Max(b.Bill_Id) FROM dbo.Bill b");
            }
            catch
            {
                return 1;
            }
        }
        public DataTable GetListBillByDate(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExcuteQuery("exec USP_GETLISTBILLBYDATE @checkin , @checkout",new object[] {checkin,checkout});
        }
        public DataTable GetListBillByDateandPage(DateTime checkin, DateTime checkout,int page)
        {
            return DataProvider.Instance.ExcuteQuery("exec UDP_GetListBillByDateAndPage @checkin , @checkout , @page", new object[] { checkin, checkout, page });
        }
        public int GetNumBillByDate(DateTime checkin, DateTime checkout)
        {
            return (int)DataProvider.Instance.ExcuteScalar("exec USP_GETNUMBERBILLBYDATE @checkin , @checkout", new object[] { checkin, checkout });
        }
    }
}
