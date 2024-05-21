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
    public partial class transactions : Form
    {
        public transactions()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainmenu obj = new Mainmenu();
            obj.Show();
        }

        private void Transfer_Click(object sender, EventArgs e)
        {


            if(!string.IsNullOrEmpty(AccountFrom.Text) && !string.IsNullOrEmpty(Amountfield.Text) && !string.IsNullOrEmpty(AccountTo.Text))
            {
                BankRepository bankrepo = new BankRepository();

                int From;
                bool AccountFromisParsed = int.TryParse(AccountFrom.Text, out From);

                decimal Amount;
                bool AmountisParsed = decimal.TryParse(Amountfield.Text, out Amount);

                int To;
                bool AccountToisParsed = int.TryParse(AccountTo.Text, out To);

                if(AccountFromisParsed && AmountisParsed && AccountToisParsed)
                {
                       MessageBox.Show(bankrepo.Transfer(From, To, Amount));
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


    }
}
