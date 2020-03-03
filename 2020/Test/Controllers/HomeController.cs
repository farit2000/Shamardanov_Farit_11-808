using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using  System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Test.Services;

namespace Test.Controllers
{
    public class HomeController
    {
        private readonly IStorage _postStorage;
        private readonly IStorage _commentStorage;
        public HomeController(Startup.ServiceResolver serviceAccessor)
        {
            _postStorage = serviceAccessor("post");
            _commentStorage = serviceAccessor("comment");
        }

        //post create form method
        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File.ReadAllText("Views/post_create.html"));
        }
        //method form get fields and add new post
        public async Task CreatePostEntry(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                await context.Response.WriteAsync(File.ReadAllText("Views/post_create.html"));
            }
            else if (context.Request.Method == "POST")
            {
                var saveResult = await _postStorage.Save(context);
                if (saveResult.Item1 is true)
                    await GetAllPosts(context);
                await context.Response.WriteAsync(saveResult.Item2);
            }
        }
        //general page methon
        public async Task GetAllPosts(HttpContext context)
        {
            List<string> posts = new List<string>();
            string beforeBody = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8""
    <title>All posts</title>
</head>
<body>";
            string afterBody = @"<a href=""/create"">Создать пост</a>
</body>
</html>";
            foreach (var post in _postStorage.Load(context))
            {
                posts.Add(String.Format(@"<div id=""{0}"">
        <h1>{1}</h1>
        <p>{2}</p>
        <img alt=""70px"" src=""{3}"">
    </div>
    <br>
         <form method=""get"" action=""/edit_post/{0}"">
            <button type=""submit"">Edit</button>
        </form>
        <form method=""post"" action=""/remove_post/{0}"">
            <button type=""submit"">Remove</button>
        </form>
         <form method=""get"" action=""/create_comment/{0}"">
            <button type=""submit"">Add comment</button>
        </form>
        <form method=""get"" action=""/show_comments/{0}"">
            <button type=""submit"">Show comments</button>
        </form>
    <hr/>", post["post_id"], post["author"], post["post"], post["img_path"]));
            }
            await context.Response.WriteAsync(beforeBody + string.Concat(posts) + afterBody);
        }
        //post delete method
        public async Task RemovePost(HttpContext context)
        {
            _postStorage.Remove(context);
            await GetAllPosts(context);
        }
        //post edit method
        public async Task EditPost(HttpContext context)
        {
            string path = context.Request.Path.Value;
            if (context.Request.Method == "POST")
            {
                var editResult = _postStorage.Edit(context);
                if(editResult.Item1 is true)
                    await GetAllPosts(context);
                await context.Response.WriteAsync(editResult.Item2);
            }
            else if (context.Request.Method == "GET")
            {
                var lines =  File.ReadAllLines(string.Format("Files/{0}.txt", path.Split("/").Last()));
                await context.Response.WriteAsync(string.Format("<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>post edit {0}</title>\n</head>\n<body>\n<form action=\"/edit_post/{0}\" method=\"post\"  enctype=\"multipart/form-data\">\n    <label for=\"name\">Author name</label>\n    <input id=\"name\" name=\"name\" value=\"{1}\" />\n    <br/>\n    <label for=\"post\">Posts' text</label>\n    <textarea id=\"post\" name=\"post\">{2}</textarea>\n    <br/>\n    <input type=\"submit\"/>\n</form>\n</body>\n</html>", path.Split("/").Last(), lines[0], lines[1]));
            }
        }

        public async Task ShowComments(HttpContext context)
        {
            var postId = context.Request.Path.Value.Split("/").Last();
            List<string> body = new List<string>();
            string before =
               string.Format("<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n" +
                             "    <title>All {0} comments</title>\n</head>\n<body>", postId);
            string after = "<br>\n<hr/>\n</body>\n</html>";
            var comments = _commentStorage.Load(context);
            foreach (var comment in comments)
            {
                body.Add(string.Format("<p>All {0}'s post comments</p><div id=\"{0}\">\n" +
                                       "    <h1>{1}</h1>\n" +
                                       "    <p>{2}</p>\n    <br/>\n" +
                                       " <hr/>\n"
                    ,postId, comment["author"], comment["comment"]));
            }
            await context.Response.WriteAsync(before + string.Concat(body) + after);
        }

        public async Task CreateComment(HttpContext context)
        {
            var postId = context.Request.Path.Value.Split("/").Last();
            if (context.Request.Method == "POST")
            {
                var saveResult = await _commentStorage.Save(context);
                if (saveResult.Item1 is true)
                    await GetAllPosts(context);
                else
                    await context.Response.WriteAsync(saveResult.Item2);
            }
            else if (context.Request.Method == "GET")
            {
                string page = string.Format(
                    "<!DOCTYPE html>\n<html>\n<head>\n    <meta charset=\"utf-8\" />\n    " +
                    "<title>Create new comment</title>\n</head>\n<body>\n<form action=\"/create_comment/{0}\" " +
                    "method=\"post\"  enctype=\"multipart/form-data\">\n    " +
                    "<label for=\"name\">Comment author name</label>\n    " +
                    "<input id=\"name\" name=\"name\" />\n    <br/>\n    " +
                    "<label for=\"comment\">Comments' text</label>\n    " +
                    "<textarea id=\"comment\" name=\"comment\"></textarea>\n    " +
                    "<br/>\n    <input type=\"submit\"/>\n</form>\n</body>\n</html>",
                    postId);
                await context.Response.WriteAsync(page);
            }
        }
        //по заданию это организовывать не требуется
        public async Task RemoveComment(HttpContext context)
        {
            await context.Response.WriteAsync("Remove comment");
        }
        //по заданию это организовывать не требуется
        public async Task EditComment(HttpContext context)
        {
            if (context.Request.Method == "POST")
            {
                
            }
            else if (context.Request.Method == "GET")
            {
                await context.Response.WriteAsync("Edit comment");
            }
        }
    }
}