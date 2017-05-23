namespace API_TEST.Models
{
    public class Image
    {
        public int ID { get; set; }
        public byte ImageData { get; set; }
        public ImageDetails ImageDetails { get; set; }
    }
}