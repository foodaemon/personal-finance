using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Finance.Web.Infrastructure;
using Finance.Common;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly AppSettings _options;

        public HomeController(IOptions<AppSettings> options)
        {
            _options = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


    } // class
} // namespace
