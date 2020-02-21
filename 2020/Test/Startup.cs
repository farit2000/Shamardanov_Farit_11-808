using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Controllers;
using Test.Services;

namespace Test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public delegate IStorage ServiceResolver(string key);
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<PostEntriesStorage>();
            services.AddTransient<CommentEntriesStorage>();
            
            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "post":
                        return serviceProvider.GetService<PostEntriesStorage>();
                    case "comment":
                        return serviceProvider.GetService<CommentEntriesStorage>();
                    default:
                        throw new KeyNotFoundException(); // or maybe return null, up to you
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceResolver serviceResolver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapGet("/create", new HomeController(serviceResolver).CreatePostEntry);
                endpoints.MapGet("/", new HomeController(serviceResolver).GetAllPosts);
                endpoints.MapPost("/home/create_post", new HomeController(serviceResolver).CreatePostEntry);
                endpoints.MapPost("/edit_post/{.}", new HomeController(serviceResolver).EditPost);
                endpoints.MapGet("/edit_post/{.}", new HomeController(serviceResolver).EditPost);
                endpoints.MapPost("/remove_post/{.}", new HomeController(serviceResolver).RemovePost);
                endpoints.MapGet("/show_comments/{.}", new HomeController(serviceResolver).ShowComments);
                endpoints.MapGet("/create_comment/{.}", new HomeController(serviceResolver).CreateComment);
                endpoints.MapPost("/create_comment/{.}", new HomeController(serviceResolver).CreateComment);
                endpoints.MapGet("/edit_comment/{.}", new HomeController(serviceResolver).EditComment);
                endpoints.MapPost("/edit_comment/{.}", new HomeController(serviceResolver).EditComment);
            });
        }
    }
}