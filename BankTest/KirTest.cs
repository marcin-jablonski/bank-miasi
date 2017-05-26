using Bank.Mechanisms.Interests;
using Bank.Mechanisms.Kir;
using Bank.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace BankTest
{
    public class KirTest
    {
        private readonly BankAccount _bank1Account;
        private readonly BankAccount _bank1Account2;
        private readonly BankAccount _bank2Account;
        private const double DepositAmount = 100;
        private const double AmountToTransfer = 30;

        public KirTest()
        {
            var bank1 = new Bank.Bank();
            _bank1Account = new BankAccount(bank1, 0, new NoInterest());
            bank1.GetProducts().Add(_bank1Account);
            _bank1Account2 = new BankAccount(bank1, 1, new NoInterest());          
            bank1.GetProducts().Add(_bank1Account2);
            Kir.AddBank(bank1);

            var bank2 = new Bank.Bank();
            _bank2Account = new BankAccount(bank2, 0, new NoInterest());
            bank2.GetProducts().Add(_bank2Account);
            Kir.AddBank(bank2);
        }

        [Fact]
        public void ShouldTransferWithinOneBank()
        {      
            _bank1Account.Deposit(DepositAmount);
            _bank1Account.Transfer(AmountToTransfer, _bank1Account2);

            Assert.Equal(DepositAmount - AmountToTransfer - 2 , _bank1Account.GetAccountState());
            Assert.Equal(AmountToTransfer, _bank1Account2.GetAccountState());
        }

        [Fact]
        public void ShouldTransferBetweenBanks()
        {
            _bank1Account.Deposit(DepositAmount);
            _bank1Account.Transfer(AmountToTransfer, _bank2Account);

            Assert.Equal(DepositAmount - AmountToTransfer - 2, _bank1Account.GetAccountState());
            Assert.Equal(AmountToTransfer, _bank2Account.GetAccountState());
        }
    }
}
