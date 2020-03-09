using System;

namespace SocialNet.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        
        public PostModel Post { get; set; }
    }
}