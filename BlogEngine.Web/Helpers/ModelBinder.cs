using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEngine.Web.Helpers
{
    public class ModelBinder
    {
        public static CategoryViewModel Category(Category category)
        {
            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CreatedDate = category.CreatedDate,
                Description = category.Description,
                Name = category.Name
            };

            return model;
        }
    }
}