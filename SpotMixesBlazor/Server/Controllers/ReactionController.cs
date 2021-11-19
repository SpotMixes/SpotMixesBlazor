using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : Controller
    {
        private readonly ReactionService _reactionService;

        public ReactionController(ReactionService reactionService)
        {
            _reactionService = reactionService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateReaction([FromBody] Reaction reaction)
        {
            await _reactionService.CreateReaction(reaction);
            return Created("Created", true);
        }
        
        [HttpDelete("delete/{audioId}/{userId}")]
        public async Task<IActionResult> DeleteReaction(string audioId, string userId)
        {
            var result =await _reactionService.DeleteReaction(audioId, userId);

            if (!result)
            {
                return BadRequest("No se elimino");
            }

            return Ok("Eliminado");
        }
        
        [HttpGet("isReaction/{audioId}/{userId}")]
        public async Task<IActionResult> IsReaction(string audioId, string userId)
        {
            var result = await _reactionService.IsReaction(audioId, userId);
            return Ok(result);
        }
        
        [HttpGet("countReactions/{audioId}")]
        public async Task<IActionResult> CountReactions(string audioId)
        {
            var result = await _reactionService.CountReactions(audioId);
            return Ok(result);
        }
    }
}