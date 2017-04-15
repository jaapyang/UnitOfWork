namespace PaulYang.UnitOfWork.Infrastrcture
{
    public interface IUnitOfWork
    {
        void RegisterAdded(IEntityBase entity,IUnitOfWorkRepository repository);
        void RegisterUpdated(IEntityBase entity, IUnitOfWorkRepository repository);
        void RegisterDeleted(IEntityBase entity,IUnitOfWorkRepository repository);
        int Commit();
    }
}