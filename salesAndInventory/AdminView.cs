using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace skelot
{
    public partial class AdminView : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;
        //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Data.accdb";

        ListViewItem lst;
        frmLogin login = new frmLogin();

        public AdminView()
        {
            InitializeComponent();
        }

        private void AdminSearch_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(login.connection);
            cn.Open();
            this.getData();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.Hide();
          FrmAdminMenu AdminMenu = new FrmAdminMenu();
            AdminMenu.Show();
        }
        public void getData()
        {
            //code for Displaying data from Database to lstView.

            try
            {
                listView2.Items.Clear();
                listView2.Columns.Clear();
                listView2.Columns.Add("Product ID", 0);
                listView2.Columns.Add("Product Name", 190);
                listView2.Columns.Add("Price", 90);
                listView2.Columns.Add("Type", 100);
                listView2.Columns.Add("Size", 80);
                listView2.Columns.Add("Brand", 100);
                listView2.Columns.Add("Stock", 80);
                listView2.Columns.Add("Offered Price", 100);
                listView2.Columns.Add("Manufacturer", 190);

                string sql = @"Select * from tblProduct where Descrip like '" + txtSearch.Text + "%'";
                cm = new SqlCommand(sql, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView2.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
                    lst.SubItems.Add(dr[8].ToString());

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string getPriceDatabase(string code)
        {
            string returnstring="";
            string sql = @"Select * from tblProduct where ID like '" + txtBarcode.Text + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }

        public string getPriceDatabase2(string code,string barcode)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where ID like '" + barcode + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }
        public string getPriceDatabaseName(string code)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where Descrip like '" + txtSearch.Text + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }
        public void getDataBarcode(string barcode)
        {
            //code for Displaying data from Database to lstView.

            try
            {
                listView2.Items.Clear();
                listView2.Columns.Clear();
                listView2.Columns.Add("Product ID", 0);
                listView2.Columns.Add("Product Name", 190);
                listView2.Columns.Add("Price", 90);
                listView2.Columns.Add("Type", 100);
                listView2.Columns.Add("Size", 80);
                listView2.Columns.Add("Brand", 100);
                listView2.Columns.Add("Stock", 80);
                listView2.Columns.Add("Offered Price", 100);
                listView2.Columns.Add("Manufacturer", 190);

                string sql = @"Select * from tblProduct where ID like '" + barcode + "%'";
                cm = new SqlCommand(sql, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView2.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
                    lst.SubItems.Add(dr[8].ToString());

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void setPriceText(string code)
        {

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txttimkiem.Text = "1";
            this.getData();
          lbGia.Text = getPriceDatabaseName(txtloai.Text);
        }

        private void AdminView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void AdminView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                lbloaigia.Text = "Giá Đại Lý";
                txtloai.Text = "1";
            }
            if (e.KeyCode == Keys.F2)
            {
                lbloaigia.Text = "Giá Cửa Hàng";
                txtloai.Text = "2";
            }
            if (e.KeyCode == Keys.F3)
            {
                lbloaigia.Text = "Giá Bán Lẽ";
                txtloai.Text = "3";
            }
            if (txttimkiem.Text == "2")
            {
                this.getDataBarcode(txtBarcode.Text);
                lbGia.Text = getPriceDatabase(txtloai.Text);
            }
            else
            {
                lbGia.Text = getPriceDatabaseName(txtloai.Text);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            txttimkiem.Text = "2";
            this.getDataBarcode(txtBarcode.Text);
            lbGia.Text = getPriceDatabase(txtloai.Text);
            
        }

        private void barcode_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBarcode_ModifiedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
