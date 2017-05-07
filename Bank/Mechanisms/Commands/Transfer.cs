using Bank.Interfaces;
using Bank.Products;
using Bank.Mechanisms.Kir;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Mechanisms.Commands
{
    public class Transfer : ICommand
    {
        private BankAccount _source;
        private BankAccount _destination;
        private double _amount;

        public Transfer(BankAccount source, BankAccount destination, double amount)
        {
            this._source = source;
            this._destination = destination;
            this._amount = amount;
        }

        public void Execute()
        {
            Kir.Kir.Transfer(_source.GetIdentificator(), _destination.GetIdentificator(), _amount);
        }
    }
}
