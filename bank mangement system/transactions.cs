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
            BankRepository bankrepo = new BankRepository();

            int From;
            bool AccountFromisParsed = int.TryParse(AccountFrom.Text, out From);

            decimal Amount;
            bool AmountisParsed = decimal.TryParse(Amountfield.Text, out Amount);

            int To;
            bool AccountToisParsed = int.TryParse(AccountTo.Text, out To);

            if(bankrepo.Transfer(From, To, Amount))
            {
                MessageBox.Show("Money Sent");
            }
            else
            {
                MessageBox.Show("Error ");
            }

            

        }


    }
}
