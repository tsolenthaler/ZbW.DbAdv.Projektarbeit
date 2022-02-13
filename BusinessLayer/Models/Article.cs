using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Article : BusinessModelBase
    {
        private ArticleGroup articlegroup = new ArticleGroup();
        public ArticleGroup ArticleGroup
        {
            get => articlegroup;
            set => Set(ref articlegroup, value);
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set => Set(ref price, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public override string ToString()
        {
            return Id + "| " + Name + " (" + ArticleGroup + ")";
        }

        public Article()
        {

        }

        public Article(ArticleDTO articleDto)
        {
            Id = articleDto.Id;
            Name = articleDto.Name;
            Price = articleDto.Price;
        }

        public ArticleDTO ToArticleDto()
        {
            var articleDto = new ArticleDTO();
            articleDto.Id = Id;
            articleDto.Name = Name;
            articleDto.Price = Price;
            articleDto.ArticleGroup = ArticleGroup.ToArticleGroupDto();
            articleDto.ArticleGroupId = ArticleGroup.Id;
            return articleDto;
        }
    }
}
