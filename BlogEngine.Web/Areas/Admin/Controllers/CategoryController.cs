using BlogEngine.Core.Infrastructure;
using BlogEngine.Core.Models;
using BlogEngine.Core.ViewModels;
using BlogEngine.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        //
        // GET: /Admin/Category/
        public ActionResult Index()
        {
            var model = _categoryRepo.GetCategories();

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
                if (!_categoryRepo.Create(model))
                {
                    return RedirectToAction("Index");
                }
                //TODO: implement error for duplicate category name 
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        //
        // GET: /Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);

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
                _categoryRepo.EditCategory(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // Get: /Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            // not sure if this is the most elegant solution
            CategoryViewModel viewModel = ModelBinder.Category(_categoryRepo.GetCategoryById(id));

            return View(viewModel);
        }

        //
        // POST: /Admin/Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteById(int id)
        {
            if (_categoryRepo.Delete(id))
            {
                return RedirectToAction("Index");
            }
            // TODO:  a better way to handle delete failures
            return RedirectToAction("Index");
        }
    }
}
