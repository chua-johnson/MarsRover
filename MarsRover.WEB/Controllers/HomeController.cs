using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarsRover.WEB.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MarsRover.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private string replaceBackslash(string date)
        {
            date = date.Replace('/', '-');
            return date;
        }

        [HttpPost]
        // public IActionResult Index(string txtDate)
        public async Task<ActionResult> DisplayImage(string txtDate)
        {
            NasaInfo nasaInfo = null;
            using (var client = new HttpClient())
            {
                txtDate = replaceBackslash(txtDate);
                client.BaseAddress = new Uri("https://localhost:44374/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync($"api/nasa/{txtDate}");
                if (response.IsSuccessStatusCode)
                {
                    nasaInfo = await response.Content.ReadAsAsync<NasaInfo>();
                }
                else
                {
                    ModelState.AddModelError("NasaError", "Something went wrong..unable to process your request");
                }
            }
            return View(nasaInfo);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
