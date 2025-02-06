using POSDemo.DB;
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
    public partial class SalesBillListData : Form
    {
        POSTutEntities db = new POSTutEntities();
        public SalesBillListData()
        {
            InitializeComponent();
            dataGridView2.DataSource = db.SalesBills.Select(x => new { x.id, x.Total, x.User.UserName, x.Customer.Name, x.Date }).ToList();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var bill = db.SalesBills.FirstOrDefault(x=>x.id==id);

            txtNumber.Text = bill.id.ToString();
            txtNotes.Text = bill.Notes;
            lblTotalAfterDiscount.Text = bill.TotalAfterDiscount.ToString();
            lblTotal.Text = bill.Total.ToString();
            dateTimePicker1.Value= (DateTime)bill.Date;
            txtDiscount.Text = bill.discount.ToString();
            comboBox1.SelectedValue = bill.Customer.id;
            dataGridView1.Rows.Clear();
            foreach ( var item in bill.SalesBillDetails) 
            {
                dataGridView1.Rows.Add(item.ProductId,item.Product.Name ,item.Price, item.quantity, item.totalPrice, item.Product.Image==null? new Bitmap(20,20) : Image.FromFile(item.Product.Image));
            }



        }

        private void SalesBillListData_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet8.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter4.Fill(this.pOSTutDataSet8.Customer);

        }
    }
}
