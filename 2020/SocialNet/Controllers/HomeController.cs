using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocialNet.Data;
using SocialNet.Models;
using Microsoft.AspNetCore.Authorization;
using SocialNet.Attributes;

namespace SocialNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SocialNetContext _db;

        public HomeController(ILogger<HomeController> logger, SocialNetContext context)
        {
            _logger = logger;
            _db = context;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var posts = _db.Posts.Include(user => user.User).AsEnumerable().ToList();
            ViewBag.Posts = posts;
            ViewBag.CurrentUser = User.Identity.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}