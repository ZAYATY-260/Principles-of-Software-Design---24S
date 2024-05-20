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

            Db.Close_connection();
        }

        public BankAccount SearchAccount(int Accountnumber)
        {

            BankAccount account = new BankAccount();
            DBconfig Db = new DBconfig();
            Db.Open_connection();

            string query = "SELECT Accountnum , balance FROM Accounts WHERE Accountnum LIKE @account";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@account", "%" + Accountnumber + "%");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                { 
                    account.AccountNumber = reader.GetInt32(reader.GetOrdinal("Accountnum"));
                    account.Balance = reader.GetDecimal(reader.GetOrdinal("balance"));
                    Debug.WriteLine("Customer found in the database.");
                }
            }

            Db.Close_connection();

            return account;
        }



        public bool Transfer(int From , int To , decimal amount)
        {
            if (SearchAccount(From) != null || SearchAccount(To) != null)
            {
                DBconfig Db = new DBconfig();
                Db.Open_connection();

                BankAccount account_from = SearchAccount(From);
                BankAccount account_to = SearchAccount(To);

                account_from.Withdraw(amount);
                account_to.Deposit(amount);


                string query = "INSERT INTO [History] ([From], [To], [Amount]) VALUES (@Fromdata, @Todata, @Amountdata)";
                SqlCommand command = new SqlCommand(query, Db.Get_Conn());
                command.Parameters.AddWithValue("@Fromdata", From);
                command.Parameters.AddWithValue("@Todata", To);
                command.Parameters.AddWithValue("@Amountdata", amount);
                command.ExecuteNonQuery();


                Db.Close_connection();
            }
            else
            {
                return false;
            }

            return true;
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

    }
}
