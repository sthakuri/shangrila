using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shangrila.Models;
using shangrila.Data;
using Microsoft.AspNetCore.Authorization;
using shangrila.Services;

namespace shangrila.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ShangrilaContext _db;
        private readonly AuthService _authService;
        public AdminController(ILogger<AdminController> logger, ShangrilaContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
            _authService= new AuthService();
        }

        public IActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            return View(model);
        }

        public IActionResult BusinessInfo()
        {
            Restaurant model = _db.Restaurant.FirstOrDefault();
            
            return View("user", model);
        }

        [HttpPost]
        public IActionResult BusinessInfo(Restaurant model)
        {   
            Restaurant item = _db.Restaurant.FirstOrDefault();
            if(item != null)
            {
                item.Name = model.Name;
                item.Address = model.Address;
                item.Phone = model.Phone;
                item.Email = model.Email;
                item.Facebook = model.Facebook;
                item.Instagram = model.Instagram;
                item.Yelp = model.Yelp;
            }
            else
            {
                _db.Restaurant.Add(model);
            }
            _db.SaveChanges();
            ViewBag.Msg="Information saved.";
            return View("User", model);
        }

        
        [Authorize(Roles ="Admin")]
        public IActionResult Users()
        {
            var model = _db.Users.ToList();
            
            return View(model);
        }
        
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Users(string name, string email, string password)
        {
            _db.Users.Add(new Models.User(){
                DisplayName= name,
                UserName = email,
                IsLocked=false,
                LastLoggedIn=DateTime.Now,
                Password=_authService.HashPassword(password)
            });
            _db.SaveChanges();

             var model = _db.Users.ToList();
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
