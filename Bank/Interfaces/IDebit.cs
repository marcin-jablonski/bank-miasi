namespace Bank.Interfaces
{
    public interface IDebit
    {
        void ReduceDebit(double amount);
        void IncreaseDebit(double amount);
        double GetAvailableDebit();
        double GetLimit();
        double GetUnpaidDebit();
    }
}