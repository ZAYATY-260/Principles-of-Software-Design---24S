using bank_mangement_system.Repo;
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

namespace bank_mangement_system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
     

        private void PictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_button_Click_1(object sender, EventArgs e)
        {
            Validation val = new Validation();
            
            if (val.Login_validation(userINP.Text, passINP.Text))
            {
                User user = new User();

                user.SetUsername(userINP.Text);
                user.SetPassword(passINP.Text);

                UserRepository userrepo = new UserRepository();
                bool Found = userrepo.SearchPersons(user);
                if(Found)
                {
                    MessageBox.Show("Login succesfully.");
                    Mainmenu Obj = new Mainmenu();
                    Obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Access denied!.");
                }
                
            }
            else
            {
                MessageBox.Show("Fill all the form!.");
            }
            
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
