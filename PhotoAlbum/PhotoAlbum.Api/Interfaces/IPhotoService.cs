using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetPhotos();
    }
}
