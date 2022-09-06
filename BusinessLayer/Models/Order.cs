using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Order;

namespace BusinessLayer.Models
{
    public class Order : BusinessModelBase {
        private int id;
        public int Id
        {
            get => id;
            set => Set(ref id, value);
        }

        private DateTime? date;
        public DateTime? Date
        {
            get => date;
            set => Set(ref date, value);
        }

        private Customer customer = new Customer();
        public Customer Customer
        {
            get => customer;
            set => Set(ref customer, value);
        }

        public Order()
        {

        }

        public Order(OrderDTO orderDto)
        {
            Id = orderDto.Id;
            Date = orderDto.Date;
            Customer = new Customer(orderDto.Customer);
        }

        public OrderDTO ToOrderDto()
        {
            var orderDto = new OrderDTO();
            orderDto.Id = Id;
            orderDto.CustomerId = Customer.Id;
            orderDto.Customer = Customer.ToCustomerDto();
            orderDto.Date = Date;
            return orderDto;
        }
    }
}