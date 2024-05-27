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
    public partial class history : Form
    {
        public history()
        {
            InitializeComponent();


        }


        private void BindGrid()
        {

            try
            {

                BankRepository accountsrepo = new BankRepository();
                List<History> lists = accountsrepo.gethistory();

                if (lists != null)
                {

                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("FromColumn", "From_account_number");
                    dataGridView1.Columns.Add("ToColumn", "To_account_number");
                    dataGridView1.Columns.Add("AmountColumn", "Amount");


                    dataGridView1.Rows.Clear();
                    foreach (var list in lists)
                    {
                         dataGridView1.Rows.Add
                         (
                                list.from,
                                list.to,
                                list.Amount
                                       
                          );
                    }
                }
                else
                {
                    MessageBox.Show("No users found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mainmenu obj = new Mainmenu();
            this.Close();
            obj.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void history_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
