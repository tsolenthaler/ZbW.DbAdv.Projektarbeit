using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Customer
{
    public interface ICustomerRepository : IRepositoryBase<CustomerDTO>
    {
        public CustomerDTO GetByIdValidDate(int id, DateTime date);
        public CustomerDTO GetByCustomerNr(string Customernr);
    }
}
