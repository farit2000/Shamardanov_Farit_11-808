using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNet.Attributes;
using SocialNet.Data;
using SocialNet.Models;
using SocialNet.ViewModels;

namespace SocialNet.Controllers
{
    public class MyAccountController : Controller
    {
        private SocialNetContext _db;

        public MyAccountController(SocialNetContext context)
        {
            _db = context;
        }
        
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email
                                                                    && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                ModelState.AddModelError("", "Login or/and password is/are incorrect");

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    _db.Users.Add(new User {FirstName = model.FirstName, LastName = model.LastName, 
                        Email = model.Email, Password = model.Password });
                    await _db.SaveChangesAsync();
 
                    await Authenticate(model.Email);
 
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Login or/and password is/are incorrect");
            }
            return View(model);
        }

        private async Task Authenticate(string email)
        {
            HttpContext.Response.Cookies.Append("Authorized", "true");
            HttpContext.Response.Cookies.Append("Email", email);
            if (email == "far@ya.ru")
            {
                HttpContext.Response.Cookies.Append("Admin", "true");
            }
            Console.WriteLine(User.Identity.Name);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Authorized");
            HttpContext.Response.Cookies.Delete("Email");
            if(HttpContext.Request.Cookies.ContainsKey("Admin"))
                HttpContext.Response.Cookies.Delete("Admin");
            return RedirectToAction("Index", "Home");
        }
    }
}