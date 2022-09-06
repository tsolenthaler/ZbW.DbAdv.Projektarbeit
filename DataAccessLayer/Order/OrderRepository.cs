using DataAccessLayer.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;

namespace DataAccessLayer.Order
{
    public class OrderRepository : RepositoryBase<OrderDTO>, IOrderRepository
    {

    }
}
