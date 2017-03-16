using Bank.Interfaces;

namespace Bank.Mechanisms.Interests
{
    public class NoInterest : IInterest
    {
        public double ChargeInterest(double amount)
        {
            return amount;
        }
    }
}