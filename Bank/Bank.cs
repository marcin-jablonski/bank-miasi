using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Enums;
using Bank.Interfaces;
using Bank.Mechanisms;
using Bank.Mechanisms.Visitors;
using Bank.Models;
using Bank.Products;
using Bank.Exceptions;

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
            BankId = Guid.NewGuid();
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

            Console.WriteLine("Money on accounts: {0}", visitor.AccountStates);
            Console.WriteLine("Total customers debit: {0}", visitor.TotalDebit);
            Console.WriteLine("Total credit taken by customers: {0}", visitor.TotalCredits);
            Console.WriteLine("Total income from installments at this moment: {0}", visitor.TotalInstallmentIncomes);
            Console.WriteLine("Total money in deposits: {0}", visitor.TotalDeposits);

            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Aggregative report" });

        }

        public double CreateCumulativeReport()
        {
            CumulativeVisitor visitor = new CumulativeVisitor();
            foreach (var product in Products)
            {
                product.Accept(visitor);
            }

            Console.WriteLine("Total money in bank: {0}", visitor.Result);

            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Cumulative report" });
            return visitor.Result;
        }

        public void CreateAccountInfoReport()
        {
            AccountsInfoVisitor visitor = new AccountsInfoVisitor();
            foreach (var product in Products)
            {
                product.Accept(visitor);
            }

            Console.WriteLine("All accounts: {0}", visitor.TotalAccounts);
            Console.WriteLine("Accounts with debit: {0}", visitor.DebitAccounts);
            Console.WriteLine("All deposits: {0}", visitor.DepositAccounts);
            Console.WriteLine("All credits: {0}", visitor.CreditAccounts);

            History.Add(new Operation { Type = OperationType.ReportCreation, Date = DateTime.Now, Description = "Third report" });

        }

        public BankProduct GetBankProduct(int productId)
        {
            try
            {
               return Products.First(x => x.GetId() == productId);
            }
            catch (InvalidOperationException e)
            {
                throw new NoSuchBankProductException();
            }
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