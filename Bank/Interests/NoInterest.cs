using Bank.Interfaces;

namespace Bank.Interests
{
    public class NoInterest : IInterest
    {
        public double ChargeInterest(double amount)
        {
            return amount;
        }
    }
}