using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicShopMVC.Models;
using MusicShopMVC.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Config;

        private string URLBase
        {
            get
            {
                return Config.GetSection("BaseURL").GetSection("URL").Value;
            }
        }

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            Config  = config;
        }

        public async Task<IActionResult> Index()
        {
            ListMusicViewModel model = new ListMusicViewModel();
            var lstMusic = new List<Music>();

            using(var httpClient = new HttpClient())
            {
                using(var respnse = await httpClient.GetAsync(URLBase + "Music"))
                {
                    string apiResponse = await respnse.Content.ReadAsStringAsync();
                    lstMusic = JsonConvert.DeserializeObject<List<Music>>(apiResponse);
                }
            }

            model.Musics = lstMusic;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddMusic()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
