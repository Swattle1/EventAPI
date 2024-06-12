using EventAPI.Data;
using EventAPI.DTO;
using EventAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly EventDBContext _context;

        public ArtistController(EventDBContext context)
        {
            _context = context;
        }

        // GET: all artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        // POST: a new artist
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist(ArtistDTO artistDTO)
        {
            // Validate the artist and genre included
            if (string.IsNullOrEmpty(artistDTO.Name) || string.IsNullOrEmpty(artistDTO.Genre))
            {
                return BadRequest("The artist name and genre are required.");
            }

            // Map DTO to model
            var artist = new Artist
            {
                Name = artistDTO.Name,
                Genre = artistDTO.Genre
            };

            // Save the artist
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            // Return artist
            return artistDTO;
        }

        // GET: artist by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // GET: all artists by event id
        [HttpGet("Events/{eventId}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsByEvent(int eventId)
        {
            var artists = await _context.Artists
                .Where(a => a.Events.Any(e => e.EventID == eventId))
                .ToListAsync();

            if (artists == null)
            {
                return NotFound();
            }

            return artists;
        }
        // DELETE: Artist by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
