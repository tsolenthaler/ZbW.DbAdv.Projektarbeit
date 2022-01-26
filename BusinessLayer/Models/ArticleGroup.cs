using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class ArticleGroup : BusinessModelBase
    {
        public string Name { get; set; }

        public ArticleGroup()
        {

        }

        public ArticleGroup(ArticleGroupDTO articleGroupDto)
        {
            //implement assignment after DTO is defined
        }
    }
}