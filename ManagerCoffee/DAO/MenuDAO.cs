using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set { instance = value; }
        }
        private MenuDAO() { }
        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> Listmenus = new List<Menu>();
            string query = "select f.Name,bf.Count,f.Price, f.Price * bf.Count as totaPrice from Bill b , BillInfo bf ,Food f where  b.ID = bf.IdBill and bf.IdFood = f.ID and Status = 0 and b.IdTable =" + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow row in data.Rows)
            {
                Menu menu = new Menu(row);
                Listmenus.Add(menu);
            }
            return Listmenus;
        }
    }
}
