using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.ViewModels
{
    public class CommentSummaryVM
    {
        public int TotalComments { get; set; }
        public int CommentsLast24hours { get; set; }
        public string UserMostComments { get; set; }
        public List<UserCommentSummaryVM> UsersCommentCount { get; set; }
    }
}
