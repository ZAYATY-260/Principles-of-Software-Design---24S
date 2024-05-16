using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    class Customer:User
    {
        public int Cust_id;
        public string Address;
        public string MobilePhone;
        public string Position;

        public List<BankAccount> Accounts { get; } = new List<BankAccount>();

        public Customer(int id, string username, string password, string type, string address, string mobilePhone, string position)
            : base(id, username, password, type)
        {
            Cust_id = id;
            Position = position;
            Address = address;
            MobilePhone = mobilePhone;
        }

        public Customer()
        {

        }

        public void AddAccount(BankAccount account)
        {
            Accounts.Add(account);
        }

        // Getters and setters
        public int GetCust_id()
        {
            return Cust_id;
        }

        public void SetCust_id(int cust_id)
        {
            Cust_id = cust_id;
        }

        public string GetAddress()
        {
            return Address;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public string GetMobilePhone()
        {
            return MobilePhone;
        }

        public void SetMobilePhone(string mobilePhone)
        {
            MobilePhone = mobilePhone;
        }

        public string GetPosition()
        {
            return Position;
        }

        public void SetPosition(string position)
        {
            Position = position;
        }

    }
}
