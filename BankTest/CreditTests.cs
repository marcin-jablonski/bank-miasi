using Bank.Exceptions;
using Bank.Mechanisms.Interests;
using Bank.Mechanisms.Kir;
using Bank.Products;
using Xunit;

namespace BankTest
{
    public class CreditTests
    {
        public CreditTests()
        {
            const int ownerId = 1;
            var bank = new Bank.Bank();
            _account = new BankAccount(bank, ownerId, new NoInterest());
            bank.CreateBankProduct(_account);
            _credit = new Credit(bank, _account, new NoInterest());
            bank.CreateBankProduct(_credit);
            Kir.AddBank(bank);
        }

        private readonly BankAccount _account;
        private readonly Credit _credit;

        [Fact]
        public void ShouldGetMoneyFromCreditAccount()
        {
            const int creditAmount = 200;
            _credit.GetMoney(creditAmount);

            Assert.Equal(200, _credit.GetAccountState());
            Assert.Equal(200, _account.GetAccountState());
        }

        [Fact]
        public void ShouldPayInstallment()
        {
            const int accountState = 2000;
            const int creditAmount = 500;
            _account.Deposit(accountState);
            _credit.GetMoney(creditAmount);

            _credit.PayCreditInstallment();

            Assert.Equal(accountState, _account.GetAccountState());
            Assert.Equal(0, _credit.GetAccountState());
        }

        [Fact]
        public void ShouldNotPayInstallmentDueToInsufficientFunds()
        {
            const int creditAmount = 500;
            _credit.GetMoney(creditAmount);
            _account.Withdraw(350);

            Assert.Throws(typeof (NotEnoughFundsException), () => _credit.PayCreditInstallment());
        }
    }
}