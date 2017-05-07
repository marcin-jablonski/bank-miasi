using Bank.Exceptions;
using Bank.Interfaces;
using Bank.Products;

namespace Bank.Mechanisms.Commands
{
    public class Deposit : ICommand
    {
        private readonly double amountToDeposit;
        private readonly BankAccount destinationAccount;

        public Deposit(BankAccount account, double amount)
        {
            destinationAccount = account;
            amountToDeposit = amount;
        }

        public void Execute()
        {
            if (amountToDeposit <= 0) throw new IllegalOperationException();

            destinationAccount.Amount += amountToDeposit;
        }
    }
}