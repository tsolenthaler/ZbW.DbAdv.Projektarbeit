using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class ArticleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ArticleGroup(ArticleGroupDTO articleGroupDto)
        {
            //implement assignment after DTO is defined
        }
    }
}