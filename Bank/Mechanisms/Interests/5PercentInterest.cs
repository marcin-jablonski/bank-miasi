using Bank.Interfaces;

namespace Bank.Mechanisms.Interests
{
    public class _5PercentInterest : IInterest
    {
        public double ChargeInterest(double amount)
        {
            return amount * 1.05;
        }
    }
}
