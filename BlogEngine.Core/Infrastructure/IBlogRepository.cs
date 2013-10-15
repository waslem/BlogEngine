using BlogEngine.Core.Models;
using System.Collections.Generic;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;

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
    }
}
