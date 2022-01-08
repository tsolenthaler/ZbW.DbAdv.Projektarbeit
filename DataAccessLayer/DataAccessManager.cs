using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class DataAccessManager
    {
        static void Main()
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
                Password = new HashCode()
            };

            context.Customer.Add(customer1);
            context.SaveChanges();
        }
    }
}
