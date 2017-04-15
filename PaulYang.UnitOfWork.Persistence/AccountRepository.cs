using System;
using System.Data;
using System.Data.SqlClient;
using PaulYang.UnitOfWork.Domain;
using PaulYang.UnitOfWork.Infrastrcture;

namespace PaulYang.UnitOfWork.Persistence
{
    public class AccountRepository:IAccountRepository,IUnitOfWorkRepository
    {
        private const string Connectionstring = @"server=.;database=northwind;integrated security=sspi;";
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BankAccount GetAccount(BankAccount account)
        {
            account.Balance = 1000;
            return account;
        }

        public int PersistNewItem(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public int PersistUpdateItem(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public int PersistDeleteItem(IEntityBase entity)
        {
            throw new NotImplementedException();
        }

        public void Save(BankAccount account)
        {
            _unitOfWork.RegisterUpdated(account, this);
        }

        public void Add(BankAccount account)
        {
            _unitOfWork.RegisterAdded(account,this);
        }

        public void Remove(BankAccount account)
        {
            _unitOfWork.RegisterDeleted(account, this);
        }
    }
}