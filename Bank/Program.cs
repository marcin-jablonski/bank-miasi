using System;
using Bank.Mechanisms.Interests;
using Bank.Mechanisms.Kir;
using Bank.Products;

namespace Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BankAccount _bankAccount;
            BankAccount _bankAccount2;
            var bank = new Bank();
            _bankAccount = new BankAccount(bank, 0, new NoInterest());
            bank.GetProducts().Add(_bankAccount);
            _bankAccount2 = new BankAccount(bank, 1, new NoInterest());
            bank.GetProducts().Add(_bankAccount2);
            Kir.AddBank(bank);
            _bankAccount.Deposit(200);
            _bankAccount.Transfer(10, _bankAccount2);
            Console.WriteLine(_bankAccount.Amount);
        }
    }
}
