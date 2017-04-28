using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Enums;
using Bank.Interfaces;
using Bank.Mechanisms;
using Bank.Mechanisms.Visitors;
using Bank.Models;
using Bank.Products;

namespace Bank
{
    public class Bank
    {
        private readonly Guid BankId;

        private readonly List<Operation> History;

        private readonly List<BankProduct> Products;

        public Bank()
        {
            Products = new List<BankProduct>();
            History = new List<Operation>();
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

        public void CreateAggregativeReport()
        {
            AggregativeVisitor visitor = new AggregativeVisitor();
            foreach (var product in Products)
            {
                product.Accept(visitor);
            }

            //TODO do something with result
            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Aggregative report" });

        }

        public double CreateCumulativeReport()
        {
            CumulativeVisitor visitor = new CumulativeVisitor();
            foreach (var product in Products)
            {
                product.Accept(visitor);
            }

            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Cumulative report" });
            return visitor.Result;
        }

        public void CreateThirdReport()
        {
            ThirdVisitor visitor = new ThirdVisitor();
            foreach (var product in Products)
            {
                product.Accept(visitor);
            }

            //TODO result hadle
            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Third report" });

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