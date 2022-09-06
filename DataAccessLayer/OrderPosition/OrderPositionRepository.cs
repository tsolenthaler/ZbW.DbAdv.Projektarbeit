using DataAccessLayer.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.OrderPosition
{
    public class OrderPositionRepository : RepositoryBase<OrderPositionDTO>, IOrderPositionRepository
    {
        public OrderPositionDTO[] GetAllByOrderId(int id)
        {
            using var context = new SetupDB();
            return context.OrderPositions.Include(c => c.Article).Where(x => x.OrderId == id).Include(g => g.Article.ArticleGroup).ToArray();
        }
    }
}
