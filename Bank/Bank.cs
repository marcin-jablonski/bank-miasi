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
    public class Bank
    {
        private readonly Guid BankId;

        private readonly List<Operation> History;

        private readonly List<BankProduct> Products;

        private readonly IReporter Reporter;

        public Bank()
        {
            Products = new List<BankProduct>();
            History = new List<Operation>();
            Reporter = new Reporter();
            BankId = new Guid();
        }

        public BankProduct CreateBankProduct(BankProduct newProduct)
        {
            Products.Add(newProduct);
            if (newProduct.GetType() == typeof(Deposit))
                History.Add(new Operation
                {
                    Date = DateTime.Now,
                    Type = OperationType.DepositCreation,
                    Description =
                        "Deposit of " + newProduct.GetAccountState() + " created for owner " + newProduct.GetOwnerId()
                });
            else if (newProduct.GetType() == typeof(Credit))
                History.Add(new Operation
                {
                    Date = DateTime.Now,
                    Type = OperationType.CreditCreation,
                    Description = "Credit created for owner " + newProduct.GetOwnerId()
                });
            return newProduct;
        }

        public void CreateReport(Func<BankProduct, bool> filter)
        {
            Reporter.CreateReport(Products, filter);
            History.Add(new Operation {Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Report"});
        }

        public BankProduct GetBankProduct(int productId)
        {
            return Products.First(x => x.GetId() == productId);
        }

        public List<BankProduct> GetProductsByOwner(int ownerId)
        {
            return Products.Where(x => x.GetOwnerId() == ownerId).ToList();
        }

        public List<Operation> GetHistory()
        {
            return History;
        }

        public List<BankProduct> GetProducts()
        {
            return Products;
        }

        public Guid GetBankId()
        {
            return BankId;
        }
    }
}