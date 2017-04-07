using Bank.Mechanisms.Interests;
using System;
using Xunit;

namespace BankTest
{
    public class DepositTests
    {
        private readonly Bank.Bank _bank;
        private readonly Bank.Products.BankAccount _account;

        public DepositTests()
        {
            const int ownerId = 1;
            _bank = new Bank.Bank();
            _account = new Bank.Products.BankAccount(_bank, ownerId, new NoInterest());
            _bank.CreateBankProduct(_account);
            
        }

        [Fact]
        public void ShouldGetMoneyWhenCancelled()
        {
            var depositAmount = 2000;
            var depositEndDate = DateTime.Today + TimeSpan.FromDays(1);
            var deposit = new Bank.Products.Deposit(_bank, _account, new NoInterest(), depositEndDate, depositAmount);
            _bank.CreateBankProduct(deposit);

            deposit.CancelDeposit();

            Assert.Equal(depositAmount, _account.GetAccountState());
        }

        [Fact]
        public void ShouldGetMoneyWithoutInterests()
        {
            const int depositAmount = 2000;
            var depositEndDate = DateTime.Today + TimeSpan.FromDays(1);
            var deposit = new Bank.Products.Deposit(_bank, _account, new NoInterest(), depositEndDate, depositAmount);
            _bank.CreateBankProduct(deposit);

            deposit.ChangeInterestSystem(new _5PercentInterest());
            deposit.CancelDeposit();

            Assert.Equal(depositAmount, _account.GetAccountState());
        }

        [Fact]
        public void ShouldGetMoneyWithInterests()
        {
            const int depositAmount = 2000;
            var depositEndDate = DateTime.Today - TimeSpan.FromDays(1);
            var deposit = new Bank.Products.Deposit(_bank, _account, new NoInterest(), depositEndDate, depositAmount);
            _bank.CreateBankProduct(deposit);

            deposit.ChangeInterestSystem(new _5PercentInterest());
            deposit.CancelDeposit();
            
            Assert.Equal(depositAmount*1.05, _account.GetAccountState());
        }
    }
}
