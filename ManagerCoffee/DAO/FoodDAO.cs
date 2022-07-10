using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            private set { instance = value; } 
        }
        private FoodDAO() { }

        public List<Food> GetListFoodByCategoryID(int id)
        {
            List<Food> foods = new List<Food>();
            string query = "select * from Food where IdCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Food food = new Food(item);
                foods.Add(food);
            }
            return foods;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("select * from Food where dbo.fuConvertToUnsign1(Name) like N'%' +  dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("insert into Food(Name,IdCategory,Price) values (N'{0}', {1}, {2})", name , id , price );
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int idfodd, string name, int id, float price)
        {
            string query = string.Format("update Food set Name = N'{0}' , IdCategory = {1}, Price = {2} where ID = {3}", name, id, price, idfodd);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int id)
        {
            BilllInfoDAO.Instance.DeleteBillInfoByFoodID(id);
            string query = string.Format("delete dbo.Food where ID = {0} ", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
