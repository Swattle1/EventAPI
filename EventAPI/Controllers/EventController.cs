using EventAPI.Data;
using EventAPI.Models;
using EventAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventDBContext _context;

        public EventController(EventDBContext context)
        {
            _context = context;
        }

        // POST: an event
        [HttpPost]
        public async Task<ActionResult<EventDTO>> PostEvent(EventDTO eventDTO)
        {
            if (string.IsNullOrEmpty(eventDTO.Name) || eventDTO.Date == null)
            {
                return BadRequest("Event name and date are required.");
            }

            // Create event from DTO
            var eventVar = new Event
            {
                Name = eventDTO.Name,
                Date = eventDTO.Date,
                VenueId = eventDTO.VenueId
            };

            _context.Events.Add(eventVar);
            await _context.SaveChangesAsync();
            
            // update dto with event id
            eventDTO.EventID = eventVar.EventID;

            return eventDTO;
        }

        // GET: all events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: events by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            // Retrieve the event by ID
            var eventVar = await _context.Events.FindAsync(id);

            // Return the event
            if (eventVar == null)
            {
                return NotFound();
            }

            return eventVar;
        }

        // POST: add artists to an event
        [HttpPost("{eventID}/Artists")]
        public async Task<ActionResult> AddArtistsToEvent(int eventID, List<int> artistIDs)
        {
            // Find event by ID
            var eventVar = await _context.Events.Include(e => e.Artists).FirstOrDefaultAsync(e => e.EventID == eventID);

            if (eventVar == null)
            {
                return NotFound();
            }

            // Find artists by ID and add to event
            var artists = await _context.Artists.Where(a => artistIDs.Contains(a.ArtistID)).ToListAsync();
            foreach (var artist in artists)
            {
                if (!eventVar.Artists.Contains(artist))
                {
                    eventVar.Artists.Add(artist);
                    artist.Events.Add(eventVar);
                }
            }

            // Save the changes
            await _context.SaveChangesAsync();

            return Ok();
        }
        // DELETE: Event by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var eventVar = await _context.Events.FindAsync(id);
            if (eventVar == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventVar);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
