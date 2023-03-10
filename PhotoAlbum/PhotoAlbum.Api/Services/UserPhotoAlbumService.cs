using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Services
{
    public class UserPhotoAlbumService : IUserPhotoAlbumService
    {
        private readonly IPhotoService _photoService;
        private readonly IAlbumService _albumService;

        public UserPhotoAlbumService(IPhotoService photoService, IAlbumService albumService)
        {
            _photoService = photoService;
            _albumService = albumService;
        }

        public async Task<IEnumerable<UserData>> GetUserPhotosAndAlbums(int? userId = null)
        {
            var photos = await _photoService.GetPhotos();
            var albums = await _albumService.GetAlbums();

            var result = from photo in photos
                          join album in albums on photo.AlbumId equals album.Id
                          select new UserData
                          {
                              Id = album.UserId,
                              AlbumTitle = album.Title,
                              PhotoTitle = photo.Title,
                              PhotoUrl = photo.Url,
                              ThumbnailUrl = photo.ThumbnailUrl
                          };

            if (userId.HasValue)
            {
                result = result.Where(r => r.Id == userId);
            }

            return result;
        }
    }
}
