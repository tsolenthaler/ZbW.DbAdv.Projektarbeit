using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Customer;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Invoice
{
    public class InvoiceDTO : TEntity
    {
        public DateTime? Date { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public double Netto { get; set; }
        public double Brutto { get; set; }
    }
}
