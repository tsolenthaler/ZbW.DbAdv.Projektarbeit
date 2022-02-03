﻿using System;
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

        public Order()
        {

        }

        public Order(OrderDTO orderDto)
        {
            Id = orderDto.Id;
            Date = orderDto.Date;
            Customer = new Customer(orderDto.Customer);
        }
    }
}