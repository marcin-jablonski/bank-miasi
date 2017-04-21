namespace Bank.Mechanisms.Kir
{
    public interface IInterbankTransferOperation
    {
        void Transfer(InterbankTransfer transfer);
    }
}