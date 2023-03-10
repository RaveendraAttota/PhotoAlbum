using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbums();
    }
}
