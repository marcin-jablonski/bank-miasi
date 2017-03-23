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
        private const double ToWithdraw = 40.31;
        private const double DepositAmount = 100.52;

        public BankAccountTest()
        {
            _bank = new Bank.Bank();
            _bankAccount = new Bank.Products.BankAccount(_bank, 0, 0);
        }

        [Fact]
        public void ShouldCreateBankAccount()
        {
            Assert.NotNull(_bankAccount);
        }


        [Fact]
        public void ShouldDeposit()
        {
            _bankAccount.Deposit(DepositAmount);
            Assert.Equal(DepositAmount, _bankAccount.GetAccountState());
        }


        [Fact]
        public void ShouldWithdraw()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.Withdraw(ToWithdraw);
            Assert.Equal(DepositAmount - ToWithdraw, _bankAccount.GetAccountState());
        }



    }
}
