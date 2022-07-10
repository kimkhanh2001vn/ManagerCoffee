using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
   public class DataProvider
    {
        private static DataProvider instance;
        private string ConnectionSQL = @"Data Source=NGOKIMKHANH\SQLEXPRESS;Initial Catalog=Ql_QuanCafe;Integrated Security=True";

        public static DataProvider Instance {
            get { if (instance == null) instance = new DataProvider(); return instance; } 
            private set { DataProvider.instance = value; }
        }
        private DataProvider()
        {

        }
        public DataTable ExecuteQuery(string query , object[] paramater = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionSQL))
            {
                connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    if (paramater != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains("@"))
                            {
                                command.Parameters.AddWithValue(item, paramater[i]);
                                i++;
                            }
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(data);
                
                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] paramater = null)
        {
            int data;
            
            using (SqlConnection connection = new SqlConnection(ConnectionSQL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, paramater[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                

                connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] paramater = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionSQL))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                if (paramater != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, paramater[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }

}
