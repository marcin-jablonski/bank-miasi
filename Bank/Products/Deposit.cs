using Bank.Enums;
using Bank.Interfaces;
using Bank.Models;
using System;

namespace Bank.Products
{
    public class Deposit : BankProduct
    {
        private BankAccount _account;

        private DateTime _to;

        private bool _isActive;

        public Deposit(Bank bank, BankAccount account, IInterest interestSystem, DateTime to, double amount) 
            : base(bank, account.GetOwnerId(), interestSystem)
        {
            _amount = amount;
            _to = to;
            _isActive = true;
            _account = account;
        }

        public void CancelDeposit()
        {
            if (_to < DateTime.Now)
                ChargeInterest();
            _account.Deposit(_amount);
            _isActive = false;

            History.Add(new Operation { Type = OperationType.DepositCancellation, Date = DateTime.Now, Description = "Cancelled deposit id " + GetId() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.DepositCancellation, Date = DateTime.Now, Description = "Cancelled deposit id " + GetId() });
        }
    }
}