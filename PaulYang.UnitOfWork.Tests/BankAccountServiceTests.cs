using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaulYang.UnitOfWork.Domain;
using PaulYang.UnitOfWork.Infrastrcture;

namespace PaulYang.UnitOfWork.Tests
{
    [TestClass]
    public class BankAccountServiceTests
    {
        [TestMethod]
        public void TransferMoney_From12_To19_Transfer200_success()
        {
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(x => x.Save(It.IsAny<BankAccount>()));

            var mockIUnitOfWork = new Mock<IUnitOfWork>();
            mockIUnitOfWork.Setup(x => x.RegisterUpdated(It.IsAny<BankAccount>(), It.IsAny<IUnitOfWorkRepository>()));
            mockIUnitOfWork.Setup(x => x.Commit()).Returns(() => 3);

            BankAccountService service = new BankAccountService(mockAccountRepository.Object,mockIUnitOfWork.Object);
            
        }
    }
}
