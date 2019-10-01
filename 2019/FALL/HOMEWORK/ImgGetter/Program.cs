using System;
using System.Net;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Linq;
using AngleSharp.Html.Dom;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImgGetter
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            WebClient client = new WebClient();
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://pixabay.com/";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var images = document.Images.Select(sl => sl.Source).Distinct().ToList();
            var i = 0;
            foreach (var e in images)
            {
                Console.WriteLine(e);
                i++;
            }
            await GetSite("https://pixabay.com/", "c:/qdasf");
        }

        static async System.Threading.Tasks.Task GetSite(string address, string path)
        {
            var parser = new HtmlParser();
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var links = GetLinks(address).Result;
            var src = document.Source.Text;
            //File.WriteAllText(path, src);
            foreach(string e in links)
            {
                var con = Configuration.Default.WithDefaultLoader();
                var doc = await BrowsingContext.New(config).OpenAsync(address);
                var res = document.Source.Text;
                //File.WriteAllText(path, res);
            }
        }

        static async Task<List<string>> GetLinks(string address)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var menuSelector = "a";
            var menuItems = document.QuerySelectorAll(menuSelector).OfType<IHtmlAnchorElement>();
            var links = menuItems.Select(m => ((IHtmlAnchorElement)m).Href).ToList();
            return links;
        }
    }
}
