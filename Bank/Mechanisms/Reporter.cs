using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Interfaces;

namespace Bank.Mechanisms
{
    class Reporter : IReporter
    {
        public List<IBankProduct> CreateReport(List<IBankProduct> products, Func<IBankProduct, bool> filter)
        {
            return products.Where(filter).ToList();
        }
    }
}
