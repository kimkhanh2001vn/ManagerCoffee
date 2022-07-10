using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DTO
{
    public class Category
    {
        private string name;
        private int iD;

        public string Name { get => name; set => name = value; }
        public int ID { get => iD; set => iD = value; }
        public Category(int id , string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public Category(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = (string)row["name"];
        }
    }
}
