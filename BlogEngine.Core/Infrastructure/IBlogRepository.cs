using BlogEngine.Core.Models;
using System.Collections.Generic;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Infrastructure
{
    public interface IBlogRepository
    {
        ICollection<Blog> GetAll();
        ICollection<BlogListViewModel> GetAllViewModel();

        void Create(Blog blog);

        Blog GetBlogById(int id);
        void Edit(BlogViewModel model);
        void Delete(int id);
    }
}
