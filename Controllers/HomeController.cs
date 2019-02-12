using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Comp4920_SAS.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Comp4920_SAS.Controllers
{
    
    public class HomeController : Controller
    {
         private DataContext db=new DataContext();
        public IActionResult Index()
        {
            var getUser = HttpContext.Session.GetObject<User>("user");
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.std = db.Users.ToList();
           
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            using (DataContext db = new DataContext())
            {
                var userDetails = db.Users.FirstOrDefault(t => t.UserId == user.UserId && t.Password == user.Password);
                if (userDetails == null)
                {
                    ViewBag.HataMesaj = "E-mail veya şifre hatalı.";
                    return RedirectToAction("Login");
                }
                else
                {
                    HttpContext.Session.SetObject("user", userDetails);
                    var getUser = HttpContext.Session.GetObject<User>("user");

                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}