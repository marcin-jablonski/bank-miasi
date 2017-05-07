using Bank.Interfaces;
using Bank.Mechanisms.Decorators;
using Bank.Products;

namespace Bank.Mechanisms.Visitors
{
    public class AggregativeVisitor : IVisitor
    {
        public double AccountStates { get; private set; }
        public double TotalDebit { get; private set; }
        public double TotalDeposits { get; private set; }
        public double TotalCredits { get; private set; }
        public double TotalInstallmentIncomes { get; private set; }

        public void Visit(BankProductDecorator account)
        {
            AccountStates += account.GetAccountState();
            TotalDebit += account.GetType() == typeof(DebitAccount) ? ((DebitAccount) account).Debit.GetUnpaidDebit() : 0;
        }

        public void Visit(Deposit account)
        {
            TotalDeposits += account.Amount;
        }

        public void Visit(Credit account)
        {
            TotalCredits += account.Amount;
            TotalInstallmentIncomes += account.GetInstallment() - account.Amount;
        }
    }
}