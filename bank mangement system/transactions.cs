using bank_mangement_system.Model;
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
                    rest();
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

        private void button3_Click(object sender, EventArgs e)
        {
            BankRepository bankrepo = new BankRepository();

            int From;
            bool AccountFromisParsed = int.TryParse(accountdeposit.Text, out From);

            decimal Amount;
            bool AmountisParsed = decimal.TryParse(amoutdeposit.Text, out Amount);


            MessageBox.Show(bankrepo.deposit(From , Amount));
            rest();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            BankRepository bankrepo = new BankRepository();

            int From;
            bool AccountFromisParsed = int.TryParse(accountwithdraw.Text, out From);

            decimal Amount;
            bool AmountisParsed = decimal.TryParse(amountwithdraw.Text, out Amount);


            MessageBox.Show(bankrepo.withdraw(From, Amount));
            rest();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BankRepository bankrepo = new BankRepository();
            int From;
            bool AccountFromisParsed = int.TryParse(checkbalance.Text, out From);
            
            BankAccount account = bankrepo.SearchAccount(From);

            balance.Text = account.Balance.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            balance.Text = "balance label";
            checkbalance.Text = "";
        }

        public void rest()
        {
            balance.Text = "balance label";
            checkbalance.Text = "";

            accountwithdraw.Text = "";
            amountwithdraw.Text = "";

            accountdeposit.Text = "";
            amoutdeposit.Text = "";

            AccountFrom.Text = "";
            Amountfield.Text = "";
            AccountTo.Text = "";
        }
    }
}
