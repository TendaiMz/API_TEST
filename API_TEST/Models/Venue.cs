using System.Collections.Generic;

namespace API_TEST.Models
{
    public class Venue
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}