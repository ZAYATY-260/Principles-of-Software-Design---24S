using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    class BankAccountFactory
    {
        private static int accountNumberSeed = 1000;

        // Method to create a new bank account
        public static BankAccount CreateAccount(Customer owner, AccountType type, decimal initialBalance)
        {
            int accountNumber = GenerateAccountNumber();
            return new BankAccount(owner, accountNumber, type, initialBalance);
        }

        // Method to generate a unique account number
        private static int GenerateAccountNumber()
        {
            return accountNumberSeed++;
        }
    }
}
