using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace PaulYang.UnitOfWork.Infrastrcture
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<IEntityBase, IUnitOfWorkRepository> _addEntities;
        private readonly Dictionary<IEntityBase, IUnitOfWorkRepository> _changedEntities;
        private readonly Dictionary<IEntityBase, IUnitOfWorkRepository> _deletedEntities;

        public UnitOfWork()
        {
            _addEntities = new Dictionary<IEntityBase, IUnitOfWorkRepository>();
            _changedEntities = new Dictionary<IEntityBase, IUnitOfWorkRepository>();
            _deletedEntities = new Dictionary<IEntityBase, IUnitOfWorkRepository>();
        }

        public void RegisterAdded(IEntityBase entity, IUnitOfWorkRepository repository)
        {
            _addEntities.Add(entity,repository);
        }

        public void RegisterUpdated(IEntityBase entity, IUnitOfWorkRepository repository)
        {
            _changedEntities.Add(entity, repository);
        }

        public void RegisterDeleted(IEntityBase entity, IUnitOfWorkRepository repository)
        {
            _deletedEntities.Add(entity, repository);
        }

        public int Commit()
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                var val = 0;
                try
                {
                    val += _addEntities.Keys.Sum(entity => _addEntities[entity].PersistNewItem(entity));

                    val += _changedEntities.Keys.Sum(entity => _changedEntities[entity].PersistUpdateItem(entity));

                    val += _deletedEntities.Keys.Sum(entity => _deletedEntities[entity].PersistDeleteItem(entity));

                    transactionScope.Complete();
                    return val;
                }
                catch (Exception e)
                {
                    return 0;
                }
                finally
                {
                    transactionScope.Dispose();
                }
            }
        }
    }
}