using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankTest
{
    public class CreditTests
    {
        private Bank.Bank _bank;
        private Bank.Products.BankAccount _account;
        private Bank.Products.Credit _credit;
        private int _ownerId;

        public CreditTests()
        {
            _ownerId = 1;
            _bank = new Bank.Bank();
            _account = new Bank.Products.BankAccount(_bank, _ownerId);
            _bank.CreateBankProduct(_account);
            _credit = new Bank.Products.Credit(_bank, _account);
            _bank.CreateBankProduct(_credit);
        }
        
        [Fact]
        public void ShouldGetMoneyFromCreditAccount()
        {
            var creditAmount = 200;
            _credit.GetMoney(creditAmount);

            Assert.Equal(200, _credit.GetAccountState());
            Assert.Equal(200, _account.GetAccountState());
        }

        [Fact]
        public void ShouldPayInstallment()
        {
            var accountState = 2000;
            var creditAmount = 500;
            _account.Deposit(accountState);
            _credit.GetMoney(creditAmount);

            _credit.PayCreditInstallment();

            Assert.Equal(accountState, _account.GetAccountState());
            Assert.Equal(0, _credit.GetAccountState());
        }

        //test for not enough funds to pay installment
    }
}
