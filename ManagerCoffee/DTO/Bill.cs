using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DTO
{
    public class Bill
    {
        
        private DateTime? dateCheckOut;
        private DateTime? dateCheckIn;
        private int iD;
        private int status;
        private int disCount;

        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int DisCount { get => disCount; set => disCount = value; }

        public Bill(int id , DateTime? datecheckin , DateTime? datecheckout, int status , int discount)
        {
            this.ID = id;
            this.DateCheckIn = datecheckin;
            this.DateCheckOut = datecheckout;
            this.Status = status;
            this.DisCount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["datecheckin"];
            this.DateCheckOut = (DateTime?)row["datecheckout"];
            this.Status = (int)row["status"];
        }
    }
}
