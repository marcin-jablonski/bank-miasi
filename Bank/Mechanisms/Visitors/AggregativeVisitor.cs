using Bank.Interfaces;
using Bank.Products;

namespace Bank.Mechanisms.Visitors
{
    public class AggregativeVisitor : IVisitor
    {
        public void Visit(BankAccount account)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Deposit account)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Credit account)
        {
            throw new System.NotImplementedException();
        }
    }
}