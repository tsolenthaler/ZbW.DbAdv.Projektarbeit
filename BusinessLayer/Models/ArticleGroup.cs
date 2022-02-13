using System;
using System.Collections.Generic;
using System.Dynamic;
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
            get => GetParentArticleGroup(new DataAccessManager()); 
            set => Set(ref parentArticleGroup, value);
        }

        private int parentArticleGroupId;
        public int ParentArticleGroupId
        {
            get => parentArticleGroupId;
            set => Set(ref parentArticleGroupId, value);
        }

        public ArticleGroup()
        {

        }

        public ArticleGroup(ArticleGroupDTO articleGroupDto)
        {
            Id = articleGroupDto.Id;
            Name = articleGroupDto.Name;
            ParentArticleGroupId = articleGroupDto.ParentArticleGroupId;
        }

        private ArticleGroup GetParentArticleGroup(DataAccessManager dataAccessManager)
        {
            if (ParentArticleGroupId == 0)
            {
                return new ArticleGroup()
                {
                    Id = 0,
                    Name = "No parent"
                };
            }
            else
            {
                return new ArticleGroup(dataAccessManager.GetArticleGroupById(ParentArticleGroupId));
            }
        }

        public ArticleGroupDTO ToArticleGroupDto()
        {
            var articleGroupDto = new ArticleGroupDTO();
            articleGroupDto.Id = Id;
            articleGroupDto.Name = Name;
            articleGroupDto.ParentArticleGroupId = ParentArticleGroup.Id;
            return articleGroupDto;
        }

        public override string ToString()
        {
            return Id + "| " + Name;
        }
    }
}