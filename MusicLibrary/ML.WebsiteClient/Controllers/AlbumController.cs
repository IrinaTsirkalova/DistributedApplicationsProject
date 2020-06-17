using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ML.Data;
using ML.Models.Entities;
using ML.WebsiteClient.Models;
using Newtonsoft.Json;

namespace ML.WebsiteClient.Controllers
{
    public class AlbumController : Controller
    {
        //Application type - needed to convert data to json type
        private const string JSON_MEDIA_TYPE = "application/json";

        //Needed for the authentication header
        private const string HEADER_AUTHORIZATION = "Authorization";

        //Token API uri/login
        private readonly Uri tokenUri = new Uri("http://localhost:49767/api/login");
        //Artists API
        private readonly Uri artistsUri = new Uri("http://localhost:49767/api/artists");
        private readonly Uri songsUri = new Uri("http://localhost:49767/api/songs");
        private readonly Uri genresUri = new Uri("http://localhost:49767/api/genres");
        private readonly Uri albumsUri = new Uri("http://localhost:49767/api/albums");


        // GET: Album
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync(albumsUri);//conects with API

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();// gets contents data

                var responseData = JsonConvert.DeserializeObject<IEnumerable<AlbumViewModel>>(jsonResponse);//data is being converted

                return View(responseData);// data is being given to the view
            }
        }

        // GET: Album/Details/5

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {

                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync($"{albumsUri}/{id}");
                                   
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<AlbumViewModel>(jsonResponse);


                return View(responseData);
            }
        }

        public ActionResult Search()
        {

            return View();

        }
        [HttpPost]
        public async Task<ActionResult> SearchResult(int id, string albumTitle)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
                id = 1;

                //genreName = "Pop";
                HttpResponseMessage response = await client.GetAsync($"{albumsUri}/{id}/{albumTitle}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<AlbumViewModel>>(jsonResponse);

                return View(responseData);

            }
        }


        // GET: Album/Create
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                //Getting the genres dropdown
                ViewBag.GenreAlbum = await GetGenreDropdownItemAsync();
                //Getting the srtists dropdown
                ViewBag.ArtistAlbum = await GetArtistDropdownItemAsync();
                //Getting the song dropdown
                ViewBag.SongAlbum = await GetSongDropdownItemAsync();
                return View();
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

                HttpResponseMessage artistResponse = await client.GetAsync(artistsUri);


                if (!artistResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();

                }
                string artistJsonResponse = await artistResponse.Content.ReadAsStringAsync();
                var artists = JsonConvert.DeserializeObject<IEnumerable<ArtistViewModel>>(artistJsonResponse);
                return artists.Select(artist => new SelectListItem($"{artist.FName} {artist.LName}", artist.Id.ToString()));
            }
        }
        private async Task<IEnumerable<SelectListItem>> GetSongDropdownItemAsync()
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage songResponse = await client.GetAsync(songsUri);


                if (!songResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();

                }
                string songJsonResponse = await songResponse.Content.ReadAsStringAsync();
                var songs = JsonConvert.DeserializeObject<IEnumerable<SongViewModel>>(songJsonResponse);
                return songs.Select(song => new SelectListItem($"{song.SongTitle}", song.Id.ToString()));
            }
        }

      
        // POST: Album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlbumViewModel album)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetToken();//the method that generate the token
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                    var serializedContent = JsonConvert.SerializeObject(album);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(albumsUri, stringContent);

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

        // GET: Album/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                HttpResponseMessage response = await client.GetAsync($"{ albumsUri}/{ id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<AlbumViewModel>(jsonResponse);
                ViewBag.GenreAlbum = await GetGenreDropdownItemAsync();
                ViewBag.ArtistAlbum = await GetArtistDropdownItemAsync();
                ViewBag.SongAlbum = await GetSongDropdownItemAsync();

                return View(responseData);
            }
        }

        // POST: Album/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AlbumViewModel album)
        {
            album.Id = id;
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetToken();//the method that generate the token
                    client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);

                    var serializedContent = JsonConvert.SerializeObject(album);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);
                    HttpResponseMessage response = await client.PutAsync($"{ albumsUri}/{id}", stringContent);

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

        // GET: Album/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetToken();//the method that generate the token
                client.DefaultRequestHeaders.Add(HEADER_AUTHORIZATION, token);
                HttpResponseMessage response = await client.GetAsync($"{ albumsUri}/{ id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<AlbumViewModel>(jsonResponse);
                return View(responseData);
            }
        }

        // POST: Album/Delete/5
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
                    HttpResponseMessage response = await client.DeleteAsync($"{albumsUri}/{ id}");
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