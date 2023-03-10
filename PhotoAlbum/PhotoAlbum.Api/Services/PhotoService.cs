using Newtonsoft.Json;
using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
                
        public PhotoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            var response = await _httpClient.GetAsync(_configuration.GetValue<string>("PhotosUrl"));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Photo>>(await response.Content.ReadAsStringAsync());
        }
    }
}
