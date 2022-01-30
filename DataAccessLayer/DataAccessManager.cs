using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DataAccessLayer.Models
{
    public class DataAccessManager
    {
        public DataAccessManager()
        {
            //MigrateDatabase();
        }

        private void MigrateDatabase()
        {
            
        }

        public void Main()
        {
            using var context = new SetupDB();

            context.Database.Migrate();

            // Test Kunde Hinzufügen
            var customer1 = new CustomerDTO()
            {
                Firstname = "Stefan",
                Lastname = "Müller",
                Address = new AddressDTO(),
                EMail = "smueller@company.com",
                Website = "https://company.com",
                Password = "test"
            };

            context.Customers.Add(customer1);
            context.SaveChanges();
        }
    }
}
