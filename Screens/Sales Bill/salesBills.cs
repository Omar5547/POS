﻿using POSDemo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo.Screens.Sales_Bill
{
    public partial class salesBills : Form
    {
        POSTutEntities db = new POSTutEntities();
        List<POSDemo.DB.Product> products;
        public salesBills()
        {
            InitializeComponent();

            products = db.Products.ToList();
            imageList1.ImageSize=new Size(70,70);
            lblUser.Text = POSDemo.Users.Name;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

  
        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
          

        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void salesBills_Load(object sender, EventArgs e)
        {
            this.customerTableAdapter3.Fill(this.pOSTutDataSet7.Customer);

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Image != null)
                {
                    imageList1.Images.Add(Image.FromFile(products[i].Image));
                }
                else
                {
                    Bitmap btm = new Bitmap(70,70);
                    imageList1.Images.Add(btm);

                }
                ListViewItem item = new ListViewItem(); 
                item.Text = products[i].Name;
                item.ImageIndex = i;
                item.Tag = products[i]; 

                listView1.Items.Add(item);
            }

        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.customerTableAdapter3.FillBy(this.pOSTutDataSet7.Customer);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        void CalculateTotal()
        {
            try
            {
                decimal total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += decimal.Parse(dataGridView1.Rows[i].Cells["totalprice"].Value.ToString());
                }

                lblTotal.Text = total.ToString();
                decimal disc = decimal.Parse(txtDiscount.Text);
                lblTotalAfterDiscount.Text = (total - disc).ToString();
            }
            catch { }   
 
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
               var product = (POSDemo.DB.Product)listView1.SelectedItems[0].Tag;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == product.id.ToString())
                    {
                        dataGridView1.Rows[i].Cells["quantity"].Value =int.Parse (dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) + 1;
                        dataGridView1.Rows[i].Cells["totalprice"].Value= dataGridView1.Rows[i].Cells["quantity"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) *
                            decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString());
                        CalculateTotal();
                        return;
                    }
                }
                MessageBox.Show(product.Price.ToString());
                dataGridView1.Rows.Add(product.id, product.Name, product.Price, 1, product.Price * 1, product.Image == null ? (new Bitmap(40, 40)) : Image.FromFile(product.Image));
                CalculateTotal();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            POSDemo.DB.SalesBill result = new POSDemo.DB.SalesBill()
            {
                Date = (DateTime)dateTimePicker1.Value.Date,
                discount = decimal.Parse(txtDiscount.Text),
                Total = decimal.Parse(lblTotal.Text),
                TotalAfterDiscount = decimal.Parse(lblTotalAfterDiscount.Text),
                Notes = txtNotes.Text,
                CustomerId = int.Parse(comboBox1.SelectedValue.ToString()),
            //  UserId = POSDemo.Users.id


            };
            List <SalesBillDetail> list = new List<SalesBillDetail>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                list.Add(new SalesBillDetail()
                {

                    ProductId = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                    quantity = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()),
                    Price = decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString()),
                    totalPrice = decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString()) * int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()),

                });
            }

            result.SalesBillDetails = list;
            db.SalesBills.Add(result);
            db.SaveChanges();
            MessageBox.Show("تم الحفظ رقم الفاتورة =" + result.id);

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SalesBillListData bills = new SalesBillListData();  
            bills.Show();
        }
    }
}
