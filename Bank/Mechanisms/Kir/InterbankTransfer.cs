using System;
using Bank.Models;

namespace Bank.Mechanisms.Kir
{
    public class InterbankTransfer : Operation
    {
        public Guid SourceBankId { get; set; }

        public int SourceOwnerId { get; set; }

        public Guid DestinationBankId { get; set; }

        public int DestinationOwnerId { get; set; }

        public double Amount { get; set; }
    }
}