using bank_mangement_system.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Repo
{
    class BankRepository
    {
        public void AddAccount(BankAccount account)
        {
            try
            { 
                DBconfig Db = new DBconfig();
                Db.Open_connection();
                

                string query = "INSERT INTO Accounts (custid,Accountnum, balance, Accounttype) VALUES (@Id, @Accountnum ,@balance, @Accounttype)";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                Customer cust = account.Owner;
                command.Parameters.AddWithValue("@Id", cust.Cust_id);
                command.Parameters.AddWithValue("@Accountnum", account.AccountNumber);
                command.Parameters.AddWithValue("@balance", account.Balance);
                command.Parameters.AddWithValue("@Accounttype", account.Type);
                command.ExecuteNonQuery();
                Debug.WriteLine(account.AccountNumber + ":- Added to the database.");

                Db.Close_connection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error adding account: " + ex.Message);
                throw; 
            }
        }

        public void DeleteAccount(int accnum)
        {
            try
            {
                DBconfig Db = new DBconfig();
                Db.Open_connection();

                string query = "Delete FROM Accounts WHERE Accountnum = @Accountnum";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());

                command.Parameters.AddWithValue("@Accountnum", accnum);

                command.ExecuteNonQuery();

                Db.Close_connection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error adding account: " + ex.Message);
                throw;
            }
        }
        public BankAccount SearchAccount(int Accountnumber)
        {
            try
            {
                BankAccount account = new BankAccount();
                DBconfig Db = new DBconfig();
                Db.Open_connection();

                string query = "SELECT Accountnum , balance FROM Accounts WHERE Accountnum = @account";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                command.Parameters.AddWithValue("@account", Accountnumber);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account.AccountNumber = reader.GetInt32(reader.GetOrdinal("Accountnum"));
                        account.Balance = reader.GetDecimal(reader.GetOrdinal("balance"));
                        Debug.WriteLine(account.AccountNumber + ":- found in the database.");
                    }
                }

                Db.Close_connection();
                return account;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error getting account: " + ex.Message);
                throw; 
            }
          
        }



        public String Transfer(int From , int To , decimal amount)
        {
            try
            {

                BankAccount account_from = SearchAccount(From);
                BankAccount account_to = SearchAccount(To);

                if (account_to.AccountNumber != 0 && account_from.AccountNumber != 0)
                {

                    DBconfig Db = new DBconfig();
                    Db.Open_connection();

                    if (account_from.Withdraw(amount))
                    {

                        account_to.Deposit(amount);
                        string query = "INSERT INTO [History] ([From], [To], [Amount]) VALUES (@Fromdata, @Todata, @Amountdata)";
                        SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                        command.Parameters.AddWithValue("@Fromdata", From);
                        command.Parameters.AddWithValue("@Todata", To);
                        command.Parameters.AddWithValue("@Amountdata", amount);
                        command.ExecuteNonQuery();

                        Debug.WriteLine(From + " send to " + To + " :- " + amount);

                        Db.Close_connection();

                        UpdateAccountBalance(account_from);
                        UpdateAccountBalance(account_to);

                        return "money sent successfully";
                    }
                    else
                    {
                        return "Insufficient funds.";
                    }
                }

                return "Account is not found.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in tranfer: " + ex.Message);
                throw;
            }
        }


        public void UpdateAccountBalance(BankAccount account)
        {
            try
            {
                DBconfig Db = new DBconfig();
                Db.Open_connection();

                string query = "UPDATE Accounts SET balance = @newBalance WHERE Accountnum = @accountNumber";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                command.Parameters.AddWithValue("@newBalance", account.Balance);
                command.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
                command.ExecuteNonQuery();
                Debug.WriteLine("Account " + account.AccountNumber + " updated with new balance: " + account.Balance);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error updating account balance: " + ex.Message);
                throw; // rethrow the exception to be handled further up the call stack
            }
        }



        public List<BankAccount> GetAllAccounts()
        {
            DBconfig Db = new DBconfig();
            Db.Open_connection();

            List<BankAccount> Account = new List<BankAccount>();
            string query = "SELECT custid,Accountnum, balance FROM  Accounts";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    BankAccount Accountdb = new BankAccount();

                    Accountdb.Customerid = reader.GetInt32(reader.GetOrdinal("custid"));
                    Accountdb.AccountNumber = reader.GetInt32(reader.GetOrdinal("Accountnum"));
                    Accountdb.Balance = reader.GetDecimal(reader.GetOrdinal("balance"));
                    Account.Add(Accountdb);

                    Debug.WriteLine("Customer found in the database.");
                }
            }

            Debug.WriteLine(Account);
            return Account;
        }

        public List<int> getAllaccountnumbers()
        {
            DBconfig Db = new DBconfig();
            Db.Open_connection();

            List<int> accountnumbers = new List<int>();
            string query = "SELECT Accountnum FROM  Accounts";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    accountnumbers.Add(reader.GetInt32(reader.GetOrdinal("Accountnum")));
                }
            }

            return accountnumbers;

        }

    }
}
