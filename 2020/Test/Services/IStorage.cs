using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Test.Services
{
    public interface IStorage
    {
        Task Save(HttpContext context);
        List<Dictionary<string, string>> Load(HttpContext context);
        void Edit(HttpContext context);
        void Remove(HttpContext context);
    }
}