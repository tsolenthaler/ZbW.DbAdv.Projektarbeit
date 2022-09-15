using Autofac;
using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

        [Fact]
        public void IsValidCustomer_CallWithWrongCU_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer1 = Helper_GetValidCustomer();
            customer1.Clientnr = "CX123";

            var customer2 = Helper_GetValidCustomer();
            customer2.Clientnr = "CU123456";

            var customer3 = Helper_GetValidCustomer();
            customer3.Clientnr = "CA12345";

            var customer4 = Helper_GetValidCustomer();
            customer4.Clientnr = "12345CA";

            //Act
            var result1 = businessManager.IsValidCustomer(customer1);
            var result2 = businessManager.IsValidCustomer(customer2);
            var result3 = businessManager.IsValidCustomer(customer3);
            var result4 = businessManager.IsValidCustomer(customer4);

            //Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
            Assert.NotNull(result4);
        }

        [Fact]
        public void IsValidCustomer_CallWithWrongWebsite_ReturnsString()
        {
            //Arrange
            var container = new IoCSetup().SetupContainer();
            var businessManager = container.Resolve<BusinessManager>();

            var customer1 = Helper_GetValidCustomer();
            customer1.Website = "xyz";

            var customer2 = Helper_GetValidCustomer();
            customer2.Website = "www.google";

            var customer3 = Helper_GetValidCustomer();
            customer3.Website = "htp://www.google.com";

            //Act
            var result1 = businessManager.IsValidCustomer(customer1);
            var result2 = businessManager.IsValidCustomer(customer2);
            var result3 = businessManager.IsValidCustomer(customer3);

            //Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
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
            Assert.NotNull(result);
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
    }
}
