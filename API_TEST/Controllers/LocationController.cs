using API_TEST.Models;
using FlickrNet;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Http;
using static API_TEST.ViewModel.FourSquare;

namespace API_TEST.Controllers
{
    [RoutePrefix("api/Location")]
    public class LocationController : ApiController
    {
        string FlickerKey = ConfigurationManager.AppSettings["FlickrKey"].ToString();
        string FlickerSecret = ConfigurationManager.AppSettings["FlickerSecret"].ToString();
        string FourSqaureID = ConfigurationManager.AppSettings["FourSqureID"].ToString();
        string FourSqaureSecret = ConfigurationManager.AppSettings["FourSquareSecret"].ToString();
        string url = "https://api.foursquare.com/v2/venues/search?client_id={0}&client_secret={1}&v=20130815";


        // GET: api/Location
        [HttpGet]
        [Route("{query}/{location}")]
        public async Task<IEnumerable<ViewModel.FourSquare.Venue>> Get(string query, string location)
        {

            url = string.Format(url + "&near={2}&query={3}", FourSqaureID, FourSqaureSecret, location, query);

            var locationSearchResult = await AcquireLocationsAsyc(url);
            await SaveLocationAsync(locationSearchResult);
            foreach (var venue in locationSearchResult)
            {
                PhotoCollection photos = await AcquirePhotosAsyc(venue.id);
                await SavePhotosAsync(photos, venue.id);
            }

            return locationSearchResult;

        }
        [HttpGet]
        [Route("{query}/{lat:float}/{lon:float}")]
        public async Task<IEnumerable<ViewModel.FourSquare.Venue>> Get(string query, float lat, float lon)
        {
            url = string.Format(url + "&ll={2},{3}&query={4}", FourSqaureID, FourSqaureSecret, lat, lon, query);
            var locationSearchResult = await AcquireLocationsAsyc(url);
            await SaveLocationAsync(locationSearchResult);
            foreach (var venue in locationSearchResult)
            {
                PhotoCollection photos = await AcquirePhotosAsyc(venue.id);
                await SavePhotosAsync(photos, venue.id);
            }

            return locationSearchResult;


        }
        [HttpGet]
        [Route("Photos/{id}")]
        public async Task<List<string>> GetPhotos(string id)
        {
            var photos = await AcquirePhotosAsyc(id);
            return photos.Select(s => s.MediumUrl).ToList();

        }
        [HttpGet]
        [Route("PhotoInfo/{id}")]
        public PhotoInfo GetPhotoInfo(string id)
        {

            Flickr flickr = new Flickr(FlickerKey, FlickerSecret);
            return flickr.PhotosGetInfo(id);


        }

        async Task SaveLocationAsync(IEnumerable<ViewModel.FourSquare.Venue> theLocations)
        {
            if (theLocations.Any())
            {
                using (var context = new ApplicationDbContext())
                {

                    foreach (var lm in theLocations)
                    {
                        if (context.Landmarks.FirstOrDefault(x => x.id == lm.id) == null)
                        {
                            Landmarks landmark = new Landmarks()
                            {
                                id = lm.id,
                                name = lm.name,


                            };
                            context.Landmarks.Add(landmark);
                            await context.SaveChangesAsync();
                        }

                    }

                }
            }

        }
        async Task SavePhotosAsync(PhotoCollection photos, string venueID)
        {
            if (photos.Any())
            {
                using (var context = new ApplicationDbContext())
                {
                    foreach (var photo in photos)
                    {
                        if (context.Image.FirstOrDefault(p => p.id == photo.PhotoId) == null)
                        {
                            Image image = new Image()
                            {
                                id = photo.PhotoId,
                                ImageUrl = photo.MediumUrl,
                                Landmarksid = venueID
                            };
                            context.Image.Add(image);
                            await context.SaveChangesAsync();
                        }

                    }


                }
            }

        }

        async Task<List<ViewModel.FourSquare.Venue>> AcquireLocationsAsyc(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var stream = await client.GetStreamAsync(url);
                DataContractJsonSerializer serialiser = new DataContractJsonSerializer(typeof(RootObject));
                RootObject rootResponse = (RootObject)serialiser.ReadObject(stream);
                return rootResponse.response.venues;
            }
        }

        async Task<PhotoCollection> AcquirePhotosAsyc(string id)
        {

            Flickr flickr = new Flickr(FlickerKey, FlickerSecret);
            PhotoSearchOptions pSearchOptions = new PhotoSearchOptions();
            pSearchOptions.FoursquareVenueID = id;
            pSearchOptions.MediaType = MediaType.Photos;

            flickr.PhotosSearch(pSearchOptions);
            var flickerPhotos = await Task.Run(() => flickr.PhotosSearch(pSearchOptions));

            return flickerPhotos;
        }




    }
}
