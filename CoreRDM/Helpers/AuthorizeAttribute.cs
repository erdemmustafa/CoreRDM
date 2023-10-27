using CoreRDM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CoreRDM.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string message = "";
            var user = (Users)context.HttpContext.Items["User"];
            var expirydate = context.HttpContext.Items["expiry"];
            if (user == null )
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
