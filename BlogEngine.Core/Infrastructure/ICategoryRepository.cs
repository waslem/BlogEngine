using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Infrastructure
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        bool Create(CategoryViewModel model);
        Category GetCategoryById(int id);

        void EditCategory(Category model);
        void EditCategory(CategoryViewModel model);

        bool Delete(int id);
    }
}
