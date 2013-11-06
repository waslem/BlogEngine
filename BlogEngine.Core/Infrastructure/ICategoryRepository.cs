using System.Web.Mvc;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System.Collections.Generic;

namespace BlogEngine.Core.Infrastructure
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();


        Category GetCategoryById(int id);

        bool Create(CategoryViewModel model);

        void EditCategory(Category model);
        void EditCategory(CategoryViewModel model);

        bool Delete(int id);

        IEnumerable<SelectListItem> GetCategoriesForBlogView();

        IEnumerable<CategoryViewModel> GetCategoriesVM();
        CategoryViewModel GetCategoryVMById(int id);
    }
}
