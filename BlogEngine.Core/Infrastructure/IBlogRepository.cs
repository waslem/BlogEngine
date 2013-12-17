using BlogEngine.Core.Models;
using System.Collections.Generic;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Infrastructure
{
    public interface IBlogRepository
    {

        ICollection<BlogEntry> GetAll();
        ICollection<BlogEntryView> GetAllView();
        ICollection<BlogListViewModel> GetAllViewModel();
        void Create(BlogEntry blogEntry);

        BlogEntry GetBlogById(int id);
        void Edit(BlogViewModel model);
        void Delete(int id);

        ICollection<BlogEntry> GetTopBlogs(int blogCount);

        IEnumerable<BlogEntry> GetBlogsByCategory(string category);
        List<BlogEntry> GetBlogsByTag(string tag);
        ICollection<BlogEntryView> Search(string search);
    }
}
