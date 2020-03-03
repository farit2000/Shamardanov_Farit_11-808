using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Test.Services
{
    public interface IStorage
    {
        Task<Tuple<bool, string>> Save(HttpContext context);
        List<Dictionary<string, string>> Load(HttpContext context);
        Tuple<bool, string> Edit(HttpContext context);
        void Remove(HttpContext context);
    }
}