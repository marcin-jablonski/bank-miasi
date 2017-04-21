using System;
using Bank.Interfaces;

namespace Bank.Products
{
    public class DebitAccount
    {
        private BankProduct _account;

        public DebitAccount(BankProduct account)
        {
            _account = account;
        }

        public void Withdraw(double amount)
        {
            throw new NotImplementedException();
        }

        public void Deposit(double amount)
        {
            throw new NotImplementedException();
        }

        public double GetAccountState()
        {
            throw new NotImplementedException();
        }
    }
}