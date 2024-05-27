using bank_mangement_system.Repo;
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
    public partial class bill_payment : Form
    {
        public bill_payment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Accountfrom.Text) && !string.IsNullOrEmpty(Amountfield.Text) && !string.IsNullOrEmpty(Accountto.Text))
            {
                BankRepository bankrepo = new BankRepository();

                int From;
                bool AccountFromisParsed = int.TryParse(Accountfrom.Text, out From);

                decimal Amount;
                bool AmountisParsed = decimal.TryParse(Amountfield.Text, out Amount);

                int To;
                bool AccountToisParsed = int.TryParse(Accountto.Text, out To);

                if (AccountFromisParsed && AmountisParsed && AccountToisParsed)
                {
                    MessageBox.Show(bankrepo.paybills(From, To, Amount));
                    reset();
                }
                else
                {
                    MessageBox.Show("Error: Please check that the data entered is a number.");
                }

            }
            else
            {
                MessageBox.Show("Error: Fill all the fields.");
            }

        }
        public void reset()
        {
            Accountto.Text = "";
            Amountfield.Text = "";
            Accountfrom.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Mainmenu obj = new Mainmenu();
            this.Close();
            obj.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Accounttype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}
