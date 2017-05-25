using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TEST.Models
{
    public class Landmarks
    {

        public string id { get; set; }
        public string name { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}