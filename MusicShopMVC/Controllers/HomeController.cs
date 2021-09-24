using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            MusicViewModel musicViewModel = new MusicViewModel();
            List<Artist> lstArtist = new List<Artist>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URLBase + "Artist"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstArtist = JsonConvert.DeserializeObject<List<Artist>>(apiResponse);

                }
            }

            musicViewModel.ArtistList = new SelectList(lstArtist, "Id", "Name");
            return View(musicViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddMusic(MusicViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    Music music = new Music() 
                    { 
                        ArtistId = int.Parse(model.ArtistId), 
                        Name = model.Music.Name 
                    };
                    
                    string stringData = JsonConvert.SerializeObject(music);
                    
                    StringContent contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(URLBase + "Music", contentData);
                    var result = response.IsSuccessStatusCode;
                    
                    if (result)
                        return RedirectToAction("Index");
                    
                    ViewBag.MessageError = response.ReasonPhrase;
                    return View(model);
                }
            }

            return View(model);
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
