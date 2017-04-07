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

            if (destinationAccount.Debit == null || (destinationAccount.Debit != null && destinationAccount.Debit.GetUnpaidDebit() == 0))
                destinationAccount.Amount += amountToDeposit;
            else if (destinationAccount.Debit.GetUnpaidDebit() >= 0)
            {
                if (destinationAccount.Debit.GetUnpaidDebit() >= amountToDeposit)
                    destinationAccount.Debit.ReduceDebit(amountToDeposit);
                else
                {
                    var toDeposit = amountToDeposit - destinationAccount.Debit.GetUnpaidDebit();
                    destinationAccount.Debit.ReduceDebit(destinationAccount.Debit.GetUnpaidDebit());
                    destinationAccount.Amount += toDeposit;
                }
            }
        }
    }
}