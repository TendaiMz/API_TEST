using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace API_TEST.ViewModel
{
    public class FourSquare
    {
      
        [DataContract]
        public class Location
        {
            [DataMember(Name = "lat")]
            public double lat { get; set; }
            [DataMember(Name = "lng")]
            public double lng { get; set; }

            [DataMember(Name = "cc")]
            public string cc { get; set; }

            [DataMember(Name = "country")]
            public string country { get; set; }

            [DataMember(Name = "formattedAddress")]
            public List<string> formattedAddress { get; set; }

            [DataMember(Name = "address")]
            public string address { get; set; }

            [DataMember(Name = "city")]
            public string city { get; set; }

            [DataMember(Name = "state")]
            public string state { get; set; }

            [DataMember(Name = "postalCode")]
            public string postalCode { get; set; }

            [DataMember(Name = "crossStreet")]
            public string crossStreet { get; set; }

            [DataMember(Name = "neighborhood")]
            public string neighborhood { get; set; }
        }


        [DataContract]
        public class Venue
        {
            [DataMember(Name = "id")]
            public string id { get; set; }

            [DataMember(Name = "name")]
            public string name { get; set; }

            [DataMember(Name = "location")]
            public Location location { get; set; }
          
        }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "venues")]
            public List<Venue> venues { get; set; }
         
        }

        [DataContract]
        public class RootObject
        {
            [DataMember(Name = "response")]
            public Response response { get; set; }
        }
    }
}