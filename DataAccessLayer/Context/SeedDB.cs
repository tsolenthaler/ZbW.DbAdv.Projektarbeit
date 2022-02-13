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
        public List<CustomerDTO> GenerateCustomerDTOs()
        {
            var customers = new List<CustomerDTO>{
                new CustomerDTO
                {
                    Firstname = "Hans",
                    Lastname = "Muster",
                    AddressId = 1,
                    Address = new AddressDTO
                    {
                        Street = "Rorschacherstrasse",
                        StreetNo = "11",
                        Plz = "9000",
                        City = "St.Gallen"
                    }
                },
                new CustomerDTO{
                    Firstname = "Kurt",
                    Lastname = "Lörrer",
                    Address = new AddressDTO
                    {
                        Street = "Bahnhofstrasse",
                        StreetNo = "5",
                        Plz = "8000",
                        City = "Zürich"
                    }
                },
                new CustomerDTO{
                    Firstname = "Simone",
                    Lastname = "Stadler",
                    Address = new AddressDTO
                    {
                        Street = "Wiesenstrasse",
                        StreetNo = "21",
                        Plz = "3000",
                        City = "Bern"
                    }
                },
                new CustomerDTO{
                    Firstname = "Peeetraa",
                    Lastname = "Sturzenegger",
                    Address = new AddressDTO
                    {
                        Street = "Hauptstrasse",
                        StreetNo = "1",
                        Plz = "9500",
                        City = "Wil"
                    }
                },
            };
            return customers;
        }
        public List<ArticleGroupDTO> GenerateArticleGroupDTOs()
        {
            var articleGroup = new List<ArticleGroupDTO>
            {
                new ArticleGroupDTO(){ Name = "Kleider", ParentArticleGroupId = 0},
                new ArticleGroupDTO(){ Name = "T-Shirt", ParentArticleGroupId = 1},
                new ArticleGroupDTO(){ Name = "Hosen", ParentArticleGroupId = 1}
            };
            return articleGroup;
        }

        public List<ArticleDTO> GenerateArticleDTOs()
        {
            using var context = new SetupDB();
            var articleGroup = context.ArticelGroups.Where(c => c.Name == "T-Shirt").First();

            var article = new List<ArticleDTO>()
            {
                new ArticleDTO() { Name = "Sonnen T-Shirt", Price = (decimal)14.50, ArticleGroupId = articleGroup.Id },
                new ArticleDTO() { Name = "Mond T-Shirt", Price = (decimal)19.50, ArticleGroupId = articleGroup.Id },
                new ArticleDTO() { Name = "Sterne T-Shirt", Price = (decimal)9.50, ArticleGroupId = articleGroup.Id }
            };
            return article;
        }

        public List<OrderPositionDTO> GenerateOrderPositionDTOs()
        {
            using var context = new SetupDB();
            var articleFirst = context.Articles.First();
            //var articleLast = context.Articles.Last();
            var order = context.Orders.First();
            var orderposition = new List<OrderPositionDTO>
            {
                new OrderPositionDTO() { Quantity = 5, ArticleId = articleFirst.Id, OrderId = order.Id},
                new OrderPositionDTO() { Quantity = 2, ArticleId = articleFirst.Id, OrderId = order.Id },
                new OrderPositionDTO() { Quantity = 11, ArticleId = articleFirst.Id,  OrderId = order.Id }
            };
            return orderposition;
        }

        public List<OrderDTO> GenerateOrderDTOs()
        {
            using var context = new SetupDB();
            var customerFirst = context.Customers.First();
            //var customerLast = context.Customers.Last();
            var order = new List<OrderDTO>
            {
                new OrderDTO { Date = new DateTime(2021, 3, 15), CustomerId = customerFirst.Id },
                new OrderDTO { Date = new DateTime(2022, 1, 15), CustomerId = customerFirst.Id},
                new OrderDTO { Date = new DateTime(2022, 1, 30), CustomerId = customerFirst.Id}
            };
            return order;
        }
    }
}
