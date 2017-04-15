namespace PaulYang.UnitOfWork.Domain
{
    public interface IAccountRepository
    {
        void Save(BankAccount account);
        void Add(BankAccount account);
        void Remove(BankAccount account);
    }
}