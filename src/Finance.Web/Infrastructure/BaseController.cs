using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Web.Infrastructure
{
    public abstract class BaseController : Controller
    {
        public string GroupId 
        { 
            get 
            {
                return GetClaimValue("GroupId");
            }
        }

		public string AccountId
		{
			get
			{
				return GetClaimValue(ClaimTypes.NameIdentifier);
			}
		}

        public string Email 
        {
            get 
            {
                return GetClaimValue(ClaimTypes.Email);
            }
        }

        string GetClaimValue(string key) 
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(key).Value;
        }
    } // class
} // namespace
