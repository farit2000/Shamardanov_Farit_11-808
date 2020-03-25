using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql.TypeHandlers.CompositeHandlers;
using SocialNet.Attributes;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.ViewModels;

namespace SocialNet.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private SocialNetContext _db;
        private IAuthorizationService _authorization;
        
        public PostController(SocialNetContext context, IAuthorizationService service)
        {
            _db = context;
            _authorization = service;
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
                User = _db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name), CreateDate = DateTime.Now});
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(PostEditModel postEditModel, int id)
        {
            var post = _db.Posts.Include(u => u.User)
                .FirstOrDefault(p => p.Id == id);
            if (post.User.UserName == User.Identity.Name)
            {
                post.Name = postEditModel.PostName;
                post.Text = postEditModel.PostText;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = _db.Posts.Include(u => u.User)
                .FirstOrDefault(p => p.Id == id);
            var authResult = await _authorization.AuthorizeAsync(User, post, "PostTimeViewPolicy");
            if (authResult.Succeeded)
            {
                ViewBag.PostName = post.Name;
                ViewBag.PostText = post.Text;
                ViewBag.PostId = post.Id;
                return View();
            }
            else
                return new ForbidResult();
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