using Bank.Enums;
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
        private readonly int _expectedHistorySize = 1;
        private readonly int _ownerId = 2;

        [Fact]
        public void ShouldCreateBankProduct()
        {
            Assert.NotNull(_bank.CreateBankProduct(BankProductType.Account, 1));
        }

        [Fact]
        public void ShouldUpdateHistoryAfterReport()
        {
            _bank.CreateReport(bankProduct => true);
            Assert.Equal(_expectedHistorySize, _bank.GetHistory().Count);
        }

        [Fact]
        public void ShouldReturnProductsForOwner()
        {
            for (var i = 0; i < 5; i++)
            {
                _bank.CreateBankProduct(BankProductType.Account, _ownerId);
            }

            Assert.Equal(5, _bank.GetProductsByOwner(_ownerId).Count);
        }
    }
}