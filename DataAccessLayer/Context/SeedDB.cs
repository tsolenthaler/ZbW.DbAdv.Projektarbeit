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
            var article = new List<ArticleDTO>()
            {
                new ArticleDTO() { Name = "Sonnen T-Shirt", Price = (decimal)14.50, ArticleGroupId = 2 },
                new ArticleDTO() { Name = "Mond T-Shirt", Price = (decimal)19.50, ArticleGroupId = 2 },
                new ArticleDTO() { Name = "Sterne T-Shirt", Price = (decimal)9.50, ArticleGroupId = 2 }
            };
            return article;
        }

        public List<OrderPositionDTO> GenerateOrderPositionDTOs()
        {
            var orderposition = new List<OrderPositionDTO>
            {
                new OrderPositionDTO() { Quantity = 5, ArticleId = 1, OrderId = 1},
                new OrderPositionDTO() { Quantity = 2, ArticleId = 2, OrderId = 1 },
                new OrderPositionDTO() { Quantity = 11, ArticleId = 3,  OrderId = 1 }
            };
            return orderposition;
        }

        public List<OrderDTO> GenerateOrderDTOs()
        {
            var order = new List<OrderDTO>
            {
                new OrderDTO { Date = new DateTime(2021, 3, 15), CustomerId = 1 },
                new OrderDTO { Date = new DateTime(2022, 1, 15), CustomerId = 2},
                new OrderDTO { Date = new DateTime(2022, 1, 30), CustomerId = 3}
            };
            return order;
        }
    }
}
