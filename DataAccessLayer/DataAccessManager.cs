using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public CustomerDTO[] GetAllCustomers()
        {
            using var context = new SetupDB();
            var customers = context.Customers.Include(c => c.Address).ToArray();
            return customers;
        }

        //public async Task<bool> Update(MyObject item)
        //{
        //    Context.Entry(await Context.MyDbSet.FirstOrDefaultAsync(x => x.Id == item.Id)).CurrentValues.SetValues(item);
        //    return (await Context.SaveChangesAsync()) > 0;
        //}

        public CustomerDTO GetCustomerFromId(int id)
        {
            using var context = new SetupDB();
            var customer = context.Customers
                .Include(c => c.Address)
                .Where(c => c.Id == id).ToArray();

            return customer[0];
        }

        //public ObservableCollection<CustomerDTO> GetAllCustomers()
        //{
        //    ObservableCollection<CustomerDTO> customers = new ObservableCollection<CustomerDTO>();

        //    using var context = new SetupDB();

        //    var query =
        //        from c in context.Customers
        //        join a in context.Addresses on c.AddressId equals a.Id
        //        select c;

        //    foreach (CustomerDTO c in query)
        //    {
        //        customers.Add(c);
        //    }

        //    return customers;
        //}

        //public void Main()
        //{
        //    using var context = new SetupDB();

        //    context.Database.Migrate();

        //    // Test Kunde Hinzufügen
        //    var customer1 = new CustomerDTO()
        //    {
        //        Firstname = "Stefan",
        //        Lastname = "Müller",
        //        Address = new AddressDTO(),
        //        EMail = "smueller@company.com",
        //        Website = "https://company.com",
        //        Password = "test"
        //    };

        //    context.Customers.Add(customer1);
        //    context.SaveChanges();
        //}

    }


}
