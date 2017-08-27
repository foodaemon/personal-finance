using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finance.Core.Services;
using Finance.Common.Dtos;
using Finance.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Finance.Web.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signin(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Signin");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if(await _accountService.IsValidLogin(model.Email, model.Password))
                {
                    var account = await _accountService.GetByEmail(model.Email);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.Id),
                        new Claim(ClaimTypes.Name, account.FirstName + " " + account.LastName),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("GroupId", account.GroupId)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "local", account.Email, account.Role.ToString());
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    } // class
} // namespace
