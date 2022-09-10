using DataAccessLayer.Context;
using DataAccessLayer.Invoice;
using DataAccessLayer.RepositoryBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataAccessLayer.Customer
{
    public class CustomerRepository : RepositoryBase<CustomerDTO>, ICustomerRepository
    {

        public override void Delete(CustomerDTO customer)
        {
            base.Delete(customer);
            using var context = new SetupDB();
            var address = context.Set<AddressDTO>().Find(customer.AddressId);
            if (address == null)
            {
                return;
            }
            context.Set<AddressDTO>().Remove(address);
        }

        public CustomerDTO GetByIdValidDate(int id, DateTime date)
        {
            using var context = new SetupDB();
            //var customer = context.Customers.Find(id);
            var customer = context.Customers
                .TemporalAsOf(date)
                .Include(c => c.Address)
                .Where(c => c.Id == id).ToArray();

            return customer[0];
        }

        /*public CustomerDTO[] GetAllCustomersByValidDate(DateTime date)
        {
            DateTime startDate2 = new DateTime(date.Year, date.Month, date.Day);

            string startDateFormat = startDate2.ToString("s");

            string sqlCommand = $"SELECT c.id as Id, c.clientnr as clientnr, c.Company as Company, c.addressid as AddressId, c.email as Email, c.password as Password, c.website as Website, c.Firstname as Firstname, c.Lastname as Lastname, c.ValidFrom as ValidFrom, c.ValidTo as ValidTo, a.Street as Street, a.StreetNo as StreetNo, a.Plz as Plz, a.City as City FROM [AuftragsverwaltungHistory].[dbo].[Customer] FOR SYSTEM_TIME ALL as c LEFT JOIN [AuftragsverwaltungHistory].[dbo].[Addresses] FOR SYSTEM_TIME ALL as a on c.addressid = a.id WHERE c.ValidFrom <= '{startDateFormat}' and c.ValidTo >= '{startDateFormat}' AND a.ValidFrom <= '{startDateFormat}' and a.ValidTo >= '{startDateFormat}'";

            Debug.WriteLine(sqlCommand);
            using var context = new SetupDB();
            var customers = context.Customers.FromSqlRaw(sqlCommand);
            return customers.ToArray();
        }*/

        /*public CustomerDTO[] GetAllCustomersByValidDate(DateTime date)
        {
            DateTime startDate2 = new DateTime(date.Year, date.Month, date.Day);

            string startDateFormat = startDate2.ToString("s");

            using var context = new SetupDB();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT c.id as Id, c.clientnr as clientnr, c.Company as Company, c.addressid as AddressId, c.email as Email, c.password as Password, c.website as Website, c.Firstname as Firstname, c.Lastname as Lastname, c.ValidFrom as ValidFrom, c.ValidTo as ValidTo, a.Street as Street, a.StreetNo as StreetNo, a.Plz as Plz, a.City as City FROM [AuftragsverwaltungHistory].[dbo].[Customer] FOR SYSTEM_TIME ALL as c LEFT JOIN [AuftragsverwaltungHistory].[dbo].[Addresses] FOR SYSTEM_TIME ALL as a on c.addressid = a.id WHERE c.ValidFrom <= '{startDateFormat}' and c.ValidTo >= '{startDateFormat}' AND a.ValidFrom <= '{startDateFormat}' and a.ValidTo >= '{startDateFormat}'";

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
        }*/
    }
}
