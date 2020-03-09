using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNet.Models;

namespace SocialNet.Data
{
    public class SocialNetContext : DbContext
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        public SocialNetContext(DbContextOptions<SocialNetContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}