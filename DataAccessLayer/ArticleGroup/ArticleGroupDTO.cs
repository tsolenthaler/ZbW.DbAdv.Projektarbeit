using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.ArticleGroup
{
    public class ArticleGroupDTO : TEntity
    {
        public string Name { get; set; }
        public int ParentArticleGroupId { get; set; }
    }
}
