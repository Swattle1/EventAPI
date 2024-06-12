namespace EventAPI.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; }
        public string LocationBBox { get; set; }
        public int Capacity { get; set; }
        public List<Event> Events { get; set; }
    }
}
