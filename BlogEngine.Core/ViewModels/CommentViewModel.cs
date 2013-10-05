using System;

namespace BlogEngine.Core.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
    }
}
