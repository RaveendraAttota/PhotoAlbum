using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Interfaces
{
    public interface IUserPhotoAlbumService
    {
        Task<IEnumerable<UserData>> GetUserPhotosAndAlbums(int? userId = null);
    }
}
