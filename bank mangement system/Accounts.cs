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
    public partial class Accounts : Form
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                UserRepository repo = new UserRepository();
                List<User> users = repo.GetAllPersons();

                if (users != null && users.Count > 0)
                {
                    // Clear existing columns
                    dataGridView1.Columns.Clear();

                    // Add columns
                    dataGridView1.Columns.Add("IDColumn", "ID");
                    dataGridView1.Columns.Add("UsernameColumn", "Username");

                    // Bind data
                    dataGridView1.Rows.Clear(); // Clear existing rows
                    foreach (var user in users)
                    {
                        dataGridView1.Rows.Add(user.GetId(), user.GetUsername());
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

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
