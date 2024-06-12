using EventAPI.Data;
using EventAPI.DTO;
using EventAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly EventDBContext _context;

        public VenueController(EventDBContext context)
        {
            _context = context;
        }
        // POST: Venue
        [HttpPost]
        public async Task<ActionResult<VenueDTO>> PostVenue(VenueDTO venueDTO)
        {

            if (string.IsNullOrEmpty(venueDTO.Name))
            {
                return BadRequest("Venue name is required.");
            }

            var venue = new Venue
            {
                Name = venueDTO.Name,
                LocationBBox = venueDTO.LocationBBox,
                Capacity = venueDTO.Capacity
            };

            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();

            venueDTO.VenueID = venue.VenueID;

            return venueDTO;
        }

        // GET: all Venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            return await _context.Venues.ToListAsync();
        }

        // GET: Venue by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            // find the venue by ID
            var venue = await _context.Venues.FindAsync(id);

            // Return the venue if found
            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }
        // DELETE: Venue by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
