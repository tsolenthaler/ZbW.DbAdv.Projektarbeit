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

        public CustomerDTO GetByCustomerNr(string Customernr)
        {
            using var context = new SetupDB();
            var customer = context.Customers.Where(c => c.Clientnr == Customernr).FirstOrDefault();
            return customer;
        }
    }
}
