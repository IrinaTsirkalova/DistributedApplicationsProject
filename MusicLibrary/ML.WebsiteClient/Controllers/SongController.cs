using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ML.WebsiteClient.Models;
using Newtonsoft.Json;

namespace ML.WebsiteClient.Controllers
{
    public class SongController : Controller
    {

        private const string JSON_MEDIA_TYPE = "application/json";

        //Needed for the authentication header
        private const string HEADER_AUTHORIZATION = "Authorization";

        //Token API uri/login
        private readonly Uri tokenUri = new Uri("http://localhost:49767/api/login");

        private readonly Uri songsUri = new Uri("http://localhost:49767/api/songs");
        private readonly Uri genresUri = new Uri("http://localhost:49767/api/genres");
        private readonly Uri artistsUri = new Uri("http://localhost:49767/api/artists");

        // GET: Song
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync(songsUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<SongViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Song/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync($"{songsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<SongViewModel>(jsonResponse);

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
        public async Task<ActionResult> SearchResult(int id, string songTitle)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
                id = 1;

                //genreName = "Pop";
                HttpResponseMessage response = await client.GetAsync($"{songsUri}/{id}/{songTitle}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<SongViewModel>>(jsonResponse);

                return View(responseData);

            }
        }

        // GET: Song/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                //Getting the genres dropdown
                ViewBag.GenreSong = await GetGenreDropdownItemAsync();
                //Getting the directors dropdown
                ViewBag.ArtistSong = await GetArtistDropdownItemAsync();
                return View();
            }
        }

        // POST: Song/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SongViewModel song)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetToken();//the method that generate the token
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                    var serializedContent = JsonConvert.SerializeObject(song);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(songsUri, stringContent);

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

        private async Task<IEnumerable<SelectListItem>> GetGenreDropdownItemAsync()
        {
            using (var client = new HttpClient())
            {

                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage genresResponse = await client.GetAsync(genresUri);


                if (!genresResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }
                string jsonResponse = await genresResponse.Content.ReadAsStringAsync();


                var genres = JsonConvert.DeserializeObject<IEnumerable<GenreViewModel>>(jsonResponse);

                return genres.Select(genre => new SelectListItem(genre.GenreName, genre.Id.ToString()));


            }

        }
        private async Task<IEnumerable<SelectListItem>> GetArtistDropdownItemAsync()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage directorResponse = await client.GetAsync(artistsUri);


                if (!directorResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();

                }
                string directorJsonResponse = await directorResponse.Content.ReadAsStringAsync();
                var directors = JsonConvert.DeserializeObject<IEnumerable<ArtistViewModel>>(directorJsonResponse);
                return directors.Select(artist => new SelectListItem($"{artist.FName} {artist.LName}", artist.Id.ToString()));
            }
        }

        // GET: Song/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync($"{ songsUri}/{ id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<SongViewModel>(jsonResponse);
                ViewBag.GenreSong = await GetGenreDropdownItemAsync();
                ViewBag.ArtistSong = await GetArtistDropdownItemAsync();

                return View(responseData);
            }
        }

        // POST: Song/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SongViewModel song)
        {
            song.Id = id;
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetToken();//the method that generate the token
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                    var serializedContent = JsonConvert.SerializeObject(song);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);
                    HttpResponseMessage response = await client.PutAsync($"{ songsUri}/{ id}", stringContent);

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

        // GET: Song/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {

                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync($"{ songsUri}/{ id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<SongViewModel>(jsonResponse);
                return View(responseData);
            }
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComf(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var token = await GetToken();//the method that generate the token
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                    HttpResponseMessage response = await client.DeleteAsync($"{songsUri}/{ id}");
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