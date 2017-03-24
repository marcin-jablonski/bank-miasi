using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankTest
{
    public class DepositTests
    {
        private Bank.Bank _bank;
        private Bank.Products.BankAccount _account;
        private Bank.Products.Deposit _deposit;
        private int _ownerId;
        private double _depositAmount;
        private DateTime _depositEndDate;

        public DepositTests()
        {
            _ownerId = 1;
            _depositAmount = 2000;
            _depositEndDate = DateTime.Today + TimeSpan.FromDays(1);
            _bank = new Bank.Bank();
            _account = new Bank.Products.BankAccount(_bank, _ownerId);
            _bank.CreateBankProduct(_account);
            _deposit = new Bank.Products.Deposit(_bank, _account, _depositEndDate, _depositAmount);
            _bank.CreateBankProduct(_deposit);
        }

        [Fact]
        public void ShouldGetMoneyWhenCancelled()
        {
            _deposit.CancelDeposit();

            Assert.Equal(_depositAmount, _account.GetAccountState());
        }

        //add interests systems to check if works
    }
}
