using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finance.Core.Services;
using Finance.Web.Infrastructure;

namespace Finance.Web.Controllers.Api
{
    [Route("api/v1/categories")]
    [Authorize]
    public class CategoryApiController: BaseController
    {
        readonly ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Json(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id) 
        {
            var category = _categoryService.GetById(id);
            return Json(category);
        }
    }
}
