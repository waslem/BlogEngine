using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.ViewModels;
using BlogEngine.Core.Work;
using BlogEngine.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Admin/Category/
        public ActionResult Index()
        {
            var model = _unitOfWork.CategoryRepository.GetCategories();
            return View(model);
        }

        //
        // GET: /Admin/Category/Create
        public ActionResult Create()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        //
        // POST: /Admin/Category/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            model.CreatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                // if the category name does not exist in the database, create it
                var success = _unitOfWork.CategoryRepository.Create(model);
                if (success)
                {
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                //TODO: implement error for duplicate category name 
                return View(model);
            }
            return View(model);
        }

        //
        // GET: /Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryById(id);

            // use helper modelbinder class to bind to viewmodel
            CategoryViewModel viewModel = ModelBinder.Category(category);

            return View(viewModel);          
        }

        //
        // POST: /Admin/Category/Edit
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.EditCategory(model);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // Get: /Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            // not sure if this is the most elegant solution
            CategoryViewModel viewModel = ModelBinder.Category(_unitOfWork.CategoryRepository.GetCategoryById(id));
            return View(viewModel);
        }

        //
        // POST: /Admin/Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteById(int id)
        {
            if (_unitOfWork.CategoryRepository.Delete(id))
            {
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            // TODO:  a better way to handle delete failures
            return RedirectToAction("Index");
        }
    }
}
