using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicShopMVC.Models;
using MusicShopMVC.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicShopMVC.Controllers
{
    public class ComposerController : Controller
    {
        private readonly ILogger<ComposerController> Logger;
        private readonly IConfiguration Config;

        private string URLBase
        {
            get
            {
                return Config.GetSection("BaseURL").GetSection("URL").Value;
            }
        }

        public ComposerController(ILogger<ComposerController> logger, IConfiguration config)
        {
            Logger = logger;
            Config = config;
        }

        public async Task<IActionResult> Index()
        {
            ListComposerViewModel model = new ListComposerViewModel();
            var lstComposer = new List<Composer>();

            using (var httpClient = new HttpClient())
            {
                using (var respnse = await httpClient.GetAsync(URLBase + "Composer"))
                {
                    string apiResponse = await respnse.Content.ReadAsStringAsync();
                    lstComposer = JsonConvert.DeserializeObject<List<Composer>>(apiResponse);
                }

                model.Composers = lstComposer;
                return View(model);
            }
        }
    }
}
