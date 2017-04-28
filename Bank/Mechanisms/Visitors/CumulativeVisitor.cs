using Bank.Interfaces;
using Bank.Products;

namespace Bank.Mechanisms.Visitors
{
    public class CumulativeVisitor : IVisitor
    {
        public double Result { get; private set; }

        public void Visit(BankAccount account)
        {
            Result += account.Amount;
        }

        public void Visit(Deposit account)
        {
            Result += account.Amount;
        }

        public void Visit(Credit account)
        {
            Result += account.Amount;
        }
    }
}