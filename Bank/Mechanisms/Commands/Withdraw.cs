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

            if (_account.GetAccountState() >= _amount)
                _account.Amount -= _amount;
            else
            {
                throw new NotEnoughFundsException();
            }
        }
    }
}