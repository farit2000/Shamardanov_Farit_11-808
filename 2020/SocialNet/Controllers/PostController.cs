using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Npgsql.TypeHandlers.CompositeHandlers;
using SocialNet.Attributes;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.ViewModels;

namespace SocialNet.Controllers
{
    [MyAuthorizeActionFilter]
    public class PostController : Controller
    {
        private SocialNetContext _db;
        
        public PostController(SocialNetContext context)
        {
            _db = context;
        }
        
        // GET
        // public IActionResult Index()
        // {
        //     return View();
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateModel postCreateModel)
        {
            _db.Posts.Add(new PostModel{Name = postCreateModel.PostName, Text = postCreateModel.PostText,
                User = _db.Users.FirstOrDefault(u => u.Email == HttpContext.Request.Cookies["email"]), CreateDate = DateTime.Now});
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(PostEditModel postEditModel, int id)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == id);
            post.Name = postEditModel.PostName;
            post.Text = postEditModel.PostText;
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == id);
            ViewBag.PostName = post.Name;
            ViewBag.PostText = post.Text;
            ViewBag.PostId = post.Id;
            return View();
        }
        
        [HttpGet]
        [AdminActionFilter]
        public IActionResult Delete(int id)
        {
            var comments = _db.Comments.Where(c => c.Post.Id == id);
            foreach (var comment in comments)
            {
                _db.Comments.Remove(comment);
            }
            _db.Posts.Remove(_db.Posts.Single(p => p.Id == id));
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}