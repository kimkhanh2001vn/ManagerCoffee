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
using ManagerCoffee.DAO;
using ManagerCoffee.DTO;

namespace ManagerCoffee
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            string username = txttaikhoan.Text;
            string password = txtmatkhau.Text;
            if (LoginAccount(username ,password))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(username);
                FrmTableManager f = new FrmTableManager(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai Tài Khoản Hoặc Mật Khẩu");
            }
            
        }

        bool LoginAccount(string username, string password)
        {
            return AccountDAO.Instance.Login(username ,password);
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát Chương Trình","Thông Báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void Qu_Click(object sender, EventArgs e)
        {
            SendMail f = new SendMail();
            f.ShowDialog();
        }
    }
}
