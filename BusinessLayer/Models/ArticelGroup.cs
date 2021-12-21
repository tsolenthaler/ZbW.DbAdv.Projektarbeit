using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    public class ArticelGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ArticelGroup(ArticleGroupDTO articleGroupDto)
        {
            //implement assignment after DTO is defined
        }
    }
}