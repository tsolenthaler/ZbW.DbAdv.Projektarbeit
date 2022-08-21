using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.ArticleGroup
{
    public class ArticleGroupRepository : RepositoryBase<ArticleGroupDTO>, IArticleGroupRepository
    {
        public ArticleGroupRepository(SetupDB context) : base(context)
        {
        }

        public ArticleGroupDTO[] GetAllArticleGroupsRecursiveCte()
        {
            string sqlCommand = "WITH CTE_ARTICLEGROUPS (Id, Name, ParentArticleGroupId, ValidFrom, ValidTo ) AS (SELECT Id, Name, ParentArticleGroupId, ValidFrom, ValidTo " +
                                "FROM dbo.ArticelGroups WHERE ParentArticleGroupId = 0 UNION ALL " +
                                "SELECT pn.Id,pn.Name, pn.ParentArticleGroupId, pn.ValidFrom, pn.ValidTo FROM dbo.ArticelGroups AS " +
                                "pn INNER JOIN CTE_ARTICLEGROUPS AS p1 ON p1.Id = pn.ParentArticleGroupId) SELECT	" +
                                "Id,Name, ParentArticleGroupId, ValidFrom, ValidTo FROM CTE_ARTICLEGROUPS ORDER BY ParentArticleGroupId";
            using var context = new SetupDB();
            var articleGroups = context.ArticelGroups.FromSqlRaw(sqlCommand);
            return articleGroups.ToArray();
        }
    }
}
