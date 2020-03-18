using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialNet.Models;

namespace SocialNet.Data
{
    public class SocialNetContext : IdentityDbContext<User>
    {
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        public SocialNetContext(DbContextOptions<SocialNetContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}