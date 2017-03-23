using System;
using System.Collections.Generic;
using System.Text;
using Bank.Exceptions;
using Xunit;

namespace BankTest
{
    public class BankAccountWithDebitTest
    {
        private Bank.Bank _bank = null;
        private Bank.Products.BankAccount _bankAccount = null;
        private Bank.Mechanisms.Debit _debit = null;
        private const double ToWithdraw = 40.31;
        private const double DepositAmount = 50.00;
        private const double DebitLimit = 200.00;
        private const double DebitToIncrease = 100.00;

        public BankAccountWithDebitTest()
        {
            _bank = new Bank.Bank();
            _bankAccount = new Bank.Products.BankAccount(_bank, 0, 0);
            _debit = new Bank.Mechanisms.Debit(DebitLimit);
            _bankAccount.CreateDebit(_debit);
        }

        [Fact]
        public void ShouldReduceDebitByDeposit()
        {
            _debit.IncreaseDebit(DebitToIncrease); //100
            _bankAccount.CreateDebit(_debit);
            _bankAccount.Deposit(DepositAmount); //50

            double expectedAmount = 0.00;
            if (DebitToIncrease >= DepositAmount)
            {
                expectedAmount = 0.00;
            }
            else expectedAmount = DepositAmount - DebitToIncrease;

            Assert.Equal(DebitToIncrease-DepositAmount, _debit.GetUnpaidDebit());
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
            Assert.Equal(depositAmount2-DebitToIncrease, _bankAccount.GetAccountState());
        }

        [Fact]
        public void WithdrawDebit()
        {
            _bankAccount.Withdraw(ToWithdraw);
            Assert.Equal(ToWithdraw,_debit.GetUnpaidDebit());
        }

    }
}
