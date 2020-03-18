using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNet.Attributes;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.ViewModels;
using SocialNet.Views.Comment;

namespace SocialNet.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private SocialNetContext _db;
        
        public CommentController(SocialNetContext context)
        {
            _db = context;
        }
        // GET
        public IActionResult Index(int id)
        {
            // var comments = _db.Comments.Include(c => c.Author)
            //     .Where(c => c.Post.Id == _db.Posts
            //         .Include(p => p.Comments)
            //         .Include(p => p.User).FirstOrDefault(t => t.Id == id).Id)
            //     .AsEnumerable()
            //     .ToList();
            var comments = _db.Comments.Include(u => u.Author)
                .Where(p => p.Post.Id == id).ToList();
            ViewBag.Comments = comments;
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;
            ViewBag.PostId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CommentCreateModel commentCreateModel, int id)
        {
            var userName = User.Identity.Name;
            var e = _db.Comments.Add(new CommentModel
            {
                
                Author = _db.Users.FirstOrDefault(u => u.UserName == userName),
                CreateDate = DateTime.Now,
                Text = commentCreateModel.CommentText,
                Post = _db.Posts.FirstOrDefault(p => p.Id == id)
            });

            _db.SaveChanges();
            return RedirectToAction("Index", "Comment", new {id = id});
        }

        [HttpPost]
        public IActionResult Edit(CommentEditModel commentEditModel, int id)
        {

            var comment = _db.Comments.Include(c => c.Post)
                .Include(u => u.Author)
                .FirstOrDefault(c => c.Id == id);
            var postId = comment.Post.Id;
            if (User.Identity.Name == comment.Author.UserName && comment.CreateDate.AddMinutes(15) >= DateTime.Now)
            {
                comment.Text = commentEditModel.CommentText;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Comment", new {id = postId});
        }
        
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.PostId = id;
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.Id == id);
            ViewBag.CommentText = comment.Text;
            ViewBag.CommentId = comment.Id;
            return View();
        }

        [HttpGet]
        [AdminActionFilter]
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