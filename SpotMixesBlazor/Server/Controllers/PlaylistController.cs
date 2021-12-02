using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly PlaylistServices _playlistServices;

        public PlaylistController(PlaylistServices playlistServices)
        {
            _playlistServices = playlistServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] Playlist playlist)
        {
            await _playlistServices.CreatePlaylist(playlist);
            return Created("Created", true);
        }
        
        [HttpDelete("delete/{audioId}/{userId}")]
        public async Task<IActionResult> DeletePlaylist(string audioId, string userId)
        {
            var result =await _playlistServices.DeletePlaylist(audioId, userId);

            if (result)
            {
                return Ok("Eliminado");
            }
            return BadRequest("No se elimino");
        }
        
        [HttpGet("IsPlaylist/{audioId}/{userId}")]
        public async Task<IActionResult> IsReaction(string audioId, string userId)
        {
            var result = await _playlistServices.IsPlaylist(audioId, userId);
            return Ok(result);
        }
    }
}