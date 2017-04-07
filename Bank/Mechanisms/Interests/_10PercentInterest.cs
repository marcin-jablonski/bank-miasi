using Bank.Interfaces;

namespace Bank.Mechanisms.Interests
{
    public class _10PercentInterest : IInterest
    {
        private const double InterestPercentBelowLimit = 1.1;
        private const int InterestAmountLimit = 10000;
        private const double InterestPercentAboveLimit = 1.05;

        public double ChargeInterest(double amount)
        {
            return amount < InterestAmountLimit
                ? amount * InterestPercentBelowLimit
                : amount * InterestPercentAboveLimit;
        }
    }
}