using DataAccessLayer.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;

namespace DataAccessLayer.Article
{
    public class ArticleRepository : RepositoryBase<ArticleDTO>, IArticleRepository
    {
        public ArticleRepository(SetupDB context) : base(context)
        {
        }


    }
}
