using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class ArticleGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }   

        public int ParentArticleGroupId { get; set; }
    }
}
