using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogEngine.Web.Api
{
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/category
        public IEnumerable<CategoryViewModel> Get()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategoriesVM();

            return categories;
        }

        // GET api/category/5
        public CategoryViewModel Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryVMById(id);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); 
            }

            return category;
        }

        // POST api/category
        public void Post([FromBody]string value)
        {
        }

        // PUT api/category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/category/5
        public void Delete(int id)
        {
        }
    }
}
