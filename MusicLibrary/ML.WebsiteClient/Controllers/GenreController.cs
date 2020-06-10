using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.WebsiteClient.Models;
using Newtonsoft.Json;

namespace ML.WebsiteClient.Controllers
{
    public class GenreController : Controller
    {
        private const string JSON_MEDIA_TYPE = "application/json";
      // CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
        private readonly Uri genresUri = new Uri("https://localhost:44326/api/genres");

        // GET: Genre
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(genresUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<GenreViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Genre/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{genresUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }
               

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<GenreViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Genre/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
       
        public async Task<ActionResult> Create(GenreViewModel genre)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedContent = JsonConvert.SerializeObject(genre);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(genresUri, stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        // GET: Genre/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{genresUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<GenreViewModel>(jsonResponse);
                return View(responseData);
            }
        }

        // POST: Genre/Edit/5
        [HttpPost]
        
        public async Task<ActionResult> Edit(int id, GenreViewModel genre)
        {
            genre.Id = id;
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedContent = JsonConvert.SerializeObject(genre);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PutAsync($"{genresUri}/{id}", stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        // GET: Genre/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{genresUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<GenreViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<ActionResult> DeleteConfirmation(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{genresUri}/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }


          
        }

       
    }
}