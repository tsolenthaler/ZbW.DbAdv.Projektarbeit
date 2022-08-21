using DataAccessLayer.ArticleGroup;
using DataAccessLayer.Models;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.Article
{
    public class ArticleDTO : TEntity
    {
        //public int Id { get; set; }
        public virtual ArticleGroupDTO ArticleGroup { get; set; }
        public int ArticleGroupId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }   
    }
}