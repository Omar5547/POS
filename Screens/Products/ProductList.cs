using POSDemo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo.Screens.Products
{
    public partial class ProductList : Form
    {
        POSTutEntities db = new POSTutEntities();
        int id;
        POSDemo.DB.Product result;
        string imagePath = "";
        public ProductList()
        {
            InitializeComponent();
            // dataGridView1.DataSource = db.Products.ToList();
           
            
            comboBox1.DataSource = db.Categories.ToList();
            comboBox2.DataSource = db.Categories.ToList();
            dataGridView1.DataSource = db.Products.Skip(1).Take(3).ToList();  
            
            //MessageBox.Show(db.Products.Max(x => x.Price).ToString());
            //var xx =db.Products.Sum(x=>x.price);
            //MessageBox.Show(xx.ToString()); 

        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet3.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet3.Category);
            // TODO: This line of code loads data into the 'pOSTutDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.pOSTutDataSet.Product);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "") 
            
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtBarcode.Text).ToList();
            else
            dataGridView1.DataSource = db.Products.Where(x => x.Code == txtBarcode.Text||x.Name.Contains(txtName.Text)).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             

            result.Name = txtFormName.Text;
            result.Code = txtFormBarcode.Text;
            result.Price = decimal.Parse(txtPrice.Text);
            result.Quantity = int.Parse(txtQty.Text);
            result.Notes = txtNotes.Text;
            result.CategoryId = int.Parse(comboBox1.SelectedValue.ToString());
            if (imagePath != "")
            {
                string newpath = Environment.CurrentDirectory + "\\images\\Products\\" + result.id + ".jpg";

                File.Copy(imagePath, newpath,true);
                result.Image = newpath;
               
            }


            db.SaveChanges();
            MessageBox.Show("تم التعديل بنجاح");
            dataGridView1.DataSource = db.Products.ToList();
           



        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                result = db.Products.SingleOrDefault(x => x.id == id);
                txtFormBarcode.Text = result.Code;
                txtFormName.Text = result.Name;
                txtPrice.Text = result.Price.ToString();
                txtQty.Text = result.Quantity.ToString();
                txtNotes.Text = result.Notes;
                pictureBox1.ImageLocation = result.Image;
                comboBox1.SelectedValue = result.CategoryId; 
            }
            catch {  }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = dialog.FileName;


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var r = MessageBox.Show("هل تريد الحذف؟","تأكيد الحذف",MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Products.Find(id);

                db.Products.Remove(result);
                db.SaveChanges();
                dataGridView1.DataSource = db.Products.ToList();


                MessageBox.Show("تم الحذف");

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catid = int.Parse(comboBox2.SelectedValue.ToString());    
          dataGridView1.DataSource=  db.Products.Where(x => x.CategoryId ==catid).ToList();
        }
    }
}
