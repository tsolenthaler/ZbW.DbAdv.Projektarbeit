using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class ArticleGroup : BusinessModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private ArticleGroup parentArticleGroup;
        public ArticleGroup ParentArticleGroup
        {
            get => parentArticleGroup; 
            set => Set(ref parentArticleGroup, value);
        }

        public ArticleGroup()
        {

        }

        public ArticleGroup(ArticleGroupDTO articleGroupDto)
        {
            //implement assignment after DTO is defined
            Id = articleGroupDto.Id;
            Name = articleGroupDto.Name;
            ParentArticleGroup = articleGroupDto.ParentArticleGroup;
        }
    }
}

//public int Id { get; set; }
//public string Name { get; set; }
//public int ParentArticleGroupId { get; set; }