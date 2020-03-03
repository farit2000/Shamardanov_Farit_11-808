using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test.Models;
using System.Reflection;
using Test.Validation;

namespace Test.Services
{
    public class PostEntriesStorage : IStorage
    {
        public async Task<Tuple<bool, string>> Save(HttpContext context)
        {
            string filePath = "Files";
            int fileCount = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories).Length;
            fileCount++;
            PostEntry postEntry = new PostEntry();
            postEntry.AuthorName= context.Request.Form["name"];
            postEntry.Post = context.Request.Form["post"];
            var validationResult = Validation.Validation.Validate(postEntry);
            if (context.Request.Form.Files.Count > 0 && validationResult.IsValid)
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
                postEntry.PhotoName = Path.Combine(filePath, fileCount + ".txt");
                File.AppendAllLines(postEntry.PhotoName, new string[] {postEntry.AuthorName, postEntry.Post});
                return new Tuple<bool, string>(true, "Ok");
            }
            return new Tuple<bool, string>(false, validationResult.ErrorMessage);
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
        
        public Tuple<bool, string> Edit(HttpContext context)
        {
            string path = context.Request.Path.Value;
            PostEntry postEntry = new PostEntry();
            postEntry.AuthorName= context.Request.Form["name"];
            postEntry.Post = context.Request.Form["post"];
            var validationResult = Validation.Validation.Validate(postEntry);
            if (validationResult.IsValid)
            {
                File.WriteAllLines(string.Format("Files/{0}.txt", path.Split("/").Last()),
                    new string[] {postEntry.AuthorName, postEntry.Post});
                return new Tuple<bool, string>(true, "Ok");
            }
            return new Tuple<bool, string>(false, validationResult.ErrorMessage);
        }

        public void Remove(HttpContext context)
        {
            string path = context.Request.Path.Value;
            File.Delete(string.Format("Files/{0}.txt", path.Split("/").Last()));
            File.Delete(string.Format("Files/{0}.jpeg", path.Split("/").Last()));
        }
    }
}