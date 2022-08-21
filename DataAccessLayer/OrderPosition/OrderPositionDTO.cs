using DataAccessLayer.Article;
using DataAccessLayer.Order;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.OrderPosition
{
    public class OrderPositionDTO : TEntity
    {
        public int ArticleId { get; set; }
        public virtual ArticleDTO Article { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}