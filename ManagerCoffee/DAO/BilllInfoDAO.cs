using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
    public class BilllInfoDAO
    {
        private static BilllInfoDAO instance;

        public static BilllInfoDAO Instance { 
            get { if (instance == null) instance = new BilllInfoDAO(); return instance; }  
            private set { instance = value; } 
        }
        public BilllInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> ListBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from BillInfo where IdBill = " + id );
            foreach (DataRow row in data.Rows)
            {
                BillInfo info = new BillInfo(row);
                ListBillInfo.Add(info);
            }
            return ListBillInfo;
        }
        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.BillInfo where IdFood =" + id);
        }
        public void InsertBillInfo(int idbill,int idfood,int count)
        {
            DataProvider.Instance.ExecuteQuery("exec USP_InsertBillInfo @idbill , @idfood , @count",new object[] {idbill, idfood, count });
        }
    }
}
