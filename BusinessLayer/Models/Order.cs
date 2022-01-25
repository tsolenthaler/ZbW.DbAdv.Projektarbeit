using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public Customer Customer { get; set; }
        public List<OrderPosition> Positions { get; set; } = new List<OrderPosition>();

        public Order()
        {

        }

        public Order(OrderDTO orderDto)
        {
            //implement assignment after DTO is defined
        }
    }
}