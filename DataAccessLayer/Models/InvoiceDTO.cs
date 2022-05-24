using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public double Netto { get; set; }
        public double Brutto { get; set; }
    }
}
