using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Finance.Core.Services;
using Finance.Common.Entities;
using Finance.Web.Infrastructure;
using System.Threading.Tasks;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = new Category();
            return View("Create", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                category.GroupId = GroupId;
                category.CreatedBy = AccountId;

                await _categoryService.Create(category);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log any errors
                return View("Create", category);
            }
            
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await _categoryService.GetById(id);
            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Category category)
        {
            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(categories);
            return View("Index", categories);
        }
    }
}
