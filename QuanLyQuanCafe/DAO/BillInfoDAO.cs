using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        { 
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { instance = value; }
        }

        public BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int BillId)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("SELECT * FROM dbo.BillInfo bi WHERE bi.Bill_Id = " + BillId);

            foreach(DataRow item in dataTable.Rows)
            {
                BillInfo billInfo = new BillInfo(item);
                listBillInfo.Add(billInfo);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int  IdBill, int IdFood, int Count)
        {
            DataProvider.Instance.ExcuteNoneQuery(" EXEC USP_INSERT_BillInfo @Bill_Id , @Food_Id , @Count ", new object[] { IdBill, IdFood, Count });
        }
        public void DeleteBillinfoByIdfood(int IdFood)
        {
            string query = string.Format(" DELETE dbo.BillInfo WHERE dbo.BillInfo.Food_Id = {0} ", IdFood);
            DataProvider.Instance.ExcuteNoneQuery(query);
        }
    }
}
