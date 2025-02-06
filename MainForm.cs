using POSDemo.DB;
using POSDemo.Screens.Customer;
using POSDemo.Screens.Products;
using POSDemo.Screens.Sales_Bill;
using POSDemo.Screens.Suppliers;
using POSDemo.Screens.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            lblUser.Text = Users.Name;

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Screens.Products.Product p = new Screens.Products.Product();
            p.Show();   
        }

        private void addNewUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUser frm = new NewUser();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            salesBills frm = new salesBills();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void listProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductList frm = new ProductList();    
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductList frm = new ProductList();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CustomerMangment c = new CustomerMangment();
            c.Show();
        }

        private void العملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerMangment c = new CustomerMangment();    
            c.Show();
        }

   
        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();  
            s.Show();
        }

        private void الموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            s.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
