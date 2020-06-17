using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML.Business.DTOs;
using ML.Business.Services;

namespace ML.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongService songService;

        public SongsController()
        {
            this.songService = new SongService();
        }

        // GET: api/Songs
        [HttpGet]
        public IEnumerable<SongDto> GetAll()
        {
            return songService.GetAll();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public ActionResult<SongDto> GetById([FromRoute]int id)
        {
            var result = songService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // GET: api/Genres/SongTitle
        [HttpGet("{id?}/{songTitle}")]
        public ActionResult<SongDto> GetAllSongsByTitle([FromRoute]string songTitle, int id)
        {
            var result = songService.GetAllSongsByTitle(songTitle);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
    
        // POST: api/Songs
        [HttpPost]
        public IActionResult Create([FromBody] SongDto song)
        {
            if (!song.IsValid())
            {
                return BadRequest();
            }
            if (songService.Create(song))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] SongDto song)
        {
            if (!song.IsValid())
            {
                return BadRequest();
            }
            song.Id = id;
            if (songService.Update(song))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (songService.Delete(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
