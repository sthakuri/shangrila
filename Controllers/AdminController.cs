using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shangrila.Models;
using shangrila.Data;

namespace shangrila.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ShangrilaContext _db;
        public AdminController(ILogger<AdminController> logger, ShangrilaContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        public IActionResult Index()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
