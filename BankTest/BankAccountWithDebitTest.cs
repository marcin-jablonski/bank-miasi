using Bank.Mechanisms;
using Bank.Products;
using Xunit;

namespace BankTest
{
    public class BankAccountWithDebitTest
    {
        public BankAccountWithDebitTest()
        {
            var bank = new Bank.Bank();
            _bankAccount = new BankAccount(bank, 0);
            _debit = new Debit(DebitLimit);
            _bankAccount.CreateDebit(_debit);
        }

        private readonly BankAccount _bankAccount;
        private readonly Debit _debit;
        private const double ToWithdraw = 40.31;
        private const double DepositAmount = 50.00;
        private const double DebitLimit = 200.00;
        private const double DebitToIncrease = 100.00;

        [Fact]
        public void ShouldReduceDebitByDeposit()
        {
            _debit.IncreaseDebit(DebitToIncrease); //100
            _bankAccount.CreateDebit(_debit);
            _bankAccount.Deposit(DepositAmount); //50

            const double expectedAmount = 0.00;

            Assert.Equal(DebitToIncrease - DepositAmount, _debit.GetUnpaidDebit());
            Assert.Equal(expectedAmount, _bankAccount.GetAccountState());
        }


        [Fact]
        public void ShouldReduceDebitByDepositAndDeposit()
        {
            const double depositAmount2 = DebitToIncrease + DepositAmount; //150
            _debit.IncreaseDebit(DebitToIncrease); //100
            _bankAccount.CreateDebit(_debit);
            _bankAccount.Deposit(depositAmount2); //150

            Assert.Equal(0.00, _debit.GetUnpaidDebit());
            Assert.Equal(depositAmount2 - DebitToIncrease, _bankAccount.GetAccountState());
        }

        [Fact]
        public void WithdrawDebit()
        {
            _bankAccount.Withdraw(ToWithdraw);
            Assert.Equal(ToWithdraw, _debit.GetUnpaidDebit());
        }
    }
}