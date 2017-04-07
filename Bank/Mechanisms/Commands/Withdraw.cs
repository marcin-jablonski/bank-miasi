using System;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Models;
using Bank.Products;

namespace Bank.Mechanisms.Commands
{
    internal class Withdraw : ICommand
    {
        private BankAccount _account;

        private double _amount;

        public Withdraw(BankAccount account, double amount)
        {
            this._account = account;
            this._amount = amount;
        }

        public void Execute()
        {
            if (_amount <= 0) throw new IllegalOperationException();

            if (_amount >= _amount)
                _amount -= _amount;
            else if (Debit != null)
            {
                var toGetFromDebit = _amount - _amount;
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
    }
}