using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public virtual CustomerDTO Customer { get; set; }
    }
}