using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public virtual ArticleGroupDTO ArticleGroup { get; set; }
        public int ArticleGroupId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }   
    }
}