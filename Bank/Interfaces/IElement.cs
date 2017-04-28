namespace Bank.Interfaces
{
    public interface IElement
    {
        void Accept(IVisitor visitor);
    }
}