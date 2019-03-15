using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Filters
{
    public class AuthenticateUser : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string tempSession =
                Convert.ToString(context.HttpContext.Session.GetString("AuthenticationToken"));
            string tempAuthCookie =
                Convert.ToString(context.HttpContext.Request.Cookies["AuthenticationToken"]);

            if (tempSession != null && tempAuthCookie != null)
            {
                if (!tempSession.Equals(tempAuthCookie))
                {
                    ViewResult result = new ViewResult { ViewName = "Login" };
                    context.Result = result;
                }
            }
            else
            {
                ViewResult result = new ViewResult { ViewName = "Login" };
                context.Result = result;
            }
        }
    }
}
