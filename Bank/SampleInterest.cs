using Bank.Interfaces;

namespace Bank
{
    public class SampleInterest : IInterest
    {
        public double ChargeInterest(double amount)
        {
            return amount*1.1;
        }
    }
}