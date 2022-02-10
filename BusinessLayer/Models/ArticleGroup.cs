using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer.Models
{
    public class ArticleGroup : BusinessModelBase
    {
        private string name = String.Empty;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private ArticleGroup parentArticleGroup = new ArticleGroup();
        public ArticleGroup ParentArticleGroup
        {
            get => parentArticleGroup; 
            set => Set(ref parentArticleGroup, value);
        }

        public ArticleGroup()
        {

        }

        public ArticleGroup(ArticleGroupDTO articleGroupDto, DataAccessManager dataAccessManager)
        {
            Id = articleGroupDto.Id;
            Name = articleGroupDto.Name;
            ParentArticleGroup = new ArticleGroup(dataAccessManager.GetArticleGroupById(articleGroupDto.Id), dataAccessManager);
        }

        public ArticleGroupDTO ToArticleGroupDto()
        {
            var articleGroupDto = new ArticleGroupDTO();
            articleGroupDto.Id = Id;
            articleGroupDto.Name = Name;
            articleGroupDto.ParentArticleGroupId = ParentArticleGroup.Id;
            return articleGroupDto;
        }
    }
}