using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

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

        private Customer customer;
        public Customer Customer
        {
            get => customer;
            set => Set(ref customer, value);
        }

        public ICollection<OrderPosition> OrderPositions { get; set; } = new List<OrderPosition>();

        public Order()
        {

        }

        public Order(OrderDTO orderDto)
        {
            //implement assignment after DTO is defined
            Id = orderDto.Id;
            Date = orderDto.Date;
            Customer = orderDto.Customer;
        }
    }
}

//public int Id { get; set; }
//public DateTime? Date { get; set; }
//public virtual CustomerDTO Customer { get; set; }
//public int CustomerId { get; set; }