using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Context
{
    public class SeedDB : UseCase
    {
        public override async Task<string> ExecuteAsync()
        {
            try
            {
                using (var context = CreateSession())
                {
                    CustomerDTO customer = await AddCustomerAsync(context);
                    await AddAddressAsync(context);
                    await AddArticleAsync(context);
                    await AddArticleGroupAsync(context);
                    await AddOrderAsync(context);
                    await AddOrderPosition(context);

                    await context.SaveChangesAsync(); // Asynchrone SaveChanges / Change Tracking
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return "seeded initial data";
        }

        private async Task<AddressDTO> AddAddressAsync(SetupDB context, string street)
        {
            var address = new AddressDTO { 
                Street = street,
                StreetNo = 33, 
                Plz = "9000",
                City = "St.Gallen"
            };

            await context.Address.AddAsync(address);
            return address;
        }

        private async Task<CustomerDTO> AddCustomerAsync(SetupDB context, String firstname, AddressDTO address)
        {
            var customer = new CustomerDTO { Firstname = firstname, Address = address};
            System.Console.WriteLine($"customer id before: [{customer.Id}"); // Zuerst ID == 0;
            await context.Customer.AddAsync(customer);
            System.Console.WriteLine($"customer id after: [{customer.Id}");
            return customer;
        }
    }
}
