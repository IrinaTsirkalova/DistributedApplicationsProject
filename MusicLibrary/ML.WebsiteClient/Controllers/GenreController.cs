using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ML.Models.Entities;
using ML.WebsiteClient.Models;
using Newtonsoft.Json;

namespace ML.WebsiteClient.Controllers
{
    public class GenreController : Controller
    {
        private const string JSON_MEDIA_TYPE = "application/json";

        private const string HEADER_AUTHORIZATION = "Authorization";

        private readonly Uri tokenUri = new Uri("http://localhost:49767/api/login");
        private readonly Uri genresUri = new Uri("http://localhost:49767/api/genres");


        // GET: Genre
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

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
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

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
        //GET:Genre/Search

        [HttpGet]
        public ActionResult Search()
        {
           
            return View();
           
        }
        [HttpPost]
        public async Task<ActionResult> SearchResult(int id,string genreName)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
                id = 1;
               
                //genreName = "Pop";
                HttpResponseMessage response = await client.GetAsync($"{genresUri}/{id}/{genreName}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<GenreViewModel>>(jsonResponse);

                 return View(responseData);
                
            }
        }

        

        // GET: Genre/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
                return View();
            }
        }

        // POST: Genre/Create
        [HttpPost]
       
        public async Task<ActionResult> Create(GenreViewModel genre)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetToken();
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

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
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

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
                    var token = await GetToken();
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
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
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
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
                    var token = await GetToken();
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
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

        private async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                var serializedContent = JsonConvert.SerializeObject(new { Username = "admin", Password = "admin" });
                var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                HttpResponseMessage response = await client.PostAsync(tokenUri, stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return $"Bearer {await response.Content.ReadAsStringAsync()}";
            }
        }
    }
}