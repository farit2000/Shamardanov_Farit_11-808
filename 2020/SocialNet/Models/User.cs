using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialNet.Models
{
    public class User : IdentityUser
    {
        public List<PostModel> Posts { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}