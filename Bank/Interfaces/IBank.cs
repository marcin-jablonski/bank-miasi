using Bank.Enums;

namespace Bank.Interfaces
{
    public interface IBank
    {
        IBankProduct CreateBankProduct(BankProductType productType);
        void CreateReport();
        IBankProduct GetBankProduct(int ownerId);
    }
}