using Microsoft.AspNetCore.Mvc;
using PhotoAlbum.Api.Interfaces;
using PhotoAlbum.Api.Models;

namespace PhotoAlbum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserPhotoAlbumService _userPhotoAlbumService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserPhotoAlbumService userPhotoAlbumService, ILogger<UserController> logger)
        {
            _userPhotoAlbumService = userPhotoAlbumService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUserPhotosAndAlbums(int? userId = null)
        {
            try
            {
                var userData = await _userPhotoAlbumService.GetUserPhotosAndAlbums(userId);
                return Ok(userData.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at GetUserPhotoAlbums:  { ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.Please contact the administrator.");
            }
            
        }
    }
}
