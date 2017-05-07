using Bank.Mechanisms.Decorators;
using Bank.Products;

namespace Bank.Interfaces
{
    public interface IVisitor
    {
        void Visit(BankProductDecorator account);
        void Visit(Deposit account);
        void Visit(Credit account);
    }
}