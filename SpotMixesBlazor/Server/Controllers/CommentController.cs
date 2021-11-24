using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            await _commentService.CreateComment(comment);
            return Created("Created", true);
        }
        
        [HttpGet("getAllComments/{audioId}")]
        public async Task<IActionResult> GetAllComments(string audioId)
        {
            var comments = await _commentService.GetAllComments(audioId);
            
            if (comments == null) return BadRequest("Not found");

            return Ok(comments);
        }
        
        [HttpGet("countComments/{audioId}")]
        public async Task<IActionResult> CountComments(string audioId)
        {
            var numberOfComments = await _commentService.CountComments(audioId);
            return Ok(numberOfComments);
        }
    }
}