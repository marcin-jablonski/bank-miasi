using Bank.Interfaces;
using Bank.Mechanisms.Decorators;
using Bank.Products;

namespace Bank.Mechanisms.Visitors
{
    public class AccountsInfoVisitor : IVisitor
    {
        public int TotalAccounts { get; private set; }
        public int DebitAccounts { get; private set; }
        public int DepositAccounts { get; private set; }
        public int CreditAccounts { get; private set; }

        public void Visit(BankProductDecorator account)
        {
            TotalAccounts++;
            if (account.GetType() == typeof(DebitAccount)) DebitAccounts++;
        }

        public void Visit(Deposit account)
        {
            TotalAccounts++;
            DepositAccounts++;
        }

        public void Visit(Credit account)
        {
            TotalAccounts++;
            CreditAccounts++;
        }
    }
}