using System;
using Bank.Mechanisms.Interests;
using Xunit;
using Bank.Products;
using Bank.Exceptions;

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
            var bankProduct = new BankAccount(_bank, FirstOwnerId, new NoInterest());
            var account = _bank.CreateBankProduct(bankProduct);
            Assert.NotNull(account);
            Assert.Equal(account.GetOwnerId(), FirstOwnerId);
        }

        [Fact]
        public void ShouldReturnBankProductForId()
        {
            var bankProduct = new BankAccount(_bank, FirstOwnerId, new NoInterest());
            _bank.CreateBankProduct(bankProduct);
            var product = _bank.GetBankProduct(1);
            Assert.NotNull(product);
            Assert.Equal(1, product.GetId());
        }

        [Fact]
        public void ShouldReturnProductsForOwner()
        {
            var bankProduct = new BankAccount(_bank, SecondOwnerId, new NoInterest());
            for (var i = 0; i < 5; i++)
                _bank.CreateBankProduct(bankProduct);
            var products = _bank.GetProductsByOwner(SecondOwnerId);
            Assert.NotNull(products);
            Assert.Equal(5, products.Count);
        }

        [Fact]
        public void ShouldThrowExceptionOnGetProductWithEmptyBank()
        {
            Assert.Throws(typeof(NoSuchBankProductException), () => _bank.GetBankProduct(1));
        }

        [Fact]
        public void ShouldReturnEmptyListOnNonExistingOwnerId()
        {
            var products = _bank.GetProductsByOwner(FirstOwnerId);
            Assert.Equal(0, products.Count);
        }
    }
}