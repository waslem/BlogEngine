using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }
        public int BlogId { get; set; }
        public int? ParentId { get; set; }

        [Required]
        [DisplayName("Enter a comment...")]
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
