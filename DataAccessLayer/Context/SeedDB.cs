using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Customer;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;
using DataAccessLayer.Invoice;

namespace DataAccessLayer.Context
{
    public class SeedDB 
    {
        public List<CustomerDTO> GenerateCustomerDTOs()
        {
            var customers = new List<CustomerDTO>{
                new CustomerDTO
                {
                    Clientnr = "CU10000",
                    Firstname = "Hans",
                    Lastname = "Muster",
                    Company = "ABC AG",
                    AddressId = 1,
                    Address = new AddressDTO
                    {
                        Street = "Rorschacherstrasse",
                        StreetNo = "11",
                        Plz = "9000",
                        City = "St.Gallen",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10001",
                    Firstname = "Kurt",
                    Lastname = "Lörrer",
                    Company = "Lörrer GmbH",
                    Address = new AddressDTO
                    {
                        Street = "Bahnhofstrasse",
                        StreetNo = "5",
                        Plz = "8000",
                        City = "Zürich",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10002",
                    Firstname = "Simone",
                    Lastname = "Stadler",
                    Company = "Stadler AG",
                    Address = new AddressDTO
                    {
                        Street = "Wiesenstrasse",
                        StreetNo = "21",
                        Plz = "3000",
                        City = "Bern",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10003",
                    Firstname = "Peeetraa",
                    Lastname = "Sturzenegger",
                    Company = "Sturzenegger & Co. AG",
                    Address = new AddressDTO
                    {
                        Street = "Hauptstrasse",
                        StreetNo = "1",
                        Plz = "9500",
                        City = "Wil",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10004",
                    Firstname = "Hansruedi",
                    Lastname = "Arpa",
                    Company = "Isernet AG",
                    Address = new AddressDTO
                    {
                        Street = "Aprastrasse",
                        StreetNo = "15",
                        Plz = "9000",
                        City = "St.Gallen",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10005",
                    Firstname = "Stan",
                    Lastname = "Rich",
                    Company = "Rich AG",
                    Address = new AddressDTO
                    {
                        Street = "Richterstrasse",
                        StreetNo = "258",
                        Plz = "8000",
                        City = "Zürich",
                        Countryname = AddressDTO.Country.Schweiz
                    }
                },
                new CustomerDTO{
                    Clientnr = "CU10006",
                    Firstname = "Jack",
                    Lastname = "Nest",
                    Company = "Nest AG",
                    Address = new AddressDTO
                    {
                        Street = "Neststrasse",
                        StreetNo = "43",
                        Plz = "8000",
                        City = "Zürich",
                        Countryname = AddressDTO.Country.Schweiz
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

        public void SeedArticleDTOsWithDatumInThePast()
        {
            //define context, open connection
            using var context = new SetupDB();
            context.Database.OpenConnection();

            //GO is specific for MS SQL -> therefore need to split commands in different batches
            var text = SqlRawCommands.SeedArticle;
            var parts = text.Split(new string[] { "GO" }, System.StringSplitOptions.None);
            foreach (var part in parts)
            {
                context.Database.ExecuteSqlRaw(part);
            }

            //close connection
            context.Database.CloseConnection();
        }

        public List<OrderPositionDTO> GenerateOrderPositionDTOs()
        {
            using var context = new SetupDB();

            var articles = new ArticleRepository().GetAll();
            var orders = new OrderRepository().GetAll();

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

            var customers = new CustomerRepository().GetAll();

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

        public List<InvoiceDTO> GenerateInvoiceDTOs()
        {
            using var context = new SetupDB();
            var customerFirst = context.Customers.First();
            var customerArpanet = context.Customers.Where(c => c.Company == "Isernet AG").First();
            var invoice = new List<InvoiceDTO>
            {
                new InvoiceDTO { Date = DateTime.Now.AddMonths(-1), CustomerId = customerFirst.Id, Netto = 999.00, Brutto = 1078.92 },
                new InvoiceDTO { Date = DateTime.Now.AddMonths(-1), CustomerId = customerFirst.Id, Netto = 1480.50, Brutto = 1598.94},
                new InvoiceDTO { Date = DateTime.Now.AddMonths(-1), CustomerId = customerFirst.Id, Netto = 200.10, Brutto = 216.10},
                new InvoiceDTO { Date = DateTime.Now.AddMonths(-1), CustomerId = customerArpanet.Id, Netto = 343.40, Brutto = 370.90},
                new InvoiceDTO { Date = DateTime.Now.AddMonths(-1), CustomerId = customerArpanet.Id, Netto = 560.30, Brutto = 605.10}
            };

            return invoice;
        }

        public List<InvoiceDTO> ChangeCustomerDTOs()
        {
            using var context = new SetupDB();
            var customer = context.Customers.Where(c => c.Company == "Isernet AG").First();
            var customerRemove = context.Customers.Where(c => c.Company == "Nest AG").First();

            var address = context.Addresses.Where(c => c.Street == "Aprastrasse").First();

            address.Street = "Zürcherstrasse";
            address.StreetNo = "908";
            address.City = "Zürich";
            address.Plz = "8000";
            customer.Company = "Arpanet AG";
            context.Remove(customerRemove);
            context.SaveChanges();

            //var customerIsernet = context.Customers.Where(c => c.Company == "Isernet AG").First();
            var invoice = new List<InvoiceDTO>
            {
                new InvoiceDTO { Date = DateTime.Now.AddDays(-2), CustomerId = customer.Id, Netto = 1205.00, Brutto = 1301.40},
                new InvoiceDTO { Date = DateTime.Now.AddDays(-10), CustomerId = customer.Id, Netto = 3205.00, Brutto = 2301.40},
                new InvoiceDTO { Date = DateTime.Now, CustomerId = customer.Id, Netto = 340.00, Brutto = 367.20},
                new InvoiceDTO { Date = DateTime.Now.AddDays(2), CustomerId = customer.Id, Netto = 102.20, Brutto = 104.20}
            };

            return invoice;
        }

        public void SeedCustomerHistory()
        {
            //define context, open connection
            using var context = new SetupDB();
            context.Database.OpenConnection();

            //GO is specific for MS SQL -> therefore need to split commands in different batches
            var text = SqlRawCommands.SeedCustomer;
            var parts = text.Split(new string[] { "GO" }, System.StringSplitOptions.None);
            foreach (var part in parts)
            {
                context.Database.ExecuteSqlRaw(part);
            }

            //close connection
            context.Database.CloseConnection();
        }

        public void SeedAddressesHistory()
        {
            //define context, open connection
            using var context = new SetupDB();
            context.Database.OpenConnection();

            //GO is specific for MS SQL -> therefore need to split commands in different batches
            var text = SqlRawCommands.SeedAddresses;
            var parts = text.Split(new string[] { "GO" }, System.StringSplitOptions.None);
            foreach (var part in parts)
            {
                context.Database.ExecuteSqlRaw(part);
            }

            //close connection
            context.Database.CloseConnection();
        }
    }
}
