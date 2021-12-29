using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class OrderPositionDTO
    {
        public int Id { get; set; }
        public virtual ArticleDTO Article { get; set; }
        public int Quantity { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}