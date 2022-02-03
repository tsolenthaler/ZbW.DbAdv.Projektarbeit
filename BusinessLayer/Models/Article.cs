using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class Article : BusinessModelBase
    {
        private ArticleGroup articlegroup;
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
            //implement assignment after DTO is defined
            Id = articleDto.Id;
            Name = articleDto.Name;
            Price = articleDto.Price;
        }
    }
}

//public int Id { get; set; }
//public virtual ArticleGroupDTO ArticleGroup { get; set; }
//public int ArticleGroupId { get; set; }
//public decimal Price { get; set; }
//public string Name { get; set; }