using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DTO
{
    public class Table
    {
        private string name;
        private int iD;
        private string status;

        public int ID {
            get { return iD; }
            set { iD = value; }
        }
        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }

        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }

        public Table(int id , string name , string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
    }
}
