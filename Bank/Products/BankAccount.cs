using System;
using System.Collections.Generic;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Mechanisms.Interests;
using Bank.Models;

namespace Bank.Products
{
    public class BankAccount : IBankProduct
    {
        private int _id;

        private int _ownerId;

        private List<Operation> History;

        private IBank Bank;

        private IInterest Interest;

        private IDebit Debit;

        private double _amount;

        public BankAccount(IBank bank, int ownerId, int id)
        {
            Bank = bank;
            _ownerId = ownerId;
            _id = id;
            _amount = 0;
            History = new List<Operation>();
            Interest = new NoInterest();
        }

        public void Deposit(double amount)
        {
            if (Debit == null)
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

            History.Add(new Operation {Type = OperationType.Deposit, Date = DateTime.Now, Description = amount.ToString()});
            Bank.GetHistory().Add(new Operation { Type = OperationType.Deposit, Date = DateTime.Now, Description = amount.ToString() });
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

            History.Add(new Operation { Type = OperationType.Withdraw, Date = DateTime.Now, Description = amount.ToString() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.Withdraw, Date = DateTime.Now, Description = amount.ToString() });
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
            History.Add(new Operation { Type = OperationType.Transfer, Date = DateTime.Now, Description = amount + " to " + destination.GetId() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.Transfer, Date = DateTime.Now, Description = amount + " to " + destination.GetId() });
        }

        public void ChangeInterestSystem(IInterest interest)
        {
            Interest = interest;
            History.Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
        }

        public void ChargeInterest()
        {
            var oldAmount = _amount;
            _amount = Interest.ChargeInterest(_amount);
            History.Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description =  (_amount - oldAmount).ToString()});
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description = (_amount - oldAmount).ToString() });
        }

        public void CancelDeposit()
        {
            throw new NotImplementedException();
        }

        public void CreateCredit()
        {
            throw new NotImplementedException();
        }

        public void PayCreditInstallment(double amount)
        {
            throw new NotImplementedException();
        }

        public void CreateDebit(IDebit debit)
        {
            Debit = debit;
            History.Add(new Operation { Type = OperationType.DebitCreation, Date = DateTime.Now, Description = debit.GetLimit().ToString() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.DebitCreation, Date = DateTime.Now, Description = debit.GetLimit().ToString() });
        }

        public int GetId()
        {
            return _id;
        }

        public int GetOwnerId()
        {
            return _ownerId;
        }

        public double GetAccountState()
        {
            return _amount;
        }
    }
}