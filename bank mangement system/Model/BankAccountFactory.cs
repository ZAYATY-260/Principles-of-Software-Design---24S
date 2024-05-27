using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    class BankAccountFactory
    {
        private static List<int> acountnumbers ;
        private static Random random = new Random();


        // Method to create a new bank account
        public static BankAccount CreateAccount(Customer owner, AccountType type, decimal initialBalance , List<int> accountNumbers)
        {
            acountnumbers = accountNumbers;
            int accountNumber = GenerateAccountNumber();
            return new BankAccount(owner, accountNumber, type, initialBalance);
        }


        private static int GenerateAccountNumber()
        {
             int randomnumber = random.Next(1, 10000);

                foreach (var account in acountnumbers)
                {
                    if(account == randomnumber)
                    {
                        GenerateAccountNumber();
                    }
                }

            return randomnumber;
        }
    }
}
