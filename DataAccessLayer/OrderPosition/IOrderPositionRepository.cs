using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.OrderPosition
{
    public interface IOrderPositionRepository : IRepositoryBase<OrderPositionDTO>
    {
        public OrderPositionDTO[] GetAllByOrderId(int id);
    }
}
