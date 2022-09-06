using DataAccessLayer.Context;
using DataAccessLayer.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Invoice
{
    public class InvoiceRepository : RepositoryBase<InvoiceDTO>, IInvoiceRepository
    {
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
            var invoices = context.Invoices.Include(c => c.Customer).Include(a => a.Customer.Address);
            var customers = context.Customers.Include(a => a.Address);
            var customersEntry = context.Entry(customers);
            return invoices.ToArray();
        }


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
