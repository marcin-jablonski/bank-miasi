using Bank.Interfaces;

namespace Bank.Products
{
    public class Credit : IBankProduct
    {
        public void Deposit(double amount)
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw(double amount)
        {
            throw new System.NotImplementedException();
        }

        public void Transfer(double amount, IBankProduct destination)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeInterestSystem(IInterest interest)
        {
            throw new System.NotImplementedException();
        }

        public void ChargeInterest()
        {
            throw new System.NotImplementedException();
        }

        public void CancelDeposit()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCredit()
        {
            throw new System.NotImplementedException();
        }

        public void PayCreditInstallment(double amount)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDebit(IDebit debit)
        {
            throw new System.NotImplementedException();
        }

        public int GetId()
        {
            throw new System.NotImplementedException();
        }

        public int GetOwnerId()
        {
            throw new System.NotImplementedException();
        }
    }
}