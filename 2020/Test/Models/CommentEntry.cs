using Test.Services;
using Test.Validation;

namespace Test.Models
{
    public class CommentEntry
    {
        [NotEmpty]
        [FirstSymbolCapital]
        public string AuthorName { get; set; }
        
        [NotEmpty]
        public string CommentText { get; set; }
    }
}