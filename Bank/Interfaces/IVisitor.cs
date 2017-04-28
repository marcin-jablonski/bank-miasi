using Bank.Products;

namespace Bank.Interfaces
{
    public interface IVisitor
    {
        void Visit(BankAccount account);
        void Visit(Deposit account);
        void Visit(Credit account);
    }
}