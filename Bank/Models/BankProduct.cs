using Bank.Enums;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Interfaces
{
    public class BankProduct
    {
        private int _id;

        private int _ownerId;

        protected List<Operation> History;

        protected Bank Bank;

        private IInterest Interest;

        public double Amount { get; set; }

        public BankProduct(Bank bank, int ownerId, IInterest interestSystem)
        {
            Bank = bank;
            _ownerId = ownerId;
            _id = bank.GetProducts().Count != 0 ? bank.GetProducts().Max(x => x.GetId()) + 1 : 1;
            Amount = 0;
            History = new List<Operation>();
            Interest = interestSystem;
        }

        public double GetAccountState()
        {
            return Amount;
        }

        public void ChangeInterestSystem(IInterest interest)
        {
            Interest = interest;
            History.Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestTypeChange, Date = DateTime.Now, Description = interest.GetType().Name });
        }

        public void ChargeInterest()
        {
            var oldAmount = Amount;
            Amount = Interest.ChargeInterest(Amount);
            History.Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description = (Amount - oldAmount).ToString() });
            Bank.GetHistory().Add(new Operation { Type = OperationType.InterestCharge, Date = DateTime.Now, Description = (Amount - oldAmount).ToString() });
        }

        public int GetId()
        {
            return _id;
        }

        public int GetOwnerId()
        {
            return _ownerId;
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}