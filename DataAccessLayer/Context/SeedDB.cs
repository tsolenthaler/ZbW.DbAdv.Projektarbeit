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
        public List<ArticleGroupDTO> GenerateFirstArticleGroupDTOs()
        {
            var articleGroup = new List<ArticleGroupDTO>
            {
                new ArticleGroupDTO(){ Name = "Kleider", ParentArticleGroupId = 0},
                new ArticleGroupDTO(){ Name = "Schuhe", ParentArticleGroupId = 0},
            };
            return articleGroup;
        }

        public List<ArticleGroupDTO> GenerateSecondArticleGroupDTOs()
        {
            using var context = new SetupDB();
            var articleGroupFirst = context.ArticelGroups.Where(c => c.Name == "Kleider").First();

            var articleGroup = new List<ArticleGroupDTO>
            {
                new ArticleGroupDTO(){ Name = "Hosen", ParentArticleGroupId = articleGroupFirst.Id},
                new ArticleGroupDTO(){ Name = "Oberteile", ParentArticleGroupId = articleGroupFirst.Id},
            };

            var articleGroupFirst2 = context.ArticelGroups.Where(c => c.Name == "Schuhe").First();

            articleGroup.Add(new ArticleGroupDTO() { Name = "Sandalen", ParentArticleGroupId = articleGroupFirst2.Id });
            articleGroup.Add(new ArticleGroupDTO() { Name = "Pantoffeln", ParentArticleGroupId = articleGroupFirst2.Id });
            articleGroup.Add(new ArticleGroupDTO() { Name = "Wanderschuhe", ParentArticleGroupId = articleGroupFirst2.Id });

            return articleGroup;
        }

        public List<ArticleGroupDTO> GenerateThirdArticleGroupDTOs()
        {
            using var context = new SetupDB();
            var articleGroupFirst = context.ArticelGroups.Where(c => c.Name == "Oberteile").First();

            var articleGroup = new List<ArticleGroupDTO>
            {
                new ArticleGroupDTO(){ Name = "T-Shirts", ParentArticleGroupId = articleGroupFirst.Id},
                new ArticleGroupDTO(){ Name = "Pullover", ParentArticleGroupId = articleGroupFirst.Id}
            };

            var articleGroupFirst2 = context.ArticelGroups.Where(c => c.Name == "Hosen").First();

            articleGroup.Add(new ArticleGroupDTO() { Name = "Lange Hosen", ParentArticleGroupId = articleGroupFirst2.Id });
            articleGroup.Add(new ArticleGroupDTO() { Name = "Kurze Hosen", ParentArticleGroupId = articleGroupFirst2.Id });
            
            return articleGroup;
        }

        public List<ArticleDTO> GenerateArticleDTOs()
        {
            using var context = new SetupDB();
            var articleGroup = context.ArticelGroups.Where(c => c.Name == "T-Shirts").First();

            var article = new List<ArticleDTO>()
            {
                new ArticleDTO() { Name = "Sonnen T-Shirt", Price = (decimal)14.50, ArticleGroupId = articleGroup.Id },
                new ArticleDTO() { Name = "Mond T-Shirt", Price = (decimal)19.50, ArticleGroupId = articleGroup.Id },
                new ArticleDTO() { Name = "Sterne T-Shirt", Price = (decimal)9.50, ArticleGroupId = articleGroup.Id }
            };
            return article;
        }

        public List<OrderPositionDTO> GenerateOrderPositionDTOs() {
            using var context = new SetupDB();

            var articles = new DataAccessManager().GetAllArticle();
            var orders = new DataAccessManager().GetAllOrders();

            var orderposition = new List<OrderPositionDTO>
            {
                new OrderPositionDTO() { Quantity = 5, ArticleId = articles[0].Id, OrderId = orders[0].Id},
                new OrderPositionDTO() { Quantity = 2, ArticleId = articles[1].Id, OrderId = orders[0].Id},
                new OrderPositionDTO() { Quantity = 11, ArticleId = articles[2].Id, OrderId = orders[0].Id},

                new OrderPositionDTO() { Quantity = 14, ArticleId = articles[0].Id, OrderId = orders[1].Id},
                new OrderPositionDTO() { Quantity = 3, ArticleId = articles[2].Id, OrderId = orders[1].Id},

                new OrderPositionDTO() { Quantity = 7, ArticleId = articles[0].Id, OrderId = orders[2].Id},
                new OrderPositionDTO() { Quantity = 23, ArticleId = articles[1].Id, OrderId = orders[2].Id},
                new OrderPositionDTO() { Quantity = 2, ArticleId = articles[2].Id, OrderId = orders[2].Id},

                new OrderPositionDTO() { Quantity = 5, ArticleId = articles[1].Id, OrderId = orders[3].Id},
                new OrderPositionDTO() { Quantity = 11, ArticleId = articles[2].Id, OrderId = orders[3].Id},

                new OrderPositionDTO() { Quantity = 12, ArticleId = articles[0].Id, OrderId = orders[4].Id},
                new OrderPositionDTO() { Quantity = 21, ArticleId = articles[1].Id, OrderId = orders[4].Id},
                new OrderPositionDTO() { Quantity = 23, ArticleId = articles[2].Id, OrderId = orders[4].Id},

                new OrderPositionDTO() { Quantity = 4, ArticleId = articles[0].Id, OrderId = orders[5].Id},
                new OrderPositionDTO() { Quantity = 12, ArticleId = articles[1].Id, OrderId = orders[5].Id},
                new OrderPositionDTO() { Quantity = 5, ArticleId = articles[2].Id, OrderId = orders[5].Id},

                new OrderPositionDTO() { Quantity = 9, ArticleId = articles[0].Id, OrderId = orders[6].Id},
                new OrderPositionDTO() { Quantity = 34, ArticleId = articles[1].Id, OrderId = orders[6].Id},
                new OrderPositionDTO() { Quantity = 23, ArticleId = articles[2].Id, OrderId = orders[6].Id},

                new OrderPositionDTO() { Quantity = 11, ArticleId = articles[0].Id, OrderId = orders[7].Id},
                new OrderPositionDTO() { Quantity = 13, ArticleId = articles[1].Id, OrderId = orders[7].Id},
                new OrderPositionDTO() { Quantity = 3, ArticleId = articles[2].Id, OrderId = orders[7].Id},

                new OrderPositionDTO() { Quantity = 7, ArticleId = articles[0].Id, OrderId = orders[8].Id},
                new OrderPositionDTO() { Quantity = 13, ArticleId = articles[1].Id, OrderId = orders[8].Id},
                new OrderPositionDTO() { Quantity = 9, ArticleId = articles[2].Id, OrderId = orders[8].Id},

                new OrderPositionDTO() { Quantity = 23, ArticleId = articles[0].Id, OrderId = orders[9].Id},
                new OrderPositionDTO() { Quantity = 3, ArticleId = articles[2].Id, OrderId = orders[9].Id},

                new OrderPositionDTO() { Quantity = 7, ArticleId = articles[0].Id, OrderId = orders[10].Id},
                new OrderPositionDTO() { Quantity = 22, ArticleId = articles[1].Id, OrderId = orders[10].Id},
                new OrderPositionDTO() { Quantity = 8, ArticleId = articles[2].Id, OrderId = orders[10].Id},

                new OrderPositionDTO() { Quantity = 15, ArticleId = articles[1].Id, OrderId = orders[11].Id},
                new OrderPositionDTO() { Quantity = 24, ArticleId = articles[2].Id, OrderId = orders[11].Id},

                new OrderPositionDTO() { Quantity = 12, ArticleId = articles[0].Id, OrderId = orders[12].Id},
                new OrderPositionDTO() { Quantity = 17, ArticleId = articles[1].Id, OrderId = orders[12].Id},
                new OrderPositionDTO() { Quantity = 8, ArticleId = articles[2].Id, OrderId = orders[12].Id},

                new OrderPositionDTO() { Quantity = 9, ArticleId = articles[0].Id, OrderId = orders[13].Id},
                new OrderPositionDTO() { Quantity = 5, ArticleId = articles[1].Id, OrderId = orders[13].Id},
                new OrderPositionDTO() { Quantity = 13, ArticleId = articles[2].Id, OrderId = orders[13].Id},

                new OrderPositionDTO() { Quantity = 13, ArticleId = articles[0].Id, OrderId = orders[14].Id},
                new OrderPositionDTO() { Quantity = 20, ArticleId = articles[1].Id, OrderId = orders[14].Id},
                new OrderPositionDTO() { Quantity = 9, ArticleId = articles[2].Id, OrderId = orders[14].Id},

                new OrderPositionDTO() { Quantity = 12, ArticleId = articles[0].Id, OrderId = orders[15].Id},
                new OrderPositionDTO() { Quantity = 16, ArticleId = articles[1].Id, OrderId = orders[15].Id},

                new OrderPositionDTO() { Quantity = 11, ArticleId = articles[0].Id, OrderId = orders[16].Id},
                new OrderPositionDTO() { Quantity = 42, ArticleId = articles[1].Id, OrderId = orders[16].Id},
                new OrderPositionDTO() { Quantity = 6, ArticleId = articles[2].Id, OrderId = orders[16].Id},

                new OrderPositionDTO() { Quantity = 16, ArticleId = articles[0].Id, OrderId = orders[16].Id},
                new OrderPositionDTO() { Quantity = 31, ArticleId = articles[1].Id, OrderId = orders[16].Id},
                new OrderPositionDTO() { Quantity = 7, ArticleId = articles[2].Id, OrderId = orders[16].Id},

                new OrderPositionDTO() { Quantity = 23, ArticleId = articles[1].Id, OrderId = orders[17].Id},
                new OrderPositionDTO() { Quantity = 6, ArticleId = articles[2].Id, OrderId = orders[17].Id},

                new OrderPositionDTO() { Quantity = 17, ArticleId = articles[0].Id, OrderId = orders[18].Id},
                new OrderPositionDTO() { Quantity = 13, ArticleId = articles[1].Id, OrderId = orders[18].Id},
                new OrderPositionDTO() { Quantity = 61, ArticleId = articles[2].Id, OrderId = orders[18].Id},
            };
            return orderposition;
        }

        public List<OrderDTO> GenerateOrderDTOs() {
            using var context = new SetupDB();

            var customers = new DataAccessManager().GetAllCustomers();

            var order = new List<OrderDTO>
            {
                /* --------- Customer 1 --------- */
                new OrderDTO() { Date = new DateTime(2019, 4, 25), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2019, 7, 5), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2020, 2, 11), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2020, 8, 21), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2020, 12, 25), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2021, 2, 6), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2021, 6, 12), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2021, 9, 22), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2022, 1, 2), CustomerId = customers[0].Id},
                new OrderDTO() { Date = new DateTime(2022, 3, 14), CustomerId = customers[0].Id},
              
                /* --------- Customer 2 --------- */
                new OrderDTO() { Date = new DateTime(2019, 3, 23), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2019, 6, 2), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2020, 1, 13), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2020, 7, 17), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2021, 2, 25), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2021, 9, 11), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2021, 11, 29), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2022, 2, 17), CustomerId = customers[1].Id},
                new OrderDTO() { Date = new DateTime(2022, 3, 3), CustomerId = customers[1].Id}

            };
            return order;
        }
    }
}
