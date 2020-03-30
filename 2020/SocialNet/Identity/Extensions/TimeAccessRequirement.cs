using Microsoft.AspNetCore.Authorization;

namespace SocialNet.Identity.Extensions
{
    public class TimeAccessRequirement : IAuthorizationRequirement
    {
        public int AccessTime = 15;
    }
}