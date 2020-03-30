using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNet.Attributes;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.Servises;

namespace SocialNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appMode = Environment.GetEnvironmentVariable("AppMode");
            if (appMode == null)
            {
                services.AddTransient<IMessageSender, EmailMessageSender>();
                Environment.SetEnvironmentVariable("AppMode", "Debug");
            }
            else
            {
                if (appMode == "Production")
                    services.AddTransient<IMessageSender, SmsMessageSender>();
                services.AddTransient<IMessageSender, EmailMessageSender>();
                
            }
            
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SocialNetContext>(options =>
                options.UseNpgsql(connection));
            // services.AddScoped<MyAuthorizeActionFilter>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SocialNetContext>();
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}