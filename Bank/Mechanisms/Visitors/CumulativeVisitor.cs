using Bank.Interfaces;
using Bank.Mechanisms.Decorators;
using Bank.Products;

namespace Bank.Mechanisms.Visitors
{
    public class CumulativeVisitor : IVisitor
    {
        public double Result { get; private set; }

        public void Visit(BankProductDecorator account)
        {
            if (account.GetType() == typeof(DebitAccount))
                Result -= ((DebitAccount) account).Debit.GetUnpaidDebit();
            Result += account.GetAccountState();
        }

        public void Visit(Deposit account)
        {
            Result += account.Amount;
        }

        public void Visit(Credit account)
        {
            Result -= account.Amount;
        }
    }
}