namespace Bank.Interfaces
{
    public interface IBankProduct
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void Transfer(double amount, int destinationId);
        void CreateInterest();
        void ChangeInterestSystem();
        void ChargeInterest();
        void CancelDeposit();
        void CreateCredit();
        void PayCreditInstallment(double amount);
        void CreateDebit();
    }
}