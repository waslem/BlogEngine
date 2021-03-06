﻿
using System.Collections.Generic;
using BlogEngine.Core.Models;

namespace BlogEngine.Core.ViewModels
{
    public class BlogListViewModel
    {
        public int BlogId { get; set; }
        public string ShortDescription { get; set; }
        public string Entry { get; set; }
        public string Title { get; set; }
        public string Category { get; set; } 
        public string CreatedBy { get; set; } 
        public string CreatedDate { get; set; }
        public int CommentCount { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
