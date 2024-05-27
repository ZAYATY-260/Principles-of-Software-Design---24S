using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bank_mangement_system
{
    public partial class Mainmenu : Form
    {
        public Mainmenu()
        {
            InitializeComponent();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Accounts acc = new Accounts();
            acc.Show();
        }

    
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            transactions obj = new transactions();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            settimgs obj = new settimgs();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            bill_payment obj = new bill_payment();
            this.Close();
            obj.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Close();
            obj.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            history obj = new history();
            obj.Show();
            this.Close();
        }
    }
}
