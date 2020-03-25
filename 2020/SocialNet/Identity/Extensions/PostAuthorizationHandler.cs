using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNet.Models;


namespace SocialNet.Identity.Extensions
{
    public class PostAuthorizationHandler : AuthorizationHandler<TimeAccessRequirement, PostModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            TimeAccessRequirement requirement, PostModel resource)
        {
            if ((DateTime.Now - resource.CreateDate).Minutes <= requirement.AccessTime)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}