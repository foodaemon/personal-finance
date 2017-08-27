using Finance.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    } // class
} // namespace
