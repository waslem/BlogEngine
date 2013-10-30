using BlogEngine.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.ViewModels
{
    public class BlogEntryView
    {
        public int BlogEntryId { get; set; }

        [Required]
        public string BlogTitle { get; set; }

        [Required]
        public string BlogShortDescription { get; set; }

        [Required]
        public string BlogEntryText { get; set; }

        public string DateCreated { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual BlogImage Image { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public BlogEntryView()
        {
            Tags = new  List<Tag>();
        }
    }
}
