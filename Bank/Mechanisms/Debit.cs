using System;
using Bank.Interfaces;
using Bank.Exceptions;

namespace Bank.Mechanisms
{
    public class Debit : IDebit
    {
        private double _limit;

        private double _currentDebit;

        public Debit(double limit)
        {
            _limit = limit;
            _currentDebit = 0;
        }

        public void ReduceDebit(double amount)
        {
            if(_currentDebit >= amount)
                _currentDebit -= amount;
            else
            {
                throw new IllegalOperationException();
            }
        }

        public void IncreaseDebit(double amount)
        {
            var newDebit = _currentDebit += amount;
            if (newDebit >= _limit)
                throw new IllegalOperationException();
            else _currentDebit = newDebit;
        }

        public double GetAvailableDebit()
        {
            return _limit - _currentDebit;
        }

        public double GetLimit()
        {
            return _limit;
        }

        public double GetUnpaidDebit()
        {
            return _currentDebit;
        }
    }
}
