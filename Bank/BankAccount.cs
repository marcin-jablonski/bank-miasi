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
            else
            {
                //TODO: check if there is anything to reduce
                Debit.ReduceDebit(amount);
            }
        }

        public void Withdraw(double amount)
        {
            if (_amount >= amount)
                _amount -= amount;
            else if (Debit != null)
            {
                //TODO: partial debit/account state withdraw
                if(Debit.GetAvailableDebit() >= amount)
                    Debit.IncreaseDebit(amount);
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

        public void Transfer(double amount, int destinationId)
        {
            throw new System.NotImplementedException();
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

        public void CreateDebit()
        {
            throw new System.NotImplementedException();
        }
    }
}