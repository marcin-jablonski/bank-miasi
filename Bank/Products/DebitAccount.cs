using System;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Mechanisms;
using Bank.Mechanisms.Decorators;
using Bank.Models;

namespace Bank.Products
{
    public class DebitAccount : BankProductDecorator
    {
        public Debit Debit { private set; get; }

        public DebitAccount(BankProductDecorator product, Debit debit) : base(product)
        {
            Debit = debit;
            bankProduct.GetHistory().Add(new Operation
            {
                Type = OperationType.DebitCreation,
                Date = DateTime.Now,
                Description = Debit.GetLimit().ToString()
            });
            bankProduct.GetBank().GetHistory()
                .Add(new Operation
                {
                    Type = OperationType.DebitCreation,
                    Date = DateTime.Now,
                    Description = Debit.GetLimit().ToString()
                });
        }

        public override void Accept(IVisitor visitor)
        {
            bankProduct.Accept(visitor);
        }

        public override void Withdraw(double amount)
        {
            if (amount >= bankProduct.Amount)
            {
                var toGetFromDebit = amount - bankProduct.Amount;
                if (Debit.GetAvailableDebit() >= toGetFromDebit)
                {
                    if (bankProduct.Amount > 0)
                        bankProduct.Withdraw(bankProduct.Amount);
                    Debit.IncreaseDebit(toGetFromDebit);
                }
                else
                {
                    throw new NotEnoughFundsException();
                }
            }
            else
            {
                bankProduct.Withdraw(amount);
            }
        }

        public override void Deposit(double amount)
        {
            if (Debit.GetUnpaidDebit() >= amount)
                Debit.ReduceDebit(amount);
            else
            {
                var toDeposit = amount - Debit.GetUnpaidDebit();
                Debit.ReduceDebit(Debit.GetUnpaidDebit());
                bankProduct.Deposit(toDeposit);
            }
        }

        public override double GetAccountState()
        {
            return bankProduct.GetAccountState();
        }
    }
}