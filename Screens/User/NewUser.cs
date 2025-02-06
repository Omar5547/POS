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

namespace POSDemo.Screens.Users
{
    public partial class NewUser : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath ;
        public NewUser()
        {
            InitializeComponent();
           
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            User u=new User
            {
                UserName = txtUser.Text,
                Password = txtPassword.Text,
                Image = imagePath

            };


            db.Users.Add(u);
            db.SaveChanges();
            if (imagePath != "")
            {
                string newpath = Environment.CurrentDirectory + "\\images\\Users\\" + u.id + ".jpg";

                File.Copy(imagePath, newpath);
                u.Image = newpath;
                db.SaveChanges();
            }
           
            MessageBox.Show("تم الحفظ");
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();  
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                imagePath = dialog.FileName;
               pictureBox1.ImageLocation=dialog.FileName;
                

            }
        }
    }
}
