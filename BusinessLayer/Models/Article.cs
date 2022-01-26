using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Article : BusinessModelBase
    {
        public ArticleGroup ArticleGroup { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Id + "| " + Name + " (" + ArticleGroup + ")";
        }

        public Article()
        {

        }

        public Article(ArticleDTO articleDto)
        {
            //implement assignment after DTO is defined
        }
    }
}