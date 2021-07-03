using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using shangrila.Models;
using shangrila.Services;
using shangrila.Data;
using System.Security.Claims;

namespace shangrila.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
          private readonly AuthService _authService;
          private readonly ShangrilaContext _db;

        public HomeController(ILogger<HomeController> logger, ShangrilaContext db)
        {
            _logger = logger;
            _db=db;
            _authService = new AuthService();
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            model.ServiceHours = _db.ServiceHours.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            HomeViewModel model = new HomeViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            model.ServiceHours = _db.ServiceHours.ToList();
            return View(model);
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            HomeViewModel model = new HomeViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            model.ServiceHours = _db.ServiceHours.ToList();
            return View(model);
        }

        [Route("Menu")]
        public IActionResult Menus()
        {
            HomeViewModel model = new HomeViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            model.ServiceHours = _db.ServiceHours.ToList();
            return View(model);
        }

        [Route("ServiceHour")]
        public IActionResult ServiceHour()
        {
            var model = new HomeViewModel();
            model.Restaurant = _db.Restaurant.FirstOrDefault();
            model.ServiceHours = _db.ServiceHours.ToList();
            return View(model);
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string email, string password, string returnUrl = null)
        {
            // _db.Users.Add(new Models.User(){
            //     DisplayName="Suvash Shah Thakuri",
            //     UserName ="sthakuri",
            //     IsLocked=false,
            //     LastLoggedIn=DateTime.Now,
            //     Password=_authService.HashPassword("iam@Sha2021")
            // });
            // _db.SaveChanges();
            
            var userAccount = _db.Users.FirstOrDefault(x=> x.UserName.ToLower() == email.ToLower());
            if(userAccount!=null)
            {
                var hashedPassword = userAccount.Password;
                var isValid = _authService.VerifyPassword(hashedPassword, password);
                if(isValid)
                {
                    
                    var role="User";
                    if(email=="sthakuri")
                    {
                        role="Admin";
                    }
                    var claims = new List<Claim>
                    {
                        new Claim("email", email),
                        new Claim("displayname", string.Format("{0}", userAccount.DisplayName )),
                        new Claim("role", role)
                    };

                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Admin");
                    }
                }
                else
                {
                    ViewBag.Error="Invalid password.";
                }
            }
            // else if(!userAccount.IsActive)
            // {
            //     ViewBag.Error="Username is not active.";
            // }
            else
            {
                ViewBag.Error="Username does not exist.";                
            }
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
