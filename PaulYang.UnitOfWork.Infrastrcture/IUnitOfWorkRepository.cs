namespace PaulYang.UnitOfWork.Infrastrcture
{
    public interface IUnitOfWorkRepository
    {
        int PersistNewItem(IEntityBase entity);
        int PersistUpdateItem(IEntityBase entity);
        int PersistDeleteItem(IEntityBase entity);
    }
}