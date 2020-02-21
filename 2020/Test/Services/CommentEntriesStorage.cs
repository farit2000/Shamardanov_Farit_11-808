using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test.Models;

namespace Test.Services
{
    public class CommentEntriesStorage : IStorage
    {
        public async Task Save(HttpContext context)
        {
            string filePath = "Files";
            var postId = context.Request.Path.Value.Split("/").Last();
            int fileCount = Directory.GetFiles(filePath, string.Format("{0}.comment*",postId),
                SearchOption.AllDirectories).Length;
            string commentAuthorName = context.Request.Form["name"];
            string commentText = context.Request.Form["comment"];
            var txtFileName = Path.Combine(filePath, postId + ".comment" + fileCount);
            File.AppendAllLines(txtFileName, new string[] {commentAuthorName, commentText});
        }

        public List<Dictionary<string, string>> Load(HttpContext context)
        {
            var postId = context.Request.Path.Value.Split("/").Last();
            var result = new List<Dictionary<string, string>>();
            DirectoryInfo directoryInfo = new DirectoryInfo("Files");
            FileInfo[] files = directoryInfo.GetFiles(string.Format("{0}.comment*", postId));
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file.FullName);
                result.Add(new Dictionary<string, string>
                {
                    {"author", lines[0]},
                    {"comment", lines[1]},
                });
            }
            return result;
        }
        //по заданию это организовывать не требуется
        public void Edit(HttpContext context)
        {
            throw new NotImplementedException();
        }
        //по заданию это организовывать не требуется
        public void Remove(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}