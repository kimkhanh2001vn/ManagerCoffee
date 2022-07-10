using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
    public class BillDAO
    {
        private static BillDAO instanre;

        public static BillDAO Instanre {
            get { if (instanre == null) instanre = new BillDAO(); return instanre; }
            private set { instanre = value; } 
        }
        public BillDAO() { }

        /// <summary>
        /// thanh cong tra ve ID cua Bill
        /// that bai tra ve trong
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckBillIdByTableId(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Bill where IdTable = '" + id + "' and Status = 0");
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CkeckOut(int id, int discount , float totalPrice)
        {
            string query = "update Bill set DateCheckOut = GETDATE() ,  Status = 1, " + " discount = "+ discount + ", totalPrice = " + totalPrice + " where ID = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public DataTable GetBillListByDate(DateTime CheckIn , DateTime CheckOut)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @CheckIn , @CheckOut", new object[] { CheckIn , CheckOut });
        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteQuery("exec USP_InsertBill @IdTable" , new object[] {id});
        }
        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(ID) from Bill"); ;
            }
            catch
            {
                return 1;
            }            
        }
    }
}
