using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DTO
{
    public class BillInfo
    {
        private int iD;
        private int iDBill;
        private int iDFood;
        private int count;

        public int IDBill { get => iDBill; set => iDBill = value; }
        public int IDFood { get => iDFood; set => iDFood = value; }
        public int Count { get => count; set => count = value; }
        public int ID { get => iD; set => iD = value; }

        public BillInfo(int id,int idbill,int idfood,int count)
        {
            this.ID = id;
            this.IDBill = idbill;
            this.IDFood = idfood;
            this.Count = count;
        }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IDBill = (int)row["idbill"];
            this.IDFood = (int)row["idfood"];
            this.Count = (int)row["count"];
        }
    }
}
