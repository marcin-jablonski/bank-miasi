using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Interfaces
{
    public interface IReporter
    {
        List<IBankProduct> CreateReport(List<IBankProduct> products, Func<IBankProduct, bool> filter);
    }
}