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

        //public bool SearchCustomer(Customer customer)
        //{
        //    bool customerFound = false;
        //    DBconfig Db = new DBconfig();
        //    Db.Open_connection();
        //    AESEncryption aesEncryption = new AESEncryption();

        //    string query = "SELECT CustId, CustName, Address, MobilePhone FROM CustomerTbl WHERE CustName LIKE @SearchTerm";
        //    SqlCommand command = new SqlCommand(query, Db.Get_Conn());
        //    command.Parameters.AddWithValue("@SearchTerm", "%" + aesEncryption.Encrypt(customer.GetUsername()) + "%");

        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            customerFound = true;
        //            customer.SetId(reader.GetInt32(reader.GetOrdinal("CustId")));
        //            customer.SetUsername(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("CustName"))));
        //            customer.Address = aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("Address")));
        //            customer.MobilePhone = aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("MobilePhone")));

        //            Debug.WriteLine("Customer found in the database.");
        //        }
        //    }

        //    if (!customerFound)
        //    {
        //        Debug.WriteLine("Customer not found in the database.");
        //    }
        //    Db.Close_connection();

        //    return customerFound;
        //}

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
