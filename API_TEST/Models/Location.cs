using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TEST.Models
{
    public class Location
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Venue Venue { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}