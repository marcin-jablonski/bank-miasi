using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Interfaces;

namespace Bank.Mechanisms
{
    class Reporter : IReporter
    {
        public List<BankProduct> CreateReport(List<BankProduct> products, Func<BankProduct, bool> filter)
        {
            return products.Where(filter).ToList();
        }
    }
}
