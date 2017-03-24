using System;
using System.Collections.Generic;
using System.Text;
using Bank.Exceptions;
using Xunit;

namespace BankTest
{
    public class BankAccountTest
    {
        private Bank.Bank _bank = null;
        private Bank.Bank _bank2 = null;
        private Bank.Products.BankAccount _bankAccount = null;
        private Bank.Products.BankAccount _bankAccount2 = null;
        private const double ToWithdraw = 40.31;
        private const double DepositAmount = 100.52;
        private const double AmountToTransfer = 23.11;

        public BankAccountTest()
        {
            _bank = new Bank.Bank();
            _bank2 = new Bank.Bank();
            _bankAccount = new Bank.Products.BankAccount(_bank, 0);
            _bankAccount2 = new Bank.Products.BankAccount(_bank, 1);
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
        public void ShouldNotDeposit()
        {
            Assert.Throws(typeof(IllegalOperationException), () => _bankAccount.Deposit(-DepositAmount));
        }


        [Fact]
        public void ShouldWithdraw()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.Withdraw(ToWithdraw);
            Assert.Equal(DepositAmount - ToWithdraw, _bankAccount.GetAccountState());
        }

        [Fact]
        public void ShouldNotWithdraw()
        {
            _bankAccount.Deposit(DepositAmount);
            Assert.Throws(typeof(IllegalOperationException), () => _bankAccount.Withdraw(-ToWithdraw));
        }

        [Fact]
        public void ShouldNotWithdraw2()
        {
            Assert.Throws(typeof(NotEnoughFundsException), () => _bankAccount.Withdraw(ToWithdraw));
        }

        [Fact]
        public void ShouldTransfer()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.Transfer(AmountToTransfer, _bankAccount2);

            Assert.Equal(DepositAmount - AmountToTransfer, _bankAccount.GetAccountState());
            Assert.Equal(AmountToTransfer, _bankAccount2.GetAccountState());
        }

        [Fact]
        public void ChargeInterest()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.ChargeInterest();
            Assert.Equal(DepositAmount, _bankAccount.GetAccountState());
        }




    }
}
