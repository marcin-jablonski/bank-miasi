using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankTest
{
    public class BankAccountTest
    {
        private Bank.Bank _bank = null;
        private Bank.Products.BankAccount _bankAccount = null;

        public BankAccountTest()
        {
            _bank = new Bank.Bank();
        }

        [Fact]
        public void ShouldCreateBankAccount()
        {
            _bankAccount = new Bank.Products.BankAccount(_bank, 0, 0);
            Assert.NotNull(_bankAccount);
        }


        [Fact]
        public void ShouldDeposit()
        {
            double amount = 100.52;
            _bankAccount = new Bank.Products.BankAccount(_bank, 0, 0);
            _bankAccount.Deposit(amount);
            Assert.Equal(amount, _bankAccount.GetAccountState());
        }


        [Fact]
        public void ShouldWithdraw()
        {
            double amount = 100.0;
            _bankAccount = new Bank.Products.BankAccount(_bank, 0, 0);
            _bankAccount.Deposit(amount);
            _bankAccount.Withdraw(40.0);
            Assert.Equal(60.0, _bankAccount.GetAccountState());
        }



    }
}
