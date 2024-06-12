namespace EventAPI.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public List<Artist> Artists { get; set; }
    }
}
