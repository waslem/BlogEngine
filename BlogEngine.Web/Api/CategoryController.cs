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
        public HttpResponseMessage Post([FromBody]CategoryViewModel category)
        {
            if (category != null)
            {
                category.CreatedDate = DateTime.Now;

                _unitOfWork.CategoryRepository.Create(category);
                _unitOfWork.Save();
                var response = Request.CreateResponse<CategoryViewModel>(HttpStatusCode.Created, category);

                string uri = Url.Link("DefaultApi", new { id = category.CategoryId });

                response.Headers.Location = new Uri(uri);

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
            
        }

        // PUT api/category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/category/5
        public HttpResponseMessage Delete(int id)
        {
            if (id > 0)
            {
                _unitOfWork.CategoryRepository.Delete(id);
                _unitOfWork.Save();

                return new HttpResponseMessage(HttpStatusCode.OK);

            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
