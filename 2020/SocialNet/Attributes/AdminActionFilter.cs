using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialNet.Attributes
{
    public class AdminActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context.HttpContext.Connection.LocalPort);
            if(!context.HttpContext.Request.Cookies.ContainsKey("Admin"))
                context.Result = new RedirectToActionResult("Login", "MyAccount", null);
            base.OnActionExecuting(context);
        }
    }
}