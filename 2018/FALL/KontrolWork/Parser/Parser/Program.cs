using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parser
{
    class Comment
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }

    class Answer
    {
        public int Id;
        public int count;
        public Answer(int id, int count)
        {
            this.Id = id;
            this.count = count;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var comments = new List<Comment>();
            comments = GetComments();
            while (comments.Any())
            {
                var result = comments.Distinct().ToList();
                result.ForEach(item => comments.Remove(item));
                WriteAsync(result);
            }
        }

        public static async void WriteAsync(List<Comment> result)
        {
            Console.WriteLine("Start");
            await Task.Run(() => Count(result));
            Console.WriteLine("Stop");
        }

        private static void Count(List<Comment> comments)
        {
            List<Answer> list = new List<Answer>();

            foreach (var e in comments)
            {
                if (e.id % 2 == 0)
                {
                    string result = Regex.Replace(e.body, @"[^A-Za-z]", "");
                    list.Add(new Answer(e.id, result.Length));
                }
            }
            foreach(var e in list)
            {
                Console.WriteLine(e.Id.ToString() + ": " + e.count);
            }
        }

        public static List<Comment> GetComments()
        {
            var comments1 = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText(@"/Users/faritsamardanov/Documents/Shamardanov_Farit_11-808/2018/FALL/KontrolWork/Parser/Parser/EmptyJSONFile.json"));
            return comments1;
        }
    }
}
