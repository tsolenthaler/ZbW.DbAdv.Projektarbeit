﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessLayer
{
    public class Article
    {
        public int Id { get; set; }
        public ArticleGroup ArticelGroup { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

        public Article(ArticleDTO articleDto)
        {
            //implement assignment after DTO is defined
        }
    }
}