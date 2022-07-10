using ManagerCoffee.DAO;
using ManagerCoffee.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = ManagerCoffee.DTO.Menu;

namespace ManagerCoffee
{
    public partial class FrmTableManager : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public FrmTableManager(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;
            LoadTable();
            LoadCategory();
        }
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 0;
            //thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.UserName + ")";
        }
        #region method
        bool status = false;
        int type = 0;

        void LoadCategory()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> foods = FoodDAO.Instance.GetListFoodByCategoryID(id);
            cbFood.DataSource = foods;
            cbFood.DisplayMember = "Name";
        }
        
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tables = TableDAO.Instance.LoadTableList();
            foreach (Table table in tables)
            {
                Button button = new Button() { Width = (int)TableDAO.TableWidth, Height = (int)TableDAO.TableHeight };
                button.Click += button_Click;
                button.Tag = table;
                if(table.Status == status.ToString())
                {
                    table.Status = "Trống";
                    button.BackColor = Color.DarkGray;
                    button.Text = table.Name + Environment.NewLine + table.Status;
                }
                else
                {
                    table.Status = "Có Người";
                    button.BackColor = Color.LightBlue;
                    button.Text = table.Name + Environment.NewLine + table.Status;
                }
                flpTable.Controls.Add(button);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<ManagerCoffee.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (ManagerCoffee.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            //Thread.CurrentThread.CurrentCulture = culture;

            txtTotalPrice.Text = totalPrice.ToString("c", culture);

        }

        #endregion

        #region Events
        private void button_Click(object sender, EventArgs e)
        {
            int tableId =((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableId);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAccountProfile f = new FrmAccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAdmin f = new FrmAdmin();
            f.InsertFood += F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.ShowDialog();
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            
            id = selected.ID;
            LoadFoodListByCategoryID(id);
        }
        private void btnDiscount_Click(object sender, EventArgs e)
        {

        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if(table == null)
            {
                MessageBox.Show("Ban Hay Chon Ban");
                return;
            }
            int idbill = BillDAO.Instanre.GetUncheckBillIdByTableId(table.ID);
            if (idbill == 0)
            {
                MessageBox.Show("Hay Chon Ban");
            }
            int idfood = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;
            if(idbill == -1)
            {
                BillDAO.Instanre.InsertBill(table.ID);
                BilllInfoDAO.Instance.InsertBillInfo(BillDAO.Instanre.GetMaxIdBill(), idfood, count);
            }
            else
            {
                BilllInfoDAO.Instance.InsertBillInfo(idbill, idfood, count);
            }
            ShowBill(table.ID);

            LoadTable();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idbill = BillDAO.Instanre.GetUncheckBillIdByTableId(table.ID);
            int discount = (int)nmDiscount.Value;
            double totalPrice = double.Parse(txtTotalPrice.Text, NumberStyles.Currency, new CultureInfo("vi-VN"));
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idbill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instanre.CkeckOut(idbill, discount,(float)finalTotalPrice);
                    ShowBill(table.ID);

                    LoadTable();
                }
                
            }
        }

        #endregion
    }
}
