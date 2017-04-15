using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaulYang.UnitOfWork.Domain;
using PaulYang.UnitOfWork.Infrastrcture;
using PaulYang.UnitOfWork.Persistence;

namespace PaulYang.UnitOfWork.Tests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly BankAccountService _bankAccountService;

        public AccountRepositoryTests()
        {
            _unitOfWork = new Infrastrcture.UnitOfWork();
            _accountRepository = new AccountRepository(_unitOfWork);
            _bankAccountService = new BankAccountService(_accountRepository, _unitOfWork);
        }


        [TestMethod]
        public void Add()
        {
            var accountLeft = new BankAccount {Balance = 200,Id=19};
            var accountRight = new BankAccount {Balance = 10,Id = 99};
            _accountRepository.Add(accountLeft);
            _accountRepository.Add(accountRight);
            _unitOfWork.Commit();
        }

        [TestMethod]
        public void Save()
        {
            var accountLeft = new BankAccount { Balance = 200, Id = 1 };
            var accountRight = new BankAccount { Balance = 10, Id = 2 };


            _bankAccountService.TransferMoney(accountLeft, accountRight, 100);
        }

        [TestMethod]
        public void Remove()
        {
            var accountLeft = new BankAccount { Balance = 200, Id = 1 };
            _accountRepository.Remove(accountLeft);

            _unitOfWork.Commit();
        }
    }
}
