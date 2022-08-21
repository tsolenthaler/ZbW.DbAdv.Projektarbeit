using DataAccessLayer.Customer;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Order
{
    public class OrderDTO : TEntity
    {
        public DateTime? Date { get; set; }
        public int CustomerId { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        
    }
}