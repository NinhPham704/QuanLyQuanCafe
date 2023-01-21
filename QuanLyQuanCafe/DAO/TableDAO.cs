using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public TableDAO() { }

        public static TableDAO Instance 
        {
            get { if(instance==null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }

        public List<Table> LoadTableList()
        {
            List<Table> listTable = new List<Table>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery("USP_GetTableList");
            foreach(DataRow dataRow in dataTable.Rows)
            {
                Table table = new Table(dataRow);
                listTable.Add(table);
            }

            return listTable;
        }
        int GetMaxTableId()
        {
            return DataProvider.Instance.ExcuteQuery("USP_GetTableList").Rows.Count;
        }
        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExcuteQuery ("USP_SWITCHTABLE @idtable1 , @idtable2", new object[] { id1, id2 });
        }
        public bool InsertTable()
        {
            int newid = GetMaxTableId()+1;
            string query = string.Format("INSERT dbo.TableFood( TableFood_Name, TableFood_Status) VALUES ( N'Bàn {0}', N'Bàn trống')", newid);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool DeleteTable(int id)
        {
            string query = string.Format("DELETE dbo.TableFood WHERE dbo.TableFood.TableFood_ID = {0} ", id);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public void UpdateNameTable()
        {
            List<Table> tables = LoadTableList();
            int index = 1;
            foreach(Table item in tables)
            {
                string query = string.Format("UPDATE dbo.TableFood SET dbo.TableFood.TableFood_Name = N'Bàn {0}' WHERE TableFood.TableFood_ID = {1} ",index,item.Id);
                DataProvider.Instance.ExcuteNoneQuery(query);
                index++;
            }
        }
    }
}
