using DataAccessLayer.Article;
using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Context;
using DataAccessLayer.Customer;
using DataAccessLayer.Models;
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

        public List<YearEndStatisticDTO> GetYearEndingData()
        {
            var yearEndStatistics = new List<YearEndStatisticDTO>();

            using var context = new SetupDB();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = SqlRawCommands.YearEndingRequest;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                YearEndStatisticDTO yearEndStatisticDto = new YearEndStatisticDTO()
                {
                    Year = Convert.ToString(reader[0]),
                    Quarter = Convert.ToString(reader[1]),
                    TotalOrdersPerQuarter = Convert.ToString(reader[2]),
                    AvgArticleQtySumPerOrderPerQuarter = Convert.ToString(reader[3]),
                    AvgSumSalesPerCustomerPerQuarter = Convert.ToString(reader[4]),
                    SalesTotalPerQuarter = Convert.ToString(reader[5]),
                    TotalArticlesInTheSystem = Convert.ToString(reader[6]),
                };

                yearEndStatistics.Add(yearEndStatisticDto);
            }
            context.Database.CloseConnection();
            return yearEndStatistics;
        }



 

        /// <summary>
        ///  READ all Invoices
        /// </summary>
        public InvoiceDTO[] GetAllInvoices()
        {
            using var context = new SetupDB();
            //return context.Invoices.Include(c => c.Customer).Include(a => a.Customer.Address).ToArray();
            var invoices = context.Invoices.Include(c => c.Customer).Include(a => a.Customer.Address);
            var customers = context.Customers.Include(a => a.Address);
            var customersEntry = context.Entry(customers);

            //var VaildFrom = context.Entry(customers).Property("VaildFrom").CurrentValue;
            //var VaildTo = context.Entry(customers).Property("VaildTo").CurrentValue;
            return invoices.ToArray();
        }

        /// <summary>
        ///  READ all Invoices by Date
        /// </summary>
        /*public InvoiceDTO[] GetAllInvoicesbyDate(DateTime startDate, DateTime endDate)
        {
            using var context = new SetupDB();
            //return context.Invoices.Include(c => c.Customer).Include(a => a.Customer.Address).ToArray();
            //var invoices = context.Invoices.TemporalAsOf(DateTime.UtcNow).Include(c => c.Customer).Include(a => a.Customer.Address).Where(x => x.Date >= startDate && x.Date <= endDate);
            var invoices = context.Invoices
                .FromSqlRaw("SELECT [CustomerId], c.Company as Name, a.Street as Street, a.StreetNo as StreetNo, a.Plz as Plz, a.City as City, a.Countryname as Countryname,i.[Id],i.[Date] As Date,[Netto],[Brutto], i.ValidFrom as ValidFrom, i.ValidTo as ValidTo FROM [AuftragsverwaltungHistory].[dbo].[Invoices] i INNER JOIN dbo.Customer FOR SYSTEM_TIME ALL as c on i.customerid = c.id INNER JOIN dbo.Addresses FOR SYSTEM_TIME ALL as a on c.addressid = a.id WHERE i.Date BETWEEN '2021-01-01' and '2022-03-06' AND c.ValidFrom <= i.Date and c.ValidTo >= i.Date AND a.ValidFrom <= i.Date and a.ValidTo >= i.Date ORDER BY i.Date");
            return invoices.ToArray();
        }*/

        public List<InvoiceReportDTO> GetAllInvoicesbyDate(DateTime startDate, DateTime endDate)
        {
            var invoiceReports = new List<InvoiceReportDTO>();

            DateTime startDate2 = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            DateTime endDate2 = new DateTime(endDate.Year, endDate.Month, endDate.Day);

            string startDateFormat = startDate.ToString("s");
            string endDateFormat = endDate.ToString("s");

            using var context = new SetupDB();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT [CustomerId], c.Company as Name, a.Street as Street, a.StreetNo as StreetNo, a.Plz as Plz, a.City as City, a.Countryname as Countryname,i.[Id],i.[Date] As Date,[Netto],[Brutto], i.ValidFrom as ValidFrom, i.ValidTo as ValidTo FROM [AuftragsverwaltungHistory].[dbo].[Invoices] i INNER JOIN dbo.Customer FOR SYSTEM_TIME ALL as c on i.customerid = c.id INNER JOIN dbo.Addresses FOR SYSTEM_TIME ALL as a on c.addressid = a.id WHERE i.Date BETWEEN '{startDateFormat}' and '{endDateFormat}' AND c.ValidFrom <= i.Date and c.ValidTo >= i.Date AND a.ValidFrom <= i.Date and a.ValidTo >= i.Date ORDER BY i.Date";

            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                InvoiceReportDTO invoiceReport = new InvoiceReportDTO()
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    Name = Convert.ToString(reader["Name"]),
                    Street = Convert.ToString(reader["Street"]),
                    StreetNo = Convert.ToString(reader["StreetNo"]),
                    Plz = Convert.ToString(reader["Plz"]),
                    City = Convert.ToString(reader["City"]),
                    Id = Convert.ToInt32(reader["Id"]),
                    Date = (DateTime)reader["Date"],
                    Netto = (Double)reader["Netto"],
                    Brutto = (Double)reader["Brutto"],
                };

                invoiceReports.Add(invoiceReport);
            }

            context.Database.CloseConnection();
            return invoiceReports;
        }
    }
}
