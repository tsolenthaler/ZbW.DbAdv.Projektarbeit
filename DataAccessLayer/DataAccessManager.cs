using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DataAccessLayer.Models
{
    public class DataAccessManager
    {

        public void MigrateDatabase()
        {
            using var context = new SetupDB();

            context.Database.Migrate();

            // ADD INITIAL DATA 
            SeedingDatabase();
        }

        /// <summary>
        ///  INITAL DATA / Seed Data / Testdata to Database
        /// </summary>
        public void SeedingDatabase()
        {
            using var context = new SetupDB();
            var seedDb = new SeedDB();

            if (!context.Customers.Any())
            {
                context.AddRange(seedDb.GenerateCustomerDTOs());
                context.SaveChanges();
            }
            if (!context.ArticelGroups.Any())
            {
                context.AddRange(seedDb.GenerateFirstArticleGroupDTOs());
                context.SaveChanges();
                context.AddRange(seedDb.GenerateSecondArticleGroupDTOs());
                context.SaveChanges();
                context.AddRange(seedDb.GenerateThirdArticleGroupDTOs());
                context.SaveChanges();
            }
            if (!context.Articles.Any())
            {
                context.AddRange(seedDb.GenerateArticleDTOs());
                context.SaveChanges();
            }
            if (!context.Orders.Any())
            {
                context.AddRange(seedDb.GenerateOrderDTOs());
                context.SaveChanges();
            }
            if (!context.OrderPositions.Any())
            {
                context.AddRange(seedDb.GenerateOrderPositionDTOs());
                context.SaveChanges();
            }
        }

        /// <summary>
        ///  READ all Customers
        /// </summary>
        public CustomerDTO[] GetAllCustomers()
        {
            using var context = new SetupDB();
            var customers = context.Customers.Include(c => c.Address).ToArray();
            return customers;
        }

        /// <summary>
        ///  READ Customer by ID
        /// </summary>
        public CustomerDTO GetCustomerById(int id)
        {
            using var context = new SetupDB();
            //var customer = context.Customers.Find(id);
            var customer = context.Customers
                .Include(c => c.Address)
                .Where(c => c.Id == id).ToArray();

            return customer[0];
        }

        /// <summary>
        ///  UPDATE Customer without Address!! --> need Testing
        /// </summary>
        public void UpdateCustomer(CustomerDTO customerDto)
        {
            using var context = new SetupDB();
            var customer = context.Customers.Find(customerDto.Id);

            //customer = customerDto funktioniert nicht -> man muss Werte einzeln hinzufügen
            if(customer != null)
            {
                customer.Firstname = customerDto.Firstname;
                customer.Lastname = customerDto.Lastname;
                customer.Website = customerDto.Website;
                customer.Address = customerDto.Address;
                customer.EMail = customerDto.EMail;
                customer.Password = customerDto.Password;
                customer.AddressId = customerDto.AddressId;
            }         
            context.SaveChanges();
        }

        /// <summary>
        ///  CREATE Customer without Address!!
        /// </summary>
        public void NewCustomer(CustomerDTO customerDto)
        {
            using var context = new SetupDB();
            context.Customers.Add(customerDto);
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Customer with Address
        /// </summary>
        public void DeleteCustomerById(int id)
        {
            using var context = new SetupDB();
            var customer = context.Customers
                .Include(a => a.Address)
                .Single(a => a.Id == id);
            
            context.Addresses.RemoveRange(customer.Address); // Löscht auch die Adresse
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        /// <summary>
        ///  CREATE new Address
        /// </summary>
        public void NewAddress(AddressDTO addressDto)
        {
            using var context = new SetupDB();
            context.Addresses.Add(addressDto);
            context.SaveChanges();
        }

        /// <summary>
        ///  READ Address by ID
        /// </summary>
        public AddressDTO GetAddressById(int id)
        {
            using var context = new SetupDB();
            var address = context.Addresses.Find(id);
            return address;
        }

        /// <summary>
        ///  UPDATE Address - Testing!
        /// </summary>
        public void UpdateAddress(AddressDTO addressDto)
        {
            using var context = new SetupDB();
            var address = context.Addresses.Find(addressDto.Id);
            if (address != null)
            {
                address.Street = addressDto.Street;
                address.StreetNo = addressDto.StreetNo;
                address.Plz = addressDto.Plz;
                address.City = addressDto.City;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Address by ID
        /// </summary>
        public void DeleteAddressByID(int id)
        {
            using var context = new SetupDB();
            var address = context.Addresses.Find(id);
            context.Addresses.Remove(address);
            context.SaveChanges();
        }

        /// <summary>
        ///  READ - Get all Article Group
        /// </summary>
        public ArticleGroupDTO[] GetAllArticleGroup()
        {
            using var context = new SetupDB();
            return context.ArticelGroups.ToArray();
        }

        /// <summary>
        ///  READ - Get Article Group by ID
        /// </summary>
        public ArticleGroupDTO GetArticleGroupById(int id)
        {
            using var context = new SetupDB();
            var articleGroup = context.ArticelGroups.Find(id);
            return articleGroup;
        }

        /// <summary>
        ///  CREATE Article Group
        /// </summary>
        public void NewArticleGroup(ArticleGroupDTO articleGroupDTO)
        {
            using var context = new SetupDB();
            context.ArticelGroups.Add(articleGroupDTO);
            context.SaveChanges();
        }

        /// <summary>
        ///  UPDATE Article Group
        /// </summary>
        public void UpdateArticleGroup(ArticleGroupDTO articleGroupDTO)
        {
            using var context = new SetupDB();
            var articleGroup = context.ArticelGroups.Find(articleGroupDTO.Id);
            if (articleGroup != null)
            {
                articleGroup.Name = articleGroupDTO.Name;
                articleGroup.ParentArticleGroupId = articleGroupDTO.ParentArticleGroupId;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Article Group by ID
        /// </summary>
        public void DeleteArticleGroup(int id)
        {
            using var context = new SetupDB();
            var articleGroup = context.ArticelGroups.Find(id);
            context.ArticelGroups.Remove(articleGroup);
            context.SaveChanges();
        }

        /// <summary>
        ///  CREATE Article
        /// </summary>
        public void NewArticle(ArticleDTO articleDTO)
        {
            using var context = new SetupDB();
            context.Articles.Add(articleDTO);
            context.SaveChanges();
        }
                                                                                                            
        /// <summary>
        ///  READ all Article
        /// </summary>
        public ArticleDTO[] GetAllArticle()
        {
            using var context = new SetupDB();
            return context.Articles.Include(c => c.ArticleGroup).ToArray();
        }

        /// <summary>
        ///  READ Article by ID
        /// </summary>
        public ArticleDTO GetArticleById(int id)
        {
            using var context = new SetupDB();
            return context.Articles.Include(c => c.ArticleGroup).Where(c => c.Id == id).ToArray()[0];                                                                                                                                                                                                                      
        }

        /// <summary>
        ///  UPDATE Article
        /// </summary>
        public void UpdateArticle(ArticleDTO articleDTO)
        {
            using var context = new SetupDB();
            var article = context.Articles.Find(articleDTO.Id);
            if (article != null)
            {
                article.Name = articleDTO.Name;
                article.ArticleGroupId = articleDTO.ArticleGroupId;
                article.ArticleGroup = articleDTO.ArticleGroup;
                article.Price = articleDTO.Price;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Article by ID
        /// </summary>
        public void DeleteArticleById(int id)
        {
            using var context = new SetupDB();
            var article = context.Articles.Find(id);
            context.Articles.Remove(article);
            context.SaveChanges();
        }

        /// <summary>
        ///  CREATE Order
        /// </summary>
        public void NewOrder(OrderDTO orderDTO)
        {
            using var context = new SetupDB();
            context.Orders.Add(orderDTO);
            context.SaveChanges();
        }

        /// <summary>
        ///  READ all Orders
        /// </summary>
        public OrderDTO[] GetAllOrders()
        {
            using var context = new SetupDB();
            return context.Orders.Include(c => c.Customer).Include(a => a.Customer.Address).ToArray();
        }

        /// <summary>
        ///  READ Order by ID
        /// </summary>
        public OrderDTO GetOrderByID(int id)
        {
            using var context = new SetupDB();
            return context.Orders.Include(c => c.Customer).Where(c => c.Id == id).ToArray()[0];
        }

        /// <summary>
        ///  UPDATE Order
        /// </summary>
        public void UpdateOrder(OrderDTO orderDTO)
        {
            using var context = new SetupDB();
            var order = context.Orders.Find(orderDTO.Id);
            if (order != null)
            {
                order.CustomerId = orderDTO.CustomerId;
                order.Date = orderDTO.Date;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Order by ID
        /// </summary>
        public void DeleteOrderById(int id)
        {
            using var context = new SetupDB();
            var order = context.Orders.Find(id);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        /// <summary>
        ///  CREATE OrderPosition
        /// </summary>
        public void NewOrderPosition(OrderPositionDTO orderPositionDTO)
        {
            using var context = new SetupDB();
            context.OrderPositions.Add(orderPositionDTO);
            context.SaveChanges();
        }

        /// <summary>
        ///  READ all Orderposition by OrderID
        /// </summary>
        public OrderPositionDTO[] GetOrderPositionByOrderID(int id)
        {
            using var context = new SetupDB();
            return context.OrderPositions.Include(c => c.Article).Where(x => x.OrderId == id).Include(g => g.Article.ArticleGroup).ToArray();
        }

        public OrderPositionDTO GetOrderPositionByID(int id)
        {
            using var context = new SetupDB();
            return context.OrderPositions.Include(c => c.Article).Where(x => x.Id == id).ToArray()[0];
        }

        /// <summary>
        ///  UPDATE OrderPosition
        /// </summary>
        public void UpdateOrderPosition(OrderPositionDTO orderPositionDTO)
        {
            using var context = new SetupDB();
            var orderPosition = context.OrderPositions.Find(orderPositionDTO.Id);
            if (orderPosition != null)
            {
                orderPosition.ArticleId = orderPositionDTO.ArticleId;
                orderPosition.Quantity = orderPositionDTO.Quantity;
                orderPosition.OrderId = orderPositionDTO.OrderId;
            }
            context.SaveChanges();
        }

        /// <summary>
        ///  DELETE Orderpostion by ID
        /// </summary>
        public void DeleteOrderPositionById(int id)
        {
            using var context = new SetupDB();
            var orderPosition = context.OrderPositions.Find(id);
            context.OrderPositions.Remove(orderPosition);
            context.SaveChanges();
        }

    }
}
