using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Validation;

namespace Test.Models
{
    public class PostEntry
    {
        [NotEmpty]
        [FirstSymbolCapital]
        public string AuthorName { get; set; }
        
        [NotEmpty]
        public string Post { get; set; }
        
        public string PhotoName { get; set; }
    }
}