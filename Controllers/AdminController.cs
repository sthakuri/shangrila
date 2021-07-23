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
    [Authorize(Roles ="Admin")]
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

            model.Sunday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Sunday");
            model.Monday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Monday");
            model.Tuesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Tuesday");
            model.Wednesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Wednesday");
            model.Thursday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Thursday");
            model.Friday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Friday");
            model.Saturday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Saturday");

            model.Announcements = _db.Announcements.Where(x=> x.Visible && x.ValidTo.Date >= DateTime.Today).ToList();
            return View(model);
        }

        public IActionResult BusinessInfo()
        {
            Restaurant model = _db.Restaurant.FirstOrDefault();
            
            return View(model);
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
            return View(model);
        }

        public IActionResult ServiceHours()
        {
            AdminViewModel model = new AdminViewModel();
            model.Sunday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Sunday");
            model.Monday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Monday");
            model.Tuesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Tuesday");
            model.Wednesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Wednesday");
            model.Thursday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Thursday");
            model.Friday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Friday");
            model.Saturday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Saturday");
            
            return View(model);
        }
        
        [HttpPost]        
        public IActionResult ServiceHours(AdminViewModel model)
        {
            
            var sunday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Sunday");
            sunday.IsOpen = model.Sunday.IsOpen;
            sunday.ServiceHours = model.Sunday.ServiceHours;

            var monday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Monday");
            monday.IsOpen = model.Monday.IsOpen;
            monday.ServiceHours = model.Monday.ServiceHours;

            var tuesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Tuesday");
            tuesday.IsOpen = model.Tuesday.IsOpen;
            tuesday.ServiceHours = model.Tuesday.ServiceHours;

            var wednesday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Wednesday");
            wednesday.IsOpen = model.Wednesday.IsOpen;
            wednesday.ServiceHours = model.Wednesday.ServiceHours;

            var thursday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Thursday");
            thursday.IsOpen = model.Thursday.IsOpen;
            thursday.ServiceHours = model.Thursday.ServiceHours;

            var friday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Friday");
            friday.IsOpen = model.Friday.IsOpen;
            friday.ServiceHours = model.Friday.ServiceHours;

            var saturday = _db.ServiceHours.FirstOrDefault(x=> x.WeekDay == "Saturday");
            saturday.IsOpen = model.Saturday.IsOpen;
            saturday.ServiceHours = model.Saturday.ServiceHours;

            _db.SaveChanges();
            ViewBag.Msg="Information saved.";
            
            return View(model);
        }
        
        public IActionResult Announcements()
        {
            AdminViewModel model = new AdminViewModel();
            
            model.Announcements = _db.Announcements.Where(x=> x.Visible && x.ValidTo.Date >= DateTime.Today).ToList();
            model.Announcement = new Announcement(){
                ValidFrom = DateTime.Today,
                ValidTo = DateTime.Today
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Announcements(int id, AdminViewModel model)
        {
            //if id exist: delete operation
            //if model exist: add operation
            
            if(model.Announcement != null && !string.IsNullOrEmpty(model.Announcement.Name))
            {
                model.Announcement.Visible = true;
                _db.Announcements.Add(model.Announcement);
                _db.SaveChanges();
                ViewBag.Msg="Information saved.";
            }
            else if(id>0){
                var item = _db.Announcements.FirstOrDefault(x=> x.ID == id);
                if(item != null)
                {
                    _db.Announcements.Remove(item);
                    _db.SaveChanges();
                    ViewBag.Msg="Information deleted.";
                }
            }
            
            model.Announcements = _db.Announcements.Where(x=> x.Visible && x.ValidTo.Date >= DateTime.Today).ToList();
            model.Announcement = new Announcement(){
                ValidFrom = DateTime.Today,
                ValidTo = DateTime.Today
            };
            return View(model);
        }
                
        public IActionResult Users()
        {
            var model = _db.Users.ToList();
            
            return View(model);
        }
                
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
