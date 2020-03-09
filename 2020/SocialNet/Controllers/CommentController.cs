using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.ViewModels;
using SocialNet.Views.Comment;

namespace SocialNet.Controllers
{
    public class CommentController : Controller
    {
        private SocialNetContext _db;
        
        public CommentController(SocialNetContext context)
        {
            _db = context;
        }
        // GET
        [Authorize]
        public IActionResult Index(int id)
        {
            var comments = _db.Comments.Include(c => c.Author)
                .Where(c => c.Post.Id == _db.Posts
                    .Include(p => p.Comments)
                    .Include(p => p.User).FirstOrDefault(t => t.Id == id).Id)
                .AsEnumerable()
                .ToList();
            ViewBag.Comments = comments;
            ViewBag.CurrentUser = User.Identity.Name;
            ViewBag.PostId = id;
            return View();
        }

        [HttpPost]
        [Authorize] 
        public IActionResult Create(CommentCreateModel commentCreateModel, int id)
        {
            var e = _db.Comments.Add(new CommentModel
            {
                Author = _db.Users.FirstOrDefault(u => u.Email == User.Identity.Name),
                CreateDate = DateTime.Now,
                Text = commentCreateModel.CommentText,
                Post = _db.Posts.FirstOrDefault(p => p.Id == id)
            });

            _db.SaveChanges();
            return RedirectToAction("Index", "Comment", new {id = id});
        }

        [HttpPost]
        [Authorize] 
        public IActionResult Edit(CommentEditModel commentEditModel, int id)
        {
            var comment = _db.Comments.Include(c => c.Post)
                .FirstOrDefault(c => c.Id == id);
            comment.Text = commentEditModel.CommentText;
            var PostId = comment.Post.Id;
            _db.SaveChanges();
            return RedirectToAction("Index", "Comment", new {id = PostId});
        }
        
        [HttpGet]
        [Authorize] 
        public IActionResult Create(int id)
        {
            ViewBag.PostId = id;
            return View();
        }

        [HttpGet]
        [Authorize] 
        public IActionResult Edit(int id)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.Id == id);
            ViewBag.CommentText = comment.Text;
            ViewBag.CommentId = comment.Id;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var comment = _db.Comments.Include(c => c.Post)
                .FirstOrDefault(c => c.Id == id);
            var PostId = comment.Post.Id;
            _db.Comments.Remove(comment);
            _db.SaveChanges();
            return RedirectToAction("Index", "Comment", new {id = PostId});
        }
    }
}