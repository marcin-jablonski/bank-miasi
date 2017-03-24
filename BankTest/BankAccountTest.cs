using Bank.Exceptions;
using Bank.Products;
using Xunit;

namespace BankTest
{
    public class BankAccountTest
    {
        public BankAccountTest()
        {
            var bank = new Bank.Bank();
            _bankAccount = new BankAccount(bank, 0);
            _bankAccount2 = new BankAccount(bank, 1);
        }

        private readonly BankAccount _bankAccount;
        private readonly BankAccount _bankAccount2;
        private const double ToWithdraw = 40.31;
        private const double DepositAmount = 100.52;
        private const double AmountToTransfer = 23.11;

        [Fact]
        public void ChargeInterest()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.ChargeInterest();
            Assert.Equal(DepositAmount, _bankAccount.GetAccountState());
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
        public void ShouldWithdraw()
        {
            _bankAccount.Deposit(DepositAmount);
            _bankAccount.Withdraw(ToWithdraw);
            Assert.Equal(DepositAmount - ToWithdraw, _bankAccount.GetAccountState());
        }
    }
}