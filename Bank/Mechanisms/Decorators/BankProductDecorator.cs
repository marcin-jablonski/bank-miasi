using Bank.Interfaces;

namespace Bank.Mechanisms.Decorators
{
    public abstract class BankProductDecorator : BankProduct
    {
        protected BankProductDecorator bankProduct;

        protected BankProductDecorator(Bank bank, int ownerId, IInterest interestSystem)
            : base(bank, ownerId, interestSystem)
        {
            
        }

        protected BankProductDecorator(BankProductDecorator product)
        {
            bankProduct = product;
        }

        public abstract void Withdraw(double amount);
        public abstract void Deposit(double amount);
        public new abstract double GetAccountState();
    }
}