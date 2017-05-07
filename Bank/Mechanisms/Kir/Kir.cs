using System.Collections.Generic;

namespace Bank.Mechanisms.Kir
{
    public static class Kir
    {
        private static List<Bank> _knownBanks;

        static Kir()
        {
            _knownBanks = new List<Bank>();
        }

        public static void Transfer(string sourceAccountId, string accountId, double amount)
        {
            new ElixirTransferOperation().Transfer(_knownBanks, sourceAccountId, accountId, amount);
        }

        public static void AddBank(Bank bank)
        {
            _knownBanks.Add(bank);
        }
    }
}