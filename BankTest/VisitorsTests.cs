using System;
using Bank.Mechanisms;
using Bank.Mechanisms.Interests;
using Bank.Mechanisms.Visitors;
using Bank.Products;
using Xunit;
using BankClass = Bank.Bank;

namespace BankTest
{
    public class VisitorsTests
    {
        private CumulativeVisitor cumulativeVisitor;
        private AggregativeVisitor aggregativeVisitor;
        private AccountsInfoVisitor accountsInfoVisitor;
        private const int OwnerId = 1;

        public VisitorsTests()
        {
            cumulativeVisitor = new CumulativeVisitor();
            aggregativeVisitor = new AggregativeVisitor();
            accountsInfoVisitor = new AccountsInfoVisitor();
        }

        [Fact]
        public void ShouldGetCorrectInfoAboutBankAccount()
        {
            const int deposit = 100;
            var bankAccount = new BankAccount(new BankClass(), OwnerId, new NoInterest());
            bankAccount.Deposit(deposit);

            bankAccount.Accept(cumulativeVisitor);
            bankAccount.Accept(aggregativeVisitor);
            bankAccount.Accept(accountsInfoVisitor);

            Assert.Equal(deposit, cumulativeVisitor.Result);
            Assert.Equal(deposit, aggregativeVisitor.AccountStates);
            Assert.Equal(0, aggregativeVisitor.TotalCredits);
            Assert.Equal(0, aggregativeVisitor.TotalDebit);
            Assert.Equal(0, aggregativeVisitor.TotalDeposits);
            Assert.Equal(0, aggregativeVisitor.TotalInstallmentIncomes);
            Assert.Equal(1, accountsInfoVisitor.TotalAccounts);
            Assert.Equal(0, accountsInfoVisitor.CreditAccounts);
            Assert.Equal(0, accountsInfoVisitor.DebitAccounts);
            Assert.Equal(0, accountsInfoVisitor.DepositAccounts);
        }

        [Fact]
        public void ShouldGetCorrectInfoAboutDebitAccount()
        {
            const int withdrawAmount = 100;
            var debitAccount = new DebitAccount(new BankAccount(new BankClass(), OwnerId, new NoInterest()), new Debit(200));
            debitAccount.Withdraw(withdrawAmount);

            debitAccount.Accept(cumulativeVisitor);
            debitAccount.Accept(aggregativeVisitor);
            debitAccount.Accept(accountsInfoVisitor);

            Assert.Equal(-withdrawAmount, cumulativeVisitor.Result);
            Assert.Equal(0, aggregativeVisitor.AccountStates);
            Assert.Equal(0, aggregativeVisitor.TotalCredits);
            Assert.Equal(withdrawAmount, aggregativeVisitor.TotalDebit);
            Assert.Equal(0, aggregativeVisitor.TotalDeposits);
            Assert.Equal(0, aggregativeVisitor.TotalInstallmentIncomes);
            Assert.Equal(1, accountsInfoVisitor.TotalAccounts);
            Assert.Equal(0, accountsInfoVisitor.CreditAccounts);
            Assert.Equal(1, accountsInfoVisitor.DebitAccounts);
            Assert.Equal(0, accountsInfoVisitor.DepositAccounts);
        }

        [Fact]
        public void ShouldGetCorrectInfoAboutDepositAccount()
        {
            const int depositAmount = 100;
            var bank = new BankClass();
            var bankAccount = new BankAccount(bank, OwnerId, new NoInterest());
            var deposit = new Deposit(bank, bankAccount, new NoInterest(), DateTime.Today + TimeSpan.FromDays(1),
                depositAmount);

            deposit.Accept(cumulativeVisitor);
            deposit.Accept(aggregativeVisitor);
            deposit.Accept(accountsInfoVisitor);

            Assert.Equal(depositAmount, cumulativeVisitor.Result);
            Assert.Equal(0, aggregativeVisitor.AccountStates);
            Assert.Equal(0, aggregativeVisitor.TotalCredits);
            Assert.Equal(0, aggregativeVisitor.TotalDebit);
            Assert.Equal(depositAmount, aggregativeVisitor.TotalDeposits);
            Assert.Equal(0, aggregativeVisitor.TotalInstallmentIncomes);
            Assert.Equal(1, accountsInfoVisitor.TotalAccounts);
            Assert.Equal(0, accountsInfoVisitor.CreditAccounts);
            Assert.Equal(0, accountsInfoVisitor.DebitAccounts);
            Assert.Equal(1, accountsInfoVisitor.DepositAccounts);
        }

        [Fact]
        public void ShouldGetCorrectInfoAboutCreditAccount()
        {
            const int creditAmount = 100;
            var bank = new BankClass();
            var bankAccount = new BankAccount(bank, OwnerId, new NoInterest());
            var credit = new Credit(bank, bankAccount, new _5PercentInterest());

            credit.GetMoney(creditAmount);

            credit.Accept(cumulativeVisitor);
            credit.Accept(aggregativeVisitor);
            credit.Accept(accountsInfoVisitor);

            Assert.Equal(-creditAmount, cumulativeVisitor.Result);
            Assert.Equal(0, aggregativeVisitor.AccountStates);
            Assert.Equal(creditAmount, aggregativeVisitor.TotalCredits);
            Assert.Equal(0, aggregativeVisitor.TotalDebit);
            Assert.Equal(0, aggregativeVisitor.TotalDeposits);
            Assert.Equal(creditAmount * 0.05, aggregativeVisitor.TotalInstallmentIncomes);
            Assert.Equal(1, accountsInfoVisitor.TotalAccounts);
            Assert.Equal(1, accountsInfoVisitor.CreditAccounts);
            Assert.Equal(0, accountsInfoVisitor.DebitAccounts);
            Assert.Equal(0, accountsInfoVisitor.DepositAccounts);
        }
    }
}