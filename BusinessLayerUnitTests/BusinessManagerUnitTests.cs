using Autofac;
using BusinessLayer;
using BusinessLayer.Models;
using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using DataAccessLayer.Customer;
using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Export;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;
using System.Collections.ObjectModel;

namespace BusinessLayerUnitTests
{
    public class BusinessManagerUnitTests
    {
        //MethodUnderTest_Scenario_ExpectedResult
        [Fact]
        public void IsValidCustomer_CallWithValidCustomer_ReturnsNull()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer = Helper_GetValidCustomer();            

            //Act
            var result = businessManager.IsValidCustomer(customer);

            //Assert
            Assert.Null(result);
        }

        private BusinessManager Helper_GetBusinessManagerWithFakeRepos()
        {
            var fakeCustomerRepo = A.Fake<ICustomerRepository>();
            var fakeArticleRepo = A.Fake<IArticleRepository>();
            var fakeArticleGroupRepo = A.Fake<IArticleGroupRepository>();
            var fakeExportClientRepo = A.Fake<IExportClientRepository>();
            var fakeInvoiceRepo = A.Fake<IInvoiceRepository>();
            var fakeOrderRepo = A.Fake<IOrderRepository>();
            var fakeOrderPosRepo = A.Fake<IOrderPositionRepository>();

            return new BusinessManager(fakeCustomerRepo, fakeExportClientRepo, fakeArticleRepo, fakeArticleGroupRepo,
                fakeOrderRepo, fakeOrderPosRepo, fakeInvoiceRepo);
        }

        [Fact]
        public void ModifySelected_ProperIndex_SetsToReadOnly()
        {
            //Arrange
            var businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Customer>();
            itemList.Add(new Customer()
            {
                ReadOnly = true,
            });

            //Act
            businessManager.ModifySelected(itemList, 0);

            //Assert
            Assert.False(itemList[0].ReadOnly);
        }

        [Fact]
        public void GetIndexOfModifiableDataGridChild_OneIsModifiable_ReturnsIndex()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Customer>();
            itemList.Add(new Customer()
            {
                ReadOnly = true,
            });
            itemList.Add(new Customer()
            {
                ReadOnly = false,
            });

            //Act
            var result = businessManager.GetIndexOfModifiableDataGridChild(itemList);

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void CancelModificationCustomer_IdZero_RemovesFromItemList()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Customer>();
            itemList.Add(new Customer()
            {
                ReadOnly = false,
                Id = 0
            });

            //Act
            businessManager.CancelModificationCustomer(itemList);

            //Assert
            Assert.Empty(itemList);
        }

        [Fact]
        public void CancelModificationCustomer_IdNotZero_LoadsFromDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Customer>();
            itemList.Add(new Customer()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.CancelModificationCustomer(itemList);

            //Assert
            A.CallTo(() => businessManager.CustomerRepository.GetSingle(1)).MustHaveHappened();
        }

        [Fact]
        public void CancelModificationArticleGroup_IdZero_RemovesFromItemList()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<ArticleGroup>();
            itemList.Add(new ArticleGroup()
            {
                ReadOnly = false,
                Id = 0
            });

            //Act
            businessManager.CancelModificationArticleGroup(itemList);

            //Assert
            Assert.Empty(itemList);
        }

        [Fact]
        public void CancelModificationArticleGroup_IdNotZero_LoadsFromDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<ArticleGroup>();
            itemList.Add(new ArticleGroup()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.CancelModificationArticleGroup(itemList);

            //Assert
            A.CallTo(() => businessManager.ArticleGroupRepository.GetSingle(1)).MustHaveHappened();
        }

        [Fact]
        public void CancelModificationArticle_IdZero_RemovesFromItemList()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Article>();
            itemList.Add(new Article()
            {
                ReadOnly = false,
                Id = 0
            });

            //Act
            businessManager.CancelModificationArticle(itemList);

            //Assert
            Assert.Empty(itemList);
        }

        [Fact]
        public void CancelModificationArticle_IdNotZero_LoadsFromDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Article>();
            itemList.Add(new Article()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.CancelModificationArticle(itemList);

            //Assert
            A.CallTo(() => businessManager.ArticleRepository.GetSingle(1)).MustHaveHappened();
        }

        [Fact]
        public void CancelModificationOrder_IdZero_RemovesFromItemList()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Order>();
            itemList.Add(new Order()
            {
                ReadOnly = false,
                Id = 0
            });

            //Act
            businessManager.CancelModificationOrder(itemList);

            //Assert
            Assert.Empty(itemList);
        }

        [Fact]
        public void CancelModificationOrder_IdNotZero_LoadsFromDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<Order>();
            itemList.Add(new Order()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.CancelModificationOrder(itemList);

            //Assert
            A.CallTo(() => businessManager.OrderRepository.GetSingle(1)).MustHaveHappened();
        }

        [Fact]
        public void CancelModificationOrderPosition_IdZero_RemovesFromItemList()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<OrderPosition>();
            itemList.Add(new OrderPosition()
            {
                ReadOnly = false,
                Id = 0
            });

            //Act
            businessManager.CancelModificationOrderPosition(itemList);

            //Assert
            Assert.Empty(itemList);
        }

        [Fact]
        public void CancelModificationOrderPosition_IdNotZero_LoadsFromDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            var itemList = new ObservableCollection<OrderPosition>();
            itemList.Add(new OrderPosition()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.CancelModificationOrderPosition(itemList);

            //Assert
            A.CallTo(() => businessManager.OrderPositionRepository.GetSingle(1)).MustHaveHappened();
        }

        [Fact]
        public void LoadAllOrdersFromDb_ClearsListAndCallsDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            businessManager.Orders = new ObservableCollection<Order>();
            businessManager.Orders.Add(new Order()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.LoadAllOrdersFromDb();

            //Assert
            Assert.Empty(businessManager.Orders);
            A.CallTo(() => businessManager.OrderRepository.GetAll()).MustHaveHappened();
        }

        [Fact]
        public void LoadAllCustomersFromDb_ClearsListAndCallsDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            businessManager.Customers = new ObservableCollection<Customer>();
            businessManager.Customers.Add(new Customer()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.LoadAllCustomersFromDb();

            //Assert
            Assert.Empty(businessManager.Customers);
            A.CallTo(() => businessManager.CustomerRepository.GetAll()).MustHaveHappened();
        }

        [Fact]
        public void LoadAllArticlesFromDb_ClearsListAndCallsDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            businessManager.Articles = new ObservableCollection<Article>();
            businessManager.Articles.Add(new Article()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.LoadAllArticlesFromDb();

            //Assert
            Assert.Empty(businessManager.Articles);
            A.CallTo(() => businessManager.ArticleRepository.GetAll()).MustHaveHappened();
        }

        [Fact]
        public void LoadAllArticleGroupsFromDb_ClearsListAndCallsDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            businessManager.ArticleGroups = new ObservableCollection<ArticleGroup>();
            businessManager.ArticleGroups.Add(new ArticleGroup()
            {
                ReadOnly = false,
                Id = 1
            });

            //Act
            businessManager.LoadAllArticleGroupsFromDb();

            //Assert
            Assert.Empty(businessManager.ArticleGroups);
            A.CallTo(() => businessManager.ArticleGroupRepository.GetAll()).MustHaveHappened();
        }

        [Fact]
        public void LoadAllOrderPositionFromDb_ClearsListAndCallsDb()
        {
            //Arrange
            BusinessManager businessManager = Helper_GetBusinessManagerWithFakeRepos();
            businessManager.OrderPositions = new ObservableCollection<OrderPosition>();
            businessManager.OrderPositions.Add(new OrderPosition()
            {
                ReadOnly = false,
                Id = 1,
                OrderId = 1
            });

            //Act
            businessManager.LoadOrderPositionsForSpecificOrder(1);

            //Assert
            Assert.Empty(businessManager.OrderPositions);
            A.CallTo(() => businessManager.OrderPositionRepository.GetAllByOrderId(1)).MustHaveHappened();
        }

        [Fact]
        public void IsValidCustomer_CallWithWrongCU_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer1 = Helper_GetValidCustomer();
            customer1.Clientnr = "CX123";

            //Act
            var result1 = businessManager.IsValidCustomer(customer1);

            //Assert
            Assert.Equal("Clientnr ist nicht gültig!", result1);
        }

        [Fact]
        public void IsValidCustomer_CallWithWrongWebsite_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer1 = Helper_GetValidCustomer();
            customer1.Website = "xyz";

            //Act
            var result1 = businessManager.IsValidCustomer(customer1);

            //Assert
            Assert.Equal("\r\n Webseite ist nicht gültig!", result1);
        }

        [Fact]
        public void IsValidCustomer_CallWithWrongPassword_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer = Helper_GetValidCustomer();
            customer.Password = "xyz";

            //Act
            var result = businessManager.IsValidCustomer(customer);

            //Assert
            Assert.Equal("\r\n Passwort ist nicht gültig!", result);
        }

        [Fact]
        public void IsValidEmail_CallWithInvalidEmail_ReturnsFalse()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var invalidEmail1 = "ab@dc";
            var invalidEmail2 = "hansawef";
            var invalidEmail3 = "g@hotmail,co";

            //Act
            var result1 = businessManager.IsValidEmail(invalidEmail1);
            var result2 = businessManager.IsValidEmail(invalidEmail2);
            var result3 = businessManager.IsValidEmail(invalidEmail3);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        [Fact]
        public void IsValidEmail_CallWithValidEmail_ReturnsTrue()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var validEmail1 = "abc@dce.com";
            var validEmail2 = "abc.def@ghi.ch";
            var validEmail3 = "abc_def@ghi.it";

            //Act
            var result1 = businessManager.IsValidEmail(validEmail1);
            var result2 = businessManager.IsValidEmail(validEmail2);
            var result3 = businessManager.IsValidEmail(validEmail3);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        [Fact]
        public void IsValidWebsite_CallWithInvalidWebsite_ReturnsFalse()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var invalidWebsite1 = "awfwef";
            var invalidWebsite2 = "!google.com";
            var invalidWebsite3 = "/google/com";

            //Act
            var result1 = businessManager.IsValidWebsite(invalidWebsite1);
            var result2 = businessManager.IsValidWebsite(invalidWebsite2);
            var result3 = businessManager.IsValidWebsite(invalidWebsite3);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        [Fact]
        public void IsValidWebsite_CallWithValidWebsite_ReturnsTrue()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var validWebsite1 = "www.google.com";
            var validWebsite2 = "google.com";
            var validWebsite3 = "https://www.google.com";

            //Act
            var result1 = businessManager.IsValidWebsite(validWebsite1);
            var result2 = businessManager.IsValidWebsite(validWebsite2);
            var result3 = businessManager.IsValidWebsite(validWebsite3);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        [Fact]
        public void IsValidPassword_CallWithInvalidPassword_ReturnsFalse()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var invalidPassword1 = "awfwef";
            var invalidPassword2 = "Pawefwe";
            var invalidPassword3 = "awefw123";

            //Act
            var result1 = businessManager.IsValidPassword(invalidPassword1);
            var result2 = businessManager.IsValidPassword(invalidPassword2);
            var result3 = businessManager.IsValidPassword(invalidPassword3);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        [Fact]
        public void IsValidPassword_CallWithValidPassword_ReturnsTrue()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var validPassword1 = "Wefw123awef!";
            var validPassword2 = "Pawefwe1231$";
            var validPassword3 = "aWefw123wef!";

            //Act
            var result1 = businessManager.IsValidPassword(validPassword1);
            var result2 = businessManager.IsValidPassword(validPassword2);
            var result3 = businessManager.IsValidPassword(validPassword3);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }



        [Fact]
        public void IsValidCustomerNr_CallWithInvalidCustomerNr_ReturnsFalse()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var invalidCustomerNr1 = "CX12345";
            var invalidCustomerNr2 = "CX1234";
            var invalidCustomerNr3 = "C123456";

            //Act
            var result1 = businessManager.IsValidCustomerNr(invalidCustomerNr1);
            var result2 = businessManager.IsValidCustomerNr(invalidCustomerNr2);
            var result3 = businessManager.IsValidCustomerNr(invalidCustomerNr3);

            //Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        [Fact]
        public void IsValidCustomerNr_CallWithValidCustomerNr_ReturnsTrue()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var validCustomerNr1 = "CU12345";
            var validCustomerNr2 = "CU00001";
            var validCustomerNr3 = "CU99999";

            //Act
            var result1 = businessManager.IsValidCustomerNr(validCustomerNr1);
            var result2 = businessManager.IsValidCustomerNr(validCustomerNr2);
            var result3 = businessManager.IsValidCustomerNr(validCustomerNr3);

            //Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        private Customer Helper_GetValidCustomer()
        {
            return new Customer()
            {
                Clientnr = "CU12345",
                Website = "www.google.com",
                EMail = "hans.muster@gmail.com",
                Password = "Aa123456!"
            };
        }

        [Fact]
        public void IsValidImport_CallWithValidCustomer_ReturnsNull()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var client = Helper_GetValidClient();

            //Act
            var result = businessManager.IsValidImport(client);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void IsValidImport_CallWithWrongCU_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer1 = Helper_GetValidClient();
            customer1.customerNr = "CX123";

            //Act
            var result1 = businessManager.IsValidImport(customer1);

            //Assert
            Assert.Equal("Clientnr ist nicht gültig!", result1);
        }

        private Client Helper_GetValidClient()
        {
            return new Client()
            {
                customerNr = "CU12345",
                website = "www.google.com",
                email = "hans.muster@gmail.com",
                password = "Aa123456!"
            };
        }
    }
}
