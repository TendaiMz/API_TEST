namespace API_TEST.Models
{
    public class Image
    {
        public string id { get; set; }
        public string ImageUrl { get; set; }
        public string Landmarksid  { get; set; }
        public Landmarks Landmark { get; set; }

    }
}