using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DTO
{
    public class Account
    {
        private string userName;
        private string passWord;
        private int type;

        public string UserName { get => userName; set => userName = value; }
        public int Type { get => type; set => type = value; }
        public string PassWord { get => passWord; set => passWord = value; }

        public Account(string userName , int type, string passWord)
        {
            this.UserName = userName;
            this.PassWord = passWord;
            this.type = type;
        }
        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.PassWord = row["passWord"].ToString();
            this.type = (int)row["type"];
        }
    }
}
