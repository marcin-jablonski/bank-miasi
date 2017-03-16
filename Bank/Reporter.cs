using Bank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank
{
    class Reporter : IReporter
    {
        public List<BankAccount> CreateReport(List<BankAccount> products, Func<BankAccount, bool> filter)
        {
            return products.Where(filter).ToList();
        }
    }
}
