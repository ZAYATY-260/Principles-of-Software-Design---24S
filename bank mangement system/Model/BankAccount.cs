using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    public enum AccountType
    {
        Checking,
        Savings
    }
    class BankAccount
    {
            public int AccountNumber { get; }
            public AccountType Type { get; }
            public decimal Balance { get; private set; }
            public Customer Owner { get; }

            public BankAccount(Customer owner, int accountNumber, AccountType type, decimal initialBalance)
            {
                AccountNumber = accountNumber;
                Type = type;
                Balance = initialBalance;
                Owner = owner;
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
