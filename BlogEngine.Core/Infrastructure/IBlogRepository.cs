using BlogEngine.Core.Contexts;
using BlogEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.ViewModels;

namespace BlogEngine.Core.Infrastructure
{
    public interface IBlogRepository
    {
        ICollection<Blog> GetAll();
        ICollection<BlogListViewModel> GetAllViewModel();

        void Create(Blog blog);

    }
}
