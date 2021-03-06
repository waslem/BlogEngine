﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BlogEngine.Core.Models
{
    public class BlogEntry
    {
        public int BlogEntryId { get; set; }

        public string BlogTitle { get; set; }
        public string BlogShortDescription { get; set; }

        [AllowHtml]
        public string BlogEntryText { get; set; }

        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int? BlogImageId { get; set; }
        public virtual BlogImage Image { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
