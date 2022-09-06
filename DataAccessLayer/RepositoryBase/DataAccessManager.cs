using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Context;
using DataAccessLayer.Customer;
using DataAccessLayer.Invoice;
using DataAccessLayer.Order;
using DataAccessLayer.OrderPosition;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
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
                seedDb.SeedArticleDTOsWithDatumInThePast();
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
            if (!context.Invoices.Any())
            {
                context.AddRange(seedDb.GenerateInvoiceDTOs());
                context.SaveChanges();
                seedDb.SeedCustomerHistory();
                seedDb.SeedAddressesHistory();
                context.AddRange(seedDb.ChangeCustomerDTOs());
                context.SaveChanges();
            }
        }

        
    }
}
