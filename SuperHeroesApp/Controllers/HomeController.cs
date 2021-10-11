using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperHeroesApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperHeroesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //HeroDetails ActionResult

        public async Task<IActionResult> Index(int id)
        {
            string apiURL = "https://superheroapi.com/api/";
            string apiToken = "6335531266489350/";
            
            SuperHeroes HeroDetails = new SuperHeroes();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiURL+apiToken+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    HeroDetails = JsonConvert.DeserializeObject<SuperHeroes>(apiResponse);
                }
            }
            return View(HeroDetails);
        }

        //HeroSearch ActionResult
        public ViewResult SearchHero() => View();

        [HttpPost]

        public async Task<ActionResult> SearchHero(string heroName)
        {
            string apiURL = "https://superheroapi.com/api/";
            string apiToken = "6335531266489350/search/";

            SuperHeroes HeroSearch = new SuperHeroes();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiURL + apiToken + heroName))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        HeroSearch = JsonConvert.DeserializeObject<SuperHeroes>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
                return View(HeroSearch);
            }
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
