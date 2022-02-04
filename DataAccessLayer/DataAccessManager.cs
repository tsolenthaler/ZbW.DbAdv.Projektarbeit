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
            //SeedingDatabase();
            if (context.Customers.Any(o => o.Id == 1))
            {
                // No INITAL DATA Customer with 1 exist
            }
            else
            {
                SeedingDatabase();
            }
        }

        public void SeedingDatabase()
        {
            using var context = new SetupDB();
            var seedDb = new SeedDB();

            context.AddRange(seedDb.GenerateCustomerDTOs());
            context.SaveChanges();
            context.AddRange(seedDb.GenerateArticleGroupDTOs());
            context.SaveChanges();
            context.AddRange(seedDb.GenerateArticleDTOs());
            context.SaveChanges();
            context.AddRange(seedDb.GenerateOrderDTOs());
            context.SaveChanges();
            context.AddRange(seedDb.GenerateOrderPositionDTOs());
            context.SaveChanges();
        }

        public CustomerDTO[] GetAllCustomers()
        {
            using var context = new SetupDB();
            var customers = context.Customers.Include(c => c.Address).ToArray();
            return customers;
        }


        public CustomerDTO GetCustomerById(int id)
        {
            using var context = new SetupDB();
            var customer = context.Customers
                .Include(c => c.Address)
                .Where(c => c.Id == id).ToArray();

            return customer[0];
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {
            //only for debugging, delete when method is implemented
            Console.WriteLine("Customer updated"); 
        }

        public void NewCustomer(CustomerDTO customerDto)
        {
            //only for debugging, delete when method is implemented
            Console.WriteLine("Customer created");
        }

        public void DeleteCustomerById(int id)
        {
            using var context = new SetupDB();
            //var customer = context.Customers.Find(id);
            var customer = context.Customers
                .Include(a => a.Address)
                .Single(a => a.Id == id);
            
            context.Addresses.RemoveRange(customer.Address); // Löscht auch die Adresse
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public ArticleGroupDTO GetArticleGroupById(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> Update(MyObject item)
        //{
        //    Context.Entry(await Context.MyDbSet.FirstOrDefaultAsync(x => x.Id == item.Id)).CurrentValues.SetValues(item);
        //    return (await Context.SaveChangesAsync()) > 0;
        //}

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
