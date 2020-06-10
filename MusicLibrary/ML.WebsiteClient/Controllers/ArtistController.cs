using System;
using System.Collections.Generic;
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
    public class ArtistController : Controller
    {
        private const string JSON_MEDIA_TYPE = "application/json";
        private readonly Uri artistsUri = new Uri("https://localhost:44326/api/artists");



        // GET: Artist
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(artistsUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<ArtistViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Artist/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{artistsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ArtistViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Artist/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArtistViewModel artist)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedContent = JsonConvert.SerializeObject(artist);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(artistsUri, stringContent);

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

        // GET: Artist/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{artistsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ArtistViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // POST: Artist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ArtistViewModel artist)
        {
           artist.Id = id;
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedContent = JsonConvert.SerializeObject(artist);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PutAsync($"{artistsUri}/{id}", stringContent);

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

        // GET: Artist/Delete/5
        [HttpGet]
        public async Task<ActionResult>  Delete(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{artistsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ArtistViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<ActionResult> DeleteConfirmation(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync($"{artistsUri}/{id}");

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