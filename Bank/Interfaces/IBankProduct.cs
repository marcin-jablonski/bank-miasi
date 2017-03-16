namespace Bank.Interfaces
{
    public interface IBankProduct
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void Transfer(double amount, IBankProduct destination);
        void ChangeInterestSystem(IInterest interest);
        void ChargeInterest();
        void CancelDeposit();
        void CreateCredit();
        void PayCreditInstallment(double amount);
        void CreateDebit(IDebit debit);
        int GetId();
        int GetOwnerId();
    }
}