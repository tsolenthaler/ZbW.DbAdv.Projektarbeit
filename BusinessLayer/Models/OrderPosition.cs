using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class OrderPosition
    {
        public int Id { get; set; }

        public int PosNo { get; set; }

        public Article Article { get; set; }
        public int Quantity { get; set; }
        
        public Order Order { get; set; }

        public OrderPosition()
        {

        }

        public OrderPosition(OrderPositionDTO orderPositionDto)
        {
            //implement assignment after DTO is defined
        }

    }
}