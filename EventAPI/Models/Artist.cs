namespace EventAPI.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<Event> Events { get; set; }
        public Artist()
        {
            Events = new List<Event>();
        }
    }
}
