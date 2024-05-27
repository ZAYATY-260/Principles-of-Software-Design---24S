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

        public void reset()
        {
            Username.Text = string.Empty;
            Password.Text = string.Empty;
            Mobilenumber.Text = string.Empty;
            Address.Text = string.Empty;
            Position.Text = string.Empty;
            Balance.Text = string.Empty;
        }

        private void Add_customer_Click(object sender, EventArgs e)
        {
            try
            {
                decimal parsedBalance;
                bool isParsed = decimal.TryParse(Balance.Text, out parsedBalance);
                if (Accounttype.SelectedItem != null && isParsed &&  !String.IsNullOrWhiteSpace(Password.Text)
                    && !String.IsNullOrWhiteSpace(Username.Text) &&  !String.IsNullOrWhiteSpace(Mobilenumber.Text)
                    && !String.IsNullOrWhiteSpace(Address.Text)  &&  !String.IsNullOrWhiteSpace(Position.Text))
                {
                    User user = new User();
                    user.SetUsername(Username.Text);
                    user.SetPassword(Password.Text);
                    user.Settype("CUST");

                    UserRepository userrepo = new UserRepository();
                    userrepo.AddPerson(user);
                    user = userrepo.GetPerson(user);

                    Customer customer = new Customer(user.GetId(), user.GetUsername(), user.GetPassword(), user.Gettype(), Mobilenumber.Text, Address.Text, Position.Text);
                    CustomerRepository custrepo = new CustomerRepository();
                    custrepo.AddCustomer(customer);

                
                    AccountType type = (AccountType)Accounttype.SelectedItem;


                    BankRepository bankRepository = new BankRepository();
                    BankAccountFactory b = new BankAccountFactory();
                    
                    BankAccount account = BankAccountFactory.CreateAccount(customer, type, parsedBalance , bankRepository.getAllaccountnumbers());

                    
                    bankRepository.AddAccount(account);

                    reset();

                  
                    MessageBox.Show($"Account created successfully!\nAccount Number:" + account.AccountNumber);

                }
                else
                {
                    MessageBox.Show($"Please make sure that all the fields are filled with data" );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            BindGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mainmenu obj = new Mainmenu();
            obj.Show();
            this.Close();
        }
    }
}
