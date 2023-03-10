namespace PhotoAlbum.Api.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public string PhotoTitle { get; set; }
        public string PhotoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
