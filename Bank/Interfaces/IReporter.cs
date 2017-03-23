using System;
using System.Collections.Generic;

namespace Bank.Interfaces
{
    public interface IReporter
    {
        List<BankProduct> CreateReport(List<BankProduct> products, Func<BankProduct, bool> filter);
    }
}