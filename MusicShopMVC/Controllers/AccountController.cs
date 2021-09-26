using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicShopMVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> Logger;
        private readonly IConfiguration Config;

        private string URLBase
        {
            get
            {
                return Config.GetSection("BaseURL").GetSection("URL").Value;
            }
        }

        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            Logger = logger;
            Config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string stringData = JsonConvert.SerializeObject(model);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(URLBase + "User/authenticate", contentData);
                    var result = response.IsSuccessStatusCode;
                    
                    if (result)
                    {
                        string stringJWT = response.Content.ReadAsStringAsync().Result;
                        var jwt = JsonConvert.DeserializeObject<System.IdentityModel.Tokens.Jwt.JwtPayload>(stringJWT);
                        var jwtString = jwt["tokenString"].ToString();
                        
                        HttpContext.Session.SetString("token", jwtString);
                        HttpContext.Session.SetString("username", jwt["username"].ToString());
                        
                        ViewBag.Message = "User logged in successfully!  " + jwt["username"].ToString();
                    }
                    
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("username");

            return RedirectToAction("Index", "Home");
        }
    }
}
