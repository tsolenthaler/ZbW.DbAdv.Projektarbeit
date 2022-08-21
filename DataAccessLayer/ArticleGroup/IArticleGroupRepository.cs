using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.RepositoryBase;

namespace DataAccessLayer.ArticleGroup
{
    public interface IArticleGroupRepository : IRepositoryBase<ArticleGroupDTO>
    {
        public ArticleGroupDTO[] GetAllArticleGroupsRecursiveCte();
    }
}
