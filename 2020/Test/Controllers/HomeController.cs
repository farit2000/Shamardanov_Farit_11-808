using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using  System.Web;

namespace Test.Controllers
{
    public class HomeController
    {
        //post create form method
        public async Task GetForm(HttpContext context)
        {
            await context.Response.WriteAsync(File.ReadAllText("Views/post_create.html"));
        }
        //method form get fields and add new post
        public async Task AddEntry(HttpContext context)
        {
            string filePath = "Files";
            
            int fileCount = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Length;
            fileCount++;
            
            if (context.Request.Form.Files.Count > 0)
            {
                foreach (var file in context.Request.Form.Files)
                {
                    var path = Path.Combine(filePath, fileCount + System.IO.Path.GetExtension(file.FileName));
                    await using (var inputStream = new FileStream(path, FileMode.Create))
                    {
                        // read file to stream
                        await file.CopyToAsync(inputStream);
                        // stream to byte array
                        byte[] array = new byte[inputStream.Length];
                        inputStream.Seek(0, SeekOrigin.Begin);
                        inputStream.Read(array, 0, array.Length);
                        // get file name
                        var fName = file.FileName;
                    }
                }
                string authorName = context.Request.Form["name"];
                string post = context.Request.Form["post"];
                var txtFileName = Path.Combine(filePath, fileCount + ".txt");
                File.AppendAllLines(txtFileName, new string[] {authorName, post});

                await GetAllPosts(context);
            }
        }
        //general page methon
        public async Task GetAllPosts(HttpContext context)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Files");
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            string page = AllPostsPageGenerator(files);
            await context.Response.WriteAsync(page);
        }
        //post delete method
        public async Task DeletePost(HttpContext context)
        {
            string path = context.Request.Path.Value;
            File.Delete(string.Format("Files/{0}.txt", path.Split("/").Last()));
            File.Delete(string.Format("Files/{0}.jpeg", path.Split("/").Last()));
            await GetAllPosts(context);
        }
        //post edit method
        public async Task EditPost(HttpContext context)
        {
            string path = context.Request.Path.Value;
            if (context.Request.Method == "POST")
            {
                var authorName = context.Request.Form["name"];
                var post = context.Request.Form["post"];
                File.WriteAllLines(string.Format("Files/{0}.txt", path.Split("/").Last()),
                    new string[] {authorName, post});
                await GetAllPosts(context);
            }
            else if (context.Request.Method == "GET")
            {
                // await context.Response.WriteAsync("Ok");
                var lines = File.ReadAllLines(string.Format("Files/{0}.txt", path.Split("/").Last()));
                await context.Response.WriteAsync(string.Format("<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <title>post edit {0}</title>\n</head>\n<body>\n<form action=\"/edit_post/{0}\" method=\"post\"  enctype=\"multipart/form-data\">\n    <label for=\"name\">Author name</label>\n    <input id=\"name\" name=\"name\" value=\"{1}\" />\n    <br/>\n    <label for=\"post\">Posts' text</label>\n    <textarea id=\"post\" name=\"post\">{2}</textarea>\n    <br/>\n    <input type=\"submit\"/>\n</form>\n</body>\n</html>", path.Split("/").Last(), lines[0], lines[1]));
            }
        }
        //all post generator method
        private string AllPostsPageGenerator(FileInfo[] files)
        {
            string result = "";
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
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file.FullName);
                posts.Add(String.Format(@"<div id=""{0}"">
        <h1>{1}</h1>
        <p>{2}</p>
        <img alt=""70px"" src=""{3}"">
    </div>
    <br>
         <form method=""get"" action=""/edit_post/{4}"">
            <button type=""submit"">Edit</button>
        </form>
        <form method=""post"" action=""/remove_post/{4}"">
            <button type=""submit"">Remove</button>
        </form>
    <hr/>", file.Name, lines[0], lines[1], 
                    file.DirectoryName + "/" + file.Name.Substring(0, file.Name.Length - 4) + ".jpeg",
                    file.Name.Substring(0, file.Name.Length - 4)));
            }
            return beforeBody + string.Concat(posts) + afterBody;
        }
    }
}