using Finance.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Finance.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("500");
        }

        public IActionResult Forbidden()
        {
            return View("403");
        }

        public IActionResult PageNotFound()
        {
            return View("404");
        }
    }
}
