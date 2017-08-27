using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Finance.Web.Infrastructure;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class BudgetController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
