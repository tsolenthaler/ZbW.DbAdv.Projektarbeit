using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace DataAccessLayer.Context
{
    public class SeedDB 
    {
        public AddressDTO[] GenerateAddressDTOs()
        {
            AddressDTO[] addresses = new AddressDTO[4];

            addresses[0] = new AddressDTO()
            {
                Street = "Schibistrasse",
            };

            addresses[1] = new AddressDTO()
            {
                Street = "Bahnhofstrasse",
            };

            addresses[2] = new AddressDTO()
            {
                Street = "Wiesenstrasse",
            };

            addresses[3] = new AddressDTO()
            {
                Street = "Rorschacherstrasse",
            };

            return addresses;
        }
        public CustomerDTO[] GenerateCustomerDTOs()
        {
            var address = new AddressDTO()
            {
                Street = "Rorschacherstrasse",
                StreetNo = "11",
                Plz = "9000",
                City = "St.Gallen"
            };
            CustomerDTO[] customers = new CustomerDTO[4];

            customers[0] = new CustomerDTO()
            {
                Firstname = "Hans",
                Lastname = "Muster",
                AddressId = 1
            };

            customers[1] = new CustomerDTO()
            {
                Firstname = "Kurt",
                Lastname = "Lörrer",
                AddressId = 2
            };

            customers[2] = new CustomerDTO()
            {
                Firstname = "Simone",
                Lastname = "Stadler",
                AddressId = 3,
            };

            customers[3] = new CustomerDTO()
            {
                Firstname = "Peeetraa",
                Lastname = "Sturzenegger",
                AddressId = 4
            };

            return customers;
        }
        public ArticleGroupDTO[] GenerateArticleGroupDTOs()
        {
            ArticleGroupDTO[] articleGroup = new ArticleGroupDTO[3];

            articleGroup[0] = new ArticleGroupDTO()
            {
                Name = "Kleider",
                ParentArticleGroupId = 0
            };
            articleGroup[1] = new ArticleGroupDTO()
            {
                Name = "T-Shirt",
                ParentArticleGroupId = 1
            };
            articleGroup[2] = new ArticleGroupDTO()
            {
                Name = "Hosen",
                ParentArticleGroupId = 1
            };

            return articleGroup;
        }

        public ArticleDTO[] GenerateArticleDTOs()
        {
            ArticleGroupDTO[] articleGroup = new ArticleGroupDTO[3];

            articleGroup[0] = new ArticleGroupDTO()
            {
                Name = "Kleider",
                ParentArticleGroupId = 0
            };
            articleGroup[1] = new ArticleGroupDTO()
            {
                Name = "T-Shirt",
                ParentArticleGroupId = 1
            };
            articleGroup[2] = new ArticleGroupDTO()
            {
                Name = "Hosen",
                ParentArticleGroupId = 1
            };

            ArticleDTO[] article = new ArticleDTO[3];

            article[0] = new ArticleDTO() { Name = "Sonnen T-Shirt", Price = (decimal)14.50, ArticleGroupId = 1 };
            article[1] = new ArticleDTO() { Name = "Mond T-Shirt", Price = (decimal)19.50, ArticleGroupId = 1 };
            article[2] = new ArticleDTO() { Name = "Sterne T-Shirt", Price = (decimal)9.50, ArticleGroupId = 1 };

            return article;
        }

        public OrderPositionDTO[] GenerateOrderPositionDTOs()
        {
            OrderPositionDTO[] orderposition = new OrderPositionDTO[3];

            orderposition[0] = new OrderPositionDTO() { Quantity = 5 };
            orderposition[1] = new OrderPositionDTO() { Quantity = 5 };
            orderposition[2] = new OrderPositionDTO() { Quantity = 5 };

            return orderposition;
        }

        public OrderDTO[] GenerateOrderDTOs()
        {
            OrderDTO[] order = new OrderDTO[3];

            order[0] = new OrderDTO { Date = new DateTime(2021, 3, 15), };
            order[1] = new OrderDTO { Date = new DateTime(2022, 1, 15), };
            order[2] = new OrderDTO { Date = new DateTime(2022, 1, 30), };

            OrderPositionDTO[] orderposition = new OrderPositionDTO[3];

            orderposition[0] = new OrderPositionDTO() { Quantity = 5, Order = order[0] };
            orderposition[1] = new OrderPositionDTO() { Quantity = 5, Order = order[1] };
            orderposition[2] = new OrderPositionDTO() { Quantity = 5, Order = order[2] };

            return order;
        }
    }
}
