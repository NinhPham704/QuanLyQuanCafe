using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private  string connectionStr = "Data Source=NINHPHAM\\PHAMNINH;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set {instance = value; }
        }

        private DataProvider() 
        { 
        }
        // trả về kết quả là một bảng
        public DataTable ExcuteQuery(string Query, object[] parameter = null)
        {
            DataTable dataTable = new DataTable();
            using(SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                if(parameter != null)
                {
                    string[] listPara = Query.Split(' ');

                    int i = 0;

                    foreach(string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);

                connection.Close();
            }

            return dataTable;
        }

        // trả về kết quả là số lượng query thành công chỉ dùng cho insert hoặc update
        public int ExcuteNoneQuery(string Query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                if (parameter != null)
                {
                    string[] listPara = Query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }
        // trả về kết quả khi thực hiện query count(*)
        public object ExcuteScalar(string Query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(Query, connection);
                if (parameter != null)
                {
                    string[] listPara = Query.Split(' ');

                    int i = 0;

                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteScalar();

                connection.Close();
            }

            return data;
        }

    }
}
