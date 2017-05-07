using Bank.Exceptions;
using Bank.Mechanisms.Interests;
using Bank.Mechanisms.Kir;
using Bank.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankTest
{
    public class ElixirTransferOperationTest
    {
        private readonly BankAccount _bank1Account;
        private readonly BankAccount _bank2Account;
        private const double DepositAmount = 100;
        private const double AmountToTransfer = 30;
        private List<Bank.Bank> _banks;
        private readonly Bank.Bank _bank1;
        private readonly Bank.Bank _bank2;

        public ElixirTransferOperationTest()
        {
            _banks = new List<Bank.Bank>();
            _bank1 = new Bank.Bank();
            _bank1Account = new BankAccount(_bank1, 0, new NoInterest());
            _bank1.GetProducts().Add(_bank1Account);
            _banks.Add(_bank1);
            _bank2 = new Bank.Bank();
            _bank2Account = new BankAccount(_bank2, 0, new NoInterest());
            _bank2.GetProducts().Add(_bank2Account);
            _banks.Add(_bank2);
        }

        [Fact]
        public void ShouldTransfer()
        {
            _bank1Account.Deposit(DepositAmount);
            new ElixirTransferOperation().Transfer(
                _banks,
                _bank1Account.GetIdentificator(),
                _bank2Account.GetIdentificator(),
                AmountToTransfer
                );

            Assert.Equal(DepositAmount - AmountToTransfer, _bank1Account.GetAccountState());
            Assert.Equal(AmountToTransfer, _bank2Account.GetAccountState());
        }

        [Fact]
        public void ShouldNotFindSourceAccount()
        {
            Assert.Throws(typeof(NoSuchBankProductException), () =>
                      new ElixirTransferOperation().Transfer(
                        _banks,
                        new BankAccount(_bank1, 0, new NoInterest()).GetIdentificator(),
                        _bank2Account.GetIdentificator(),
                        AmountToTransfer
                        )
            );
        }








    }
}
