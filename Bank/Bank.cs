using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Enums;
using Bank.Interfaces;
using Bank.Mechanisms;
using Bank.Models;
using Bank.Products;

namespace Bank
{
    public class Bank : IBank
    {
        private List<IBankProduct> Products;

        private IReporter Reporter;

        private List<Operation> History;

        public Bank()
        {
            Products = new List<IBankProduct>();
            History = new List<Operation>();
            Reporter = new Reporter();
        }

        public IBankProduct CreateBankProduct(BankProductType productType, int ownerId)
        {
            IBankProduct newProduct = null;
            switch (productType)
            {
                case BankProductType.Account:
                    newProduct = new BankAccount(this, ownerId, Products.Max(x => x.GetId()) + 1);
                    break;
            }
            Products.Add(newProduct);
            return newProduct;
        }

        public void CreateReport(Func<IBankProduct, bool> filter)
        {
            Reporter.CreateReport(Products, filter);
            History.Add(new Operation {Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Report"});
        }

        public IBankProduct GetBankProduct(int productId)
        {
            return Products.First(x => x.GetId() == productId);
        }

        public List<IBankProduct> GetProductsByOwner(int ownerId)
        {
            return Products.Where(x => x.GetOwnerId() == ownerId).ToList();
        }

        public List<Operation> GetHistory()
        {
            return History;
        }
    }
}