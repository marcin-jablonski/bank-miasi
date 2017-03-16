using System;
using Bank.Exceptions;
using Bank.Interfaces;

namespace Bank
{
    public class BankAccount : IBankProduct
    {
        private IInterest Interest;

        private IDebit Debit;

        private double _amount;

        public void Deposit(double amount)
        {
            if (Debit != null)
                _amount += amount;
            else if (Debit.GetUnpaidDebit() >= 0)
            {
                if(Debit.GetUnpaidDebit() >= amount)
                    Debit.ReduceDebit(amount);
                else
                {
                    var toDeposit = amount - Debit.GetUnpaidDebit();
                    Debit.ReduceDebit(Debit.GetUnpaidDebit());
                    _amount += toDeposit;
                }
            }
        }

        public void Withdraw(double amount)
        {
            if (_amount >= amount)
                _amount -= amount;
            else if (Debit != null)
            {
                var toGetFromDebit = amount - _amount;
                if (Debit.GetAvailableDebit() >= toGetFromDebit)
                {
                    _amount = 0;
                    Debit.IncreaseDebit(toGetFromDebit);
                }
                else
                {
                    throw new NotEnoughFundsException();
                }
            }
            else
            {
                throw new NotEnoughFundsException();
            }
        }

        public void Transfer(double amount, IBankProduct destination)
        {
            try
            {
                Withdraw(amount);
            }
            catch (Exception)
            {
                throw;
            }

            destination.Deposit(amount);
        }

        public void CreateInterest()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInterestSystem()
        {
            throw new System.NotImplementedException();
        }

        public void ChargeInterest()
        {
            _amount = Interest.ChargeInterest(_amount);
        }

        public void CancelDeposit()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCredit()
        {
            throw new System.NotImplementedException();
        }

        public void PayCreditInstallment(double amount)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDebit(IDebit debit)
        {
            Debit = debit;
        }
    }
}