using Newtonsoft.Json;
using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public AlbumService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            var response = await _httpClient.GetAsync(_configuration.GetValue<string>("AlbumsUrl"));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Album>>(await response.Content.ReadAsStringAsync());            
        }
    }
}
