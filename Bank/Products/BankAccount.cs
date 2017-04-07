using System;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Models;
using Bank.Mechanisms;
using Commands = Bank.Mechanisms.Commands;

namespace Bank.Products
{
    public class BankAccount : BankProduct
    {
        private Debit Debit;

        public BankAccount(Bank bank, int ownerId, IInterest interestSystem)
            : base(bank, ownerId, interestSystem)
        {
        }

        public void Deposit(double amount)
        {
            if (amount <= 0) throw new IllegalOperationException();

            if (Debit == null || (Debit != null && Debit.GetUnpaidDebit() == 0))
                _amount += amount;
            else if (Debit.GetUnpaidDebit() >= 0)
            {
                if (Debit.GetUnpaidDebit() >= amount)
                    Debit.ReduceDebit(amount);
                else
                {
                    var toDeposit = amount - Debit.GetUnpaidDebit();
                    Debit.ReduceDebit(Debit.GetUnpaidDebit());
                    _amount += toDeposit;
                }
            }

            History.Add(new Operation
            {
                Type = OperationType.Deposit,
                Date = DateTime.Now,
                Description = amount.ToString()
            });
            Bank.GetHistory()
                .Add(new Operation {Type = OperationType.Deposit, Date = DateTime.Now, Description = amount.ToString()});
        }

        public void Withdraw(double amount)
        {
            new Commands.Withdraw(this, amount).Execute();

            History.Add(new Operation
            {
                Type = OperationType.Withdraw,
                Date = DateTime.Now,
                Description = _amount.ToString()
            });
            Bank.GetHistory()
                .Add(new Operation
                {
                    Type = OperationType.Withdraw,
                    Date = DateTime.Now,
                    Description = _amount.ToString()
                });
        }

        public void Transfer(double amount, BankAccount destination)
        {
            try
            {
                Withdraw(amount);
            }
            catch (Exception)
            {
                throw;
            }

            destination.Deposit(amount); // change possibly
            History.Add(new Operation
            {
                Type = OperationType.Transfer,
                Date = DateTime.Now,
                Description = amount + " to " + destination.GetId()
            });
            Bank.GetHistory()
                .Add(new Operation
                {
                    Type = OperationType.Transfer,
                    Date = DateTime.Now,
                    Description = amount + " to " + destination.GetId()
                });
        }

        public void CreateDebit(Debit debit)
        {
            Debit = debit;
            History.Add(new Operation
            {
                Type = OperationType.DebitCreation,
                Date = DateTime.Now,
                Description = debit.GetLimit().ToString()
            });
            Bank.GetHistory()
                .Add(new Operation
                {
                    Type = OperationType.DebitCreation,
                    Date = DateTime.Now,
                    Description = debit.GetLimit().ToString()
                });
        }
    }
}