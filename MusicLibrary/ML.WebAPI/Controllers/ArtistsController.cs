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
    public class ArtistsController : ControllerBase
    {
        private readonly ArtistService artistService;

        public ArtistsController()
        {
            this.artistService = new ArtistService();
        }

        // GET: api/Artists
        [HttpGet]
        public IEnumerable<ArtistDto> GetAll()
        {
            return artistService.GetAll();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public ActionResult<ArtistDto> GetById([FromRoute]int id)
        {
            var result = artistService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // GET: api/Artists/FName
        [HttpGet("{id?}/{FName}")]
        public ActionResult<ArtistDto> GetAllByFirstName([FromRoute]string FName, int id)
        {
            var result = artistService.GetAllByFirstName(FName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

       /* // GET: api/Artists/LName
        [HttpGet("LName")]
        public ActionResult<ArtistDto> GetAllByLastName([FromRoute]string LName)
        {
            var result = artistService.GetAllByLastName(LName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
      */
        
        // POST: api/Artists
        [HttpPost]
        public IActionResult Create([FromBody] ArtistDto artist)
        {
            if (!artist.IsValid())
            {
                return BadRequest();
            }
            if (artistService.Create(artist))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ArtistDto artist)
        {
            if (!artist.IsValid())
            {
                return BadRequest();
            }
            artist.Id = id;
            if (artistService.Update(artist))
            {
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (artistService.Delete(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
