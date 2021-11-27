using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : Controller
    {
        private readonly FollowerService _followerService;

        public FollowerController(FollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] Follower follower)
        {
            await _followerService.Follow(follower);
            return Created("Created", true);
        }
        
        [HttpDelete("delete/{followerId}/{followedId}")]
        public async Task<IActionResult> Unfollow(string followerId, string followedId)
        {
            var result = await _followerService.Unfollow(followerId, followedId);

            if (result)
            {
                return Ok("Eliminado");    
            }

            return BadRequest("No se elimino");
        }
        
        [HttpGet("isFollower/{followerId}/{followedId}")]
        public async Task<IActionResult> IsFollower(string followerId, string followedId)
        {
            var result = await _followerService.IsFollower(followerId, followedId);
            return Ok(result);
        }
        
        [HttpGet("countFollowers/{followedId}")]
        public async Task<IActionResult> CountFollowers(string followedId)
        {
            var result = await _followerService.CountFollowers(followedId);
            return Ok(result);
        }
    }
}