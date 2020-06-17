using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Business.DTOs;
using ML.Business.Services;
using ML.Models.Entities;

namespace ML.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumService albumService;

        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }

        // GET: api/Albums
        [HttpGet]
        public IEnumerable<AlbumDto> GetAll()
        {
            return albumService.GetAll();
        }


        // GET: api/Albums/5
        [HttpGet("{id}")]
        public ActionResult<AlbumDto> GetById(int id)
        {
            var result = albumService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Albums/id/title
        [HttpGet("{id?}/{albumTitle}")]
        public ActionResult<GenreDto> GetAlbumTitle(int id,[FromRoute]string albumTitle)
        {
            var result = albumService.GetAllAlbumsByTitle(albumTitle);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // POST: api/Albums
        [HttpPost]
        public IActionResult Create([FromBody] AlbumDto album)
        {
            if (!album.IsValid())
            {
                return BadRequest();
            }
            if (albumService.Create(album))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // PUT: api/Albums/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AlbumDto album)
        {
            if (!album.IsValid())
            {
                return BadRequest();
            }
            album.Id = id;
            if (albumService.Update(album))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (albumService.Delete(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
