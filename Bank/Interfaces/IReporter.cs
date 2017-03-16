using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Interfaces
{
    public interface IReporter
    {
        List<BankAccount> CreateReport(List<BankAccount> products, Func<BankAccount, bool> filter);
    }
}