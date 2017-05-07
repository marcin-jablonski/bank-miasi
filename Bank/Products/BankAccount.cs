using System;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Models;
using Bank.Mechanisms;
using Bank.Mechanisms.Kir;
using Commands = Bank.Mechanisms.Commands;

namespace Bank.Products
{
    public class BankAccount : BankProduct
    {
        public Debit Debit { private set; get; }

        public BankAccount(Bank bank, int ownerId, IInterest interestSystem)
            : base(bank, ownerId, interestSystem)
        {
        }

        public void Deposit(double amount)
        {
           
            new Commands.Deposit(this, amount).Execute();

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
                Description = amount.ToString()
            });
            Bank.GetHistory()
                .Add(new Operation {Type = OperationType.Withdraw, Date = DateTime.Now, Description = amount.ToString()});
        }

        public void Transfer(double amount, BankAccount destination)
        {
            //Kir.Transfer(GetIdentificator(), destination.GetIdentificator(), amount);

            new Commands.Transfer(this, destination, amount).Execute();

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

        public new void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}