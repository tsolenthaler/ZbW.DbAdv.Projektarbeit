using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        // Null Artikel-Gruppen zulässig, daher das "?"
        public virtual ICollection<ArticleGroupDTO>? ArticleGroups { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }   
    }
}