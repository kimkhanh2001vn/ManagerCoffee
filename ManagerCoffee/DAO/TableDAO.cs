﻿using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerCoffee.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }
        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("exec USP_GetTableList");

            foreach(DataRow row in data.Rows)
            {
                Table table = new Table(row);
                tableList.Add(table);
            }
            return tableList;
        }

        public static double TableWidth = 80;
        public static double TableHeight = 80;
    }
}
