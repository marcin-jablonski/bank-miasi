using System;
using Bank.Enums;
using Bank.Interfaces;
using Xunit;

namespace BankTest
{
    public class BankTest
    {
        public BankTest()
        {
            _bank = new Bank.Bank();
        }

        private readonly Bank.Bank _bank;
        private const int ExpectedHistorySize = 1;
        private const int FirstOwnerId = 1;
        private const int SecondOwnerId = 2;

        [Fact]
        public void ShouldCreateBankProduct()
        {
            var account = _bank.CreateBankProduct(BankProductType.Account, FirstOwnerId);
            Assert.NotNull(account);
            Assert.Equal(account.GetOwnerId(), FirstOwnerId);
        }

        [Fact]
        public void ShouldReturnBankProductForId()
        {
            _bank.CreateBankProduct(BankProductType.Account, FirstOwnerId);
            var product = _bank.GetBankProduct(1);
            Assert.NotNull(product);
            Assert.Equal(1, product.GetId());
        }

        [Fact]
        public void ShouldReturnProductsForOwner()
        {
            for (var i = 0; i < 5; i++)
                _bank.CreateBankProduct(BankProductType.Account, SecondOwnerId);
            var products = _bank.GetProductsByOwner(SecondOwnerId);
            Assert.NotNull(products);
            Assert.Equal(5, products.Count);
        }

        [Fact]
        public void ShouldThrowExceptionOnGetProductWithEmptyBank()
        {
            Assert.Throws(typeof(InvalidOperationException), () => _bank.GetBankProduct(1));
        }

        [Fact]
        public void ShouldReturnEmptyListOnNonExistingOwnerId()
        {
            var products = _bank.GetProductsByOwner(FirstOwnerId);
            Assert.Equal(0, products.Count);
        }

        [Fact]
        public void ShouldUpdateHistoryAfterReport()
        {
            _bank.CreateReport(bankProduct => true);
            Assert.Equal(ExpectedHistorySize, _bank.GetHistory().Count);
        }
    }
}