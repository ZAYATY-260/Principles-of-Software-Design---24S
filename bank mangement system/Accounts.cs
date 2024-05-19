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
    public partial class Accounts : Form
    {
        public Accounts()
        {
            InitializeComponent();
            Accounttype.DataSource = Enum.GetValues(typeof(AccountType));
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

                CustomerRepository custrepo = new CustomerRepository();
                List<Customer> customers = custrepo.GetAllCustomers();

                BankRepository accountsrepo = new BankRepository();
                List<BankAccount> accounts = accountsrepo.GetAllAccounts();

                if (users != null && users.Count > 0)
               {
                    
                    dataGridView1.Columns.Clear();

                    
                    dataGridView1.Columns.Add("IDColumn", "ID");
                    dataGridView1.Columns.Add("AccountNumberColumn", "AccountNumber");
                    dataGridView1.Columns.Add("UsernameColumn", "Username");
                    dataGridView1.Columns.Add("PositionColumn", "Type");
                    dataGridView1.Columns.Add("AddressColumn", "Address");
                   dataGridView1.Columns.Add("MobilePhoneColumn", "Mobile Phone");
                   dataGridView1.Columns.Add("PositionColumn", "Position");
                    dataGridView1.Columns.Add("BalanceColumn", "Balance");

                    dataGridView1.Rows.Clear();
                    foreach (var user in users)
                    {
                        foreach (var customer in customers)
                        {
                            foreach (var account in accounts)
                           {
                                if (user.GetId() == customer.GetCust_id() && customer.GetCust_id() == account.Customerid)
                                {
                                    dataGridView1.Rows.Add
                                    (
                                        user.GetId(),
                                        account.AccountNumber,
                                        user.GetUsername(),
                                        user.Gettype(),
                                        customer.GetAddress(),
                                        customer.GetMobilePhone(),
                                        customer.GetPosition(),
                                        account.Balance
                                    );
                             }
                            }
                        }

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

        private void Add_customer_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.SetUsername(Username.Text);
            user.SetPassword(Password.Text);
            user.Settype("CUST");

            UserRepository userrepo = new UserRepository();
            userrepo.AddPerson(user);
            user = userrepo.GetPerson(user);

            Customer customer = new Customer(user.GetId(),user.GetUsername() , user.GetPassword() ,user.Gettype() , Mobilenumber.Text , Address.Text , Position.Text);
            CustomerRepository custrepo = new CustomerRepository();
            custrepo.AddCustomer(customer);

            try
            {
                // Validate the selected item and balance input
                if (Accounttype.SelectedItem == null)
                {
                    MessageBox.Show("Please select an account type.");
                    return;
                }

                if (!decimal.TryParse(Balance.Text, out decimal initialBalance))
                {
                    MessageBox.Show("Please enter a valid balance amount.");
                    return;
                }

                // Convert selected item to AccountType enum
                AccountType type = (AccountType)Accounttype.SelectedItem;


                // Create the bank account using the factory
                BankAccount account = BankAccountFactory.CreateAccount(customer, type, initialBalance);

                // Save the account to the repository
                BankRepository bankRepository = new BankRepository();
                bankRepository.AddAccount(account);

                // Display success message
                MessageBox.Show($"Account created successfully!\nAccount Number:");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            BindGrid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
