using ManagerCoffee.DAO;
using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerCoffee
{
    public partial class FrmAdmin : Form
    {
        BindingSource FoodList = new BindingSource();
        BindingSource AccountList = new BindingSource();
        public FrmAdmin()
        {
            InitializeComponent();
            Load();
        }
        #region methos
        List<Food> SearchFoodByName(string name)
        {
            List<Food> Listfoods = FoodDAO.Instance.SearchFoodByName(name);
            return Listfoods;
        }
        void Load()
        {
            dtgvFood.DataSource = FoodList;
            dtgvAccount.DataSource = AccountList;
            loaddata();
            LoadListFood();
            AddFoodBidings();
            LoadCategoryIntoComBoBox(cbCategory);
            //LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            AddAcountBinding();
            LoadAccount();
        }
        void AddAcountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("text", dtgvAccount.DataSource, "UserName"));
            txtDisplayName.DataBindings.Add(new Binding("text", dtgvAccount.DataSource, "UserName"));
            txtAccountType.DataBindings.Add(new Binding("text", dtgvAccount.DataSource, "Type"));
        }
        void LoadAccount()
        {
            AccountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void loaddata()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void LoadCategoryIntoComBoBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember= "Name";
        }
        void LoadListFood()
        {
            FoodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddFoodBidings()
        {
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        //void LoadDatePickerBill()
        //{
        //    DateTime today = DateTime.Now;
        //    dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
        //    dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        //}

        void LoadListBillByDate(DateTime CheckIn , DateTime CheckOut)
        {
           dtgvBill.DataSource = BillDAO.Instanre.GetBillListByDate(CheckIn, CheckOut);
        }
        #endregion
        #region Events
        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int idCategory = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryById(idCategory);

                    cbCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                        }
                        i++;
                    }
                    cbCategory.SelectedIndex = index;
                }
            }
            catch
            {

            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int category = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, category, price))
            {
                MessageBox.Show("Them Thanh Cong");
                LoadListFood();
                if ( insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("That bai Khi Them Mon");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int category = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = int.Parse(txtFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, category, price))
            {
                MessageBox.Show("Sua Thanh Cong");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("That bai Khi Sua Mon");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoa Thanh Cong");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("That bai Khi Xoa Mon");
            }
        }
        private void btnViewFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
           FoodList.DataSource = SearchFoodByName(txtSearchFoodName.Text);
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        #endregion

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmAdmin_Load_1(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.WindowState = FormWindowState.Maximized;
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}