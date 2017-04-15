using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using PaulYang.UnitOfWork.Infrastrcture;


namespace PaulYang.UnitOfWork.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void RegisterAdded_InputItems_Success()
        {
            //Arrange
            var fackRepositoty = GetFackUnitOfWorkRepository();

            var uow = new Infrastrcture.UnitOfWork();

            //Action
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterAdded(GetFackEntityBase(), fackRepositoty);
            }

            var addEntities = uow.GetPrivateFiled<Dictionary<IEntityBase, IUnitOfWorkRepository>>("_addEntities");

            //Assert
            Assert.IsNotNull(addEntities);
            Assert.IsTrue(addEntities.Count()== 10);
            CollectionAssert.AllItemsAreInstancesOfType(addEntities.Keys,typeof(IEntityBase));
            CollectionAssert.AllItemsAreInstancesOfType(addEntities.Values,typeof(IUnitOfWorkRepository));
        }

        [TestMethod]
        public void RegisterUpdated_InputItems_Success()
        {
            //Arrange
            var fackRepositoty = GetFackUnitOfWorkRepository();

            var uow = new Infrastrcture.UnitOfWork();

            //Action
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterUpdated(GetFackEntityBase(), fackRepositoty);
            }

            var changedEntities = uow.GetPrivateFiled<Dictionary<IEntityBase, IUnitOfWorkRepository>>("_changedEntities");

            //Assert
            Assert.IsNotNull(changedEntities);
            Assert.IsTrue(changedEntities.Count() == 10);
            CollectionAssert.AllItemsAreInstancesOfType(changedEntities.Keys, typeof(IEntityBase));
            CollectionAssert.AllItemsAreInstancesOfType(changedEntities.Values, typeof(IUnitOfWorkRepository));
        }

        [TestMethod]
        public void RegisterDeleted_InputItems_Success()
        {
            //Arrange
            var fackRepositoty = GetFackUnitOfWorkRepository();

            var uow = new Infrastrcture.UnitOfWork();

            //Action
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterDeleted(GetFackEntityBase(), fackRepositoty);
            }

            var deletedEntities = uow.GetPrivateFiled<Dictionary<IEntityBase, IUnitOfWorkRepository>>("_deletedEntities");

            //Assert
            Assert.IsNotNull(deletedEntities);
            Assert.IsTrue(deletedEntities.Count() == 10);
            CollectionAssert.AllItemsAreInstancesOfType(deletedEntities.Keys, typeof(IEntityBase));
            CollectionAssert.AllItemsAreInstancesOfType(deletedEntities.Values, typeof(IUnitOfWorkRepository));
        }

        [TestMethod]
        public void Commit_JustAdd_Success()
        {
            var fackRepository = GetFackUnitOfWorkRepository(x => x.PersistNewItem(It.IsAny<IEntityBase>()),()=>1);

            var uow = new Infrastrcture.UnitOfWork();
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterAdded(GetFackEntityBase(), fackRepository);
            }
            var actual = uow.Commit();
            Assert.IsTrue(actual == 10);
        }

        [TestMethod]
        public void Commit_JustUpdate_Success()
        {
            var fackRepository = GetFackUnitOfWorkRepository(x => x.PersistUpdateItem(It.IsAny<IEntityBase>()), () => 1);

            var uow = new Infrastrcture.UnitOfWork();
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterUpdated(GetFackEntityBase(), fackRepository);
            }
            var actual = uow.Commit();
            Assert.IsTrue(actual == 10);
        }

        [TestMethod]
        public void Commit_JustDelete_Success()
        {
            var fackRepository = GetFackUnitOfWorkRepository(x => x.PersistDeleteItem(It.IsAny<IEntityBase>()), () => 1);

            var uow = new Infrastrcture.UnitOfWork();
            for (int i = 0; i < 10; i++)
            {
                uow.RegisterDeleted(GetFackEntityBase(), fackRepository);
            }
            var actual = uow.Commit();
            Assert.IsTrue(actual == 10);
        }

        [TestMethod]
        public void Commit_ThrowAnyException_Failed()
        {
            var mockRepository = new Mock<IUnitOfWorkRepository>();
            mockRepository.Setup(x => x.PersistNewItem(It.IsAny<IEntityBase>())).Returns(() => 1);
            // change the exception throw by method Mock.Throws<T>
            mockRepository.Setup(x => x.PersistDeleteItem(It.IsAny<IEntityBase>())).Throws(new Exception());
            mockRepository.Setup(x => x.PersistUpdateItem(It.IsAny<IEntityBase>())).Returns(() => 1);

            var fackRepository = mockRepository.Object;
            
            var uow = new Infrastrcture.UnitOfWork();
            for (int i = 0; i < 2; i++)
            {
                uow.RegisterAdded(GetFackEntityBase(), fackRepository);
                uow.RegisterUpdated(GetFackEntityBase(), fackRepository);
                uow.RegisterDeleted(GetFackEntityBase(), fackRepository);
            }
            var actual = uow.Commit();
            Assert.IsTrue(actual == 0);
        }
        

        private IUnitOfWorkRepository GetFackUnitOfWorkRepository(
            Expression<Func<IUnitOfWorkRepository, int>> setupExpression = null, 
            Func<int> returnExpression = null)
        {
            var mockRepository = new Mock<IUnitOfWorkRepository>();

            ISetup<IUnitOfWorkRepository,int> setup = null;
            if (setupExpression != null)
                setup = mockRepository.Setup(setupExpression);

            if (returnExpression != null && setup != null)
                setup.Returns(returnExpression);

            var fackRepositoty = mockRepository.Object;
            return fackRepositoty;
        }

        private IEntityBase GetFackEntityBase()
        {
            var mockEntities = new Mock<IEntityBase>();
            var fackEntity = mockEntities.Object;
            return fackEntity;
        }
    }
}
