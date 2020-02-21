using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test.Models;

namespace Test.Services
{
    public class PostEntriesStorage : IStorage
    {
        public async Task Save(HttpContext context)
        {
            string filePath = "Files";
            int fileCount = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).Length;
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
            }
        }

        public List<Dictionary<string, string>> Load(HttpContext context)
        {
            var result = new List<Dictionary<string, string>>();
            DirectoryInfo directoryInfo = new DirectoryInfo("Files");
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file.FullName);
                result.Add(new Dictionary<string, string>
                {
                    {"author", lines[0]},
                    {"post", lines[1]},
                    {"post_id", file.Name.Substring(0, file.Name.Length - 4)},
                    {"img_path", file.DirectoryName + "/" + file.Name.Substring(0, file.Name.Length - 4) + ".jpeg"}
                });
            }
            return result;
        }
        
        public void Edit(HttpContext context)
        {
            string path = context.Request.Path.Value;
            var authorName = context.Request.Form["name"];
            var post = context.Request.Form["post"];
            File.WriteAllLines(string.Format("Files/{0}.txt", path.Split("/").Last()),
                new string[] {authorName, post});
        }

        public void Remove(HttpContext context)
        {
            string path = context.Request.Path.Value;
            File.Delete(string.Format("Files/{0}.txt", path.Split("/").Last()));
            File.Delete(string.Format("Files/{0}.jpeg", path.Split("/").Last()));
        }
    }
}