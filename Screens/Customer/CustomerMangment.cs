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


namespace POSDemo.Screens.Customer
{
    public partial class CustomerMangment : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";
        int id;
        POSDemo.DB.Customer result;
        public CustomerMangment()
        {
            InitializeComponent();
            checkBox1.Checked = false;
            dataGridView1.DataSource = db.Customers.ToList();
        }

        private void label9_Click(object sender, EventArgs e)
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

        private void CustomerMangment_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")

                dataGridView1.DataSource = db.Customers.Where(x => x.Phone.Contains(txtPhone.Text)).ToList();
            else
                dataGridView1.DataSource = db.Customers.Where(x => x.Phone.Contains(txtPhone.Text) || x.Name.Contains(txtName.Text)).ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("هل أنت متأكد من اضافة هذا العميل كعميل جديد ", "اضافة عميل جديد", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                if (txtFormName.Text == "" || txtFormPhone.Text == "")
                {
                    MessageBox.Show("  برجاء اكمال البيانات المطلوبة اولا");
                }
                else
                {
                    POSDemo.DB.Customer c = new POSDemo.DB.Customer();
                    c.Name = txtFormName.Text;
                    c.Phone = txtFormPhone.Text;
                    c.Notes = txtNotes.Text;
                    c.email = txtMail.Text;
                    c.address = txtAddress.Text;
                    c.Company = txtCompany.Text;
                    c.isActive = checkBox1.Checked;


                    db.Customers.Add(c);
                    db.SaveChanges();
                    MessageBox.Show("تم حفظ العميل");

                    if (imagePath != "")
                    {



                        string newpath = Environment.CurrentDirectory + "\\images\\Customer\\" + c.id + ".jpg";

                        File.Copy(imagePath, newpath);
                        c.Image = newpath;
                        db.SaveChanges();

                    }
                    txtFormName.Text = "";
                    txtFormPhone.Text = "";
                    txtNotes.Text = "";
                    txtMail.Text = "";
                    txtAddress.Text =
                    txtCompany.Text = "";
                    pictureBox1.ImageLocation = "";
                    checkBox1.Checked = false;

                    dataGridView1.DataSource = db.Customers.ToList();

                }


            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                result = db.Customers.SingleOrDefault(x => x.id == id);
                txtFormName.Text = result.Name;
                txtFormPhone.Text = result.Phone;
                txtMail.Text = result.email;
                txtAddress.Text = result.address;
                txtNotes.Text = result.Notes;
                txtCompany.Text = result.Company;
                if (result.isActive == null)
                {
                    result.isActive = false;
                }
                checkBox1.Checked = (bool)result.isActive;
                pictureBox1.ImageLocation = result.Image;
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var r = MessageBox.Show("هل تريد الحذف؟", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Customers.Find(id);

                db.Customers.Remove(result);
                db.SaveChanges();
                dataGridView1.DataSource = db.Customers.ToList();


                MessageBox.Show("تم الحذف");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("هل تريد حفظ التعديل؟", "تأكيد التعديل", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                result.Name = txtFormName.Text;
                result.Phone = txtFormPhone.Text;
                result.email = txtMail.Text;
                result.address = txtAddress.Text;
                result.Notes = txtNotes.Text;
                result.Company = txtCompany.Text;
                result.isActive = checkBox1.Checked;



                if (imagePath != "")
                {
                    string newpath = Environment.CurrentDirectory + "\\images\\Customer\\" + result.id + ".jpg";

                    File.Copy(imagePath, newpath, true);
                    result.Image = newpath;

                }


                db.SaveChanges();
                MessageBox.Show("تم التعديل بنجاح");
                dataGridView1.DataSource = db.Customers .ToList();

            }
        }
    }
}
