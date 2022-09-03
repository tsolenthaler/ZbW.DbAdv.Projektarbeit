﻿using DataAccessLayer.Context;
using DataAccessLayer.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Customer
{
    public class CustomerRepository : RepositoryBase<CustomerDTO>, ICustomerRepository
    {
        public CustomerRepository(SetupDB context) : base(context)
        {
            
        }

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
            using var context = _context;
            //var customer = context.Customers.Find(id);
            var customer = context.Customers
                .TemporalAsOf(date)
                .Include(c => c.Address)
                .Where(c => c.Id == id).ToArray();

            return customer[0];
        }
    }
}