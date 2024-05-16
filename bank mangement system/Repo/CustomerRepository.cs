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
    class CustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();

            string query = "INSERT INTO CustTbl (Id,Position, Address, MobileNum) VALUES (@AdId, @Position ,@Address, @MobilePhone)";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@AdId", customer.Cust_id);
            command.Parameters.AddWithValue("@Position", aesEncryption.Encrypt(customer.Position));
            command.Parameters.AddWithValue("@Address", aesEncryption.Encrypt(customer.Address));
            command.Parameters.AddWithValue("@MobilePhone", aesEncryption.Encrypt(customer.MobilePhone));
            command.ExecuteNonQuery();

            Db.Close_connection();
        }

        public bool SearchCustomer(Customer customer)
        {
            bool customerFound = false;
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();

            string query = "SELECT CustId, CustName, Address, MobilePhone FROM CustomerTbl WHERE CustName LIKE @SearchTerm";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());
            command.Parameters.AddWithValue("@SearchTerm", "%" + aesEncryption.Encrypt(customer.GetUsername()) + "%");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customerFound = true;
                    customer.SetId(reader.GetInt32(reader.GetOrdinal("CustId")));
                    customer.SetUsername(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("CustName"))));
                    customer.Address = aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("Address")));
                    customer.MobilePhone = aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("MobilePhone")));

                    Debug.WriteLine("Customer found in the database.");
                }
            }

            if (!customerFound)
            {
                Debug.WriteLine("Customer not found in the database.");
            }
            Db.Close_connection();

            return customerFound;
        }

        public List<Customer> GetAllCustomers()
        {
            DBconfig Db = new DBconfig();
            Db.Open_connection();
            AESEncryption aesEncryption = new AESEncryption();
            List<Customer> customers = new List<Customer>();
            string query = "SELECT ID, Position,  MobileNum , Address FROM CustTbl";
            SqlCommand command = new SqlCommand(query, Db.Get_Conn());

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.SetCust_id(reader.GetInt32(reader.GetOrdinal("Id")));
                    customer.SetAddress(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("Address"))));
                    customer.SetMobilePhone(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("MobileNum"))));
                    customer.SetPosition(aesEncryption.Decrypt(reader.GetString(reader.GetOrdinal("Position"))));
                    customers.Add(customer);

                    Debug.WriteLine("Customer found in the database.");
                }
            }

            Debug.WriteLine(customers);
            return customers;
        }
    
}
}
