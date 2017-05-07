using Bank.Enums;
using Bank.Interfaces;
using Bank.Models;
using System;

namespace Bank.Products
{
    public class Credit : BankProduct
    {
        private BankAccount _account;

        public Credit(Bank bank, BankAccount account, IInterest interestSystem) 
            : base(bank, account.GetOwnerId(), interestSystem)
        {
            _account = account;
        }

        public void GetMoney(double amount)
        {
            Amount += amount;
            _account.Deposit(amount);
        }

        public void PayCreditInstallment()
        {
            ChargeInterest();
            _account.Withdraw(Amount);
            Amount = 0;
            History.Add(new Operation { Type = OperationType.CreditInstallmentPayment, Date = DateTime.Now, Description = "Installment payment for credit " + GetId() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.CreditInstallmentPayment, Date = DateTime.Now, Description = "Installment payment for credit " + GetId() });
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double GetInstallment()
        {
            return Interest.ChargeInterest(Amount);
        }
    }
}