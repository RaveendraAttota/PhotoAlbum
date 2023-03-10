using Moq;
using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Models;
using PhotoAlbum.Api.Services;

namespace PhotoAlbum.Api.Tests
{
    [TestClass]
    public class UserPhotoAlbumServiceTests
    {
        IUserPhotoAlbumService userPhotoAlbumService;

        Mock<IPhotoService> _mockPhotoService;
        Mock<IAlbumService> _mockAlbumService;

        IEnumerable<Photo> photos;
        IEnumerable<Album> albums;

        [TestMethod]
        public async Task GetUserPhotosAndAlbumsShouldReturnDataWhenGivenUserIdContainsData()
        {
            //Arrange 
            int userId = 1;
            var userPhotoAlbums = GetPhotoAlbums().Where(u => u.Id == userId);
            int expectedCount = userPhotoAlbums.Count();

            // Act
            var result = await userPhotoAlbumService.GetUserPhotosAndAlbums(userId);

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
            var userData = result.First();
            Assert.AreEqual(userId, userData.Id);
        }

        [TestMethod]
        public async Task GetUserPhotosAndAlbumsShouldReturnAllDataWhenNoUserIdIsGiven()
        {
            //Arrange 
            var photoAlbums = GetPhotoAlbums();
            int expectedCount = photoAlbums.Count();
            int expectedUserCount = photoAlbums.Select(u => u.Id).Distinct().Count();

            // Act
            var result = await userPhotoAlbumService.GetUserPhotosAndAlbums();

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
            Assert.AreEqual(expectedUserCount, result.Select(u => u.Id).Distinct().Count());
        }

        [TestMethod]
        public async Task GetUserPhotosAndAlbumsShouldReturnNoDataWhenUnavailableUserIdIsGiven()
        {
            //Arrange 
            int userId = 100;
            int expectedCount = 0;

            // Act
            var result = await userPhotoAlbumService.GetUserPhotosAndAlbums(userId);

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestInitialize]
        public void SetUp()
        {
            SetUpData();
            SetUpMocks();
            SetUpExpectations();
            SetUpServicesUnderTest();
        }
        void SetUpData()
        {
            photos = new List<Photo>()
            {
                new Photo
                {
                    AlbumId= 1,
                    Id= 1,
                    Title = "accusamus beatae ad facilis cum similique qui sunt",
                    Url = "https://via.placeholder.com/600/92c952",
                    ThumbnailUrl = "https://via.placeholder.com/150/92c952"
                },
                new Photo
                {
                    AlbumId= 1,
                    Id= 2,
                    Title = "reprehenderit est deserunt velit ipsam",
                    Url = "https://via.placeholder.com/600/771796",
                    ThumbnailUrl = "https://via.placeholder.com/150/771796"
                },
                new Photo
                {
                    AlbumId= 11,
                    Id= 502,
                    Title = "omnis qui sit et",
                    Url = "https://via.placeholder.com/600/74e371",
                    ThumbnailUrl = "https://via.placeholder.com/150/74e371"
                }
            };

            albums = new List<Album>()
            {
                new Album
                {
                    UserId = 1,
                    Id = 1,
                    Title = "quidem molestiae enim"
                },
                new Album
                {
                    UserId = 2,
                    Id = 11,
                    Title = "quam nostrum impedit mollitia quod et dolor"
                }
            };
        }

        void SetUpMocks()
        {
            _mockPhotoService = new Mock<IPhotoService>();
            _mockAlbumService = new Mock<IAlbumService>();
        }

        void SetUpExpectations()
        {
            _mockPhotoService.Setup(m => m.GetPhotos())
                .ReturnsAsync(photos);

            _mockAlbumService.Setup(m => m.GetAlbums())
                .ReturnsAsync(albums);
        }
        void SetUpServicesUnderTest()
        {
            userPhotoAlbumService = new UserPhotoAlbumService(_mockPhotoService.Object, _mockAlbumService.Object);
        }

        private IEnumerable<UserData> GetPhotoAlbums()
        {
            return from photo in photos
                   join album in albums on photo.AlbumId equals album.Id
                   select new UserData
                   {
                       Id = album.UserId,
                       AlbumTitle = album.Title,
                       PhotoTitle = photo.Title,
                       PhotoUrl = photo.Url,
                       ThumbnailUrl = photo.ThumbnailUrl
                   };
        }
    }
}