using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    class BankAccount
    {
            public int Customerid   { get; set; }
            public int AccountNumber { get; set; }
            public AccountType Type { get; set; }
            public decimal Balance { get;  set; }
            public Customer Owner { get; set; }

            public BankAccount(Customer owner, int accountNumber, AccountType type, decimal initialBalance)
            {
                AccountNumber = accountNumber;
                Type = type;
                Balance = initialBalance;
                Owner = owner;
            }

        public BankAccount()
        {
        }

        public void Deposit(decimal amount)
            {
                Balance += amount;
                Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
            }

            public void Withdraw(decimal amount)
            {
                if (amount > Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    return;
                }

                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount:C}. New balance: {Balance:C}");
            }

    }
}
