using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialNet.Controllers;

namespace SocialNet.Attributes
{
    public class MyAuthorizeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context.HttpContext.Connection.LocalPort);
            if(!context.HttpContext.Request.Cookies.ContainsKey("Authorized"))
                context.Result = new RedirectToActionResult("Login", "MyAccount", null);
            base.OnActionExecuting(context);
        }
    }
}