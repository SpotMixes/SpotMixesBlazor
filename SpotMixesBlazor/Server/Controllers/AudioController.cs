using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : Controller
    {
        private readonly AudioService _audioService;

        public AudioController(AudioService audioService)
        {
            _audioService = audioService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAudio([FromBody] Audio audio)
        {
            await _audioService.CreateAudio(audio);
            return Created("Created", true);
        }
        
        [HttpGet("all/{page}")]
        public async Task<ActionResult> GetAllAudios(int page)
        {
            var audios = await _audioService.GetAllAudios(21, page);
            if (audios == null)
            {
                return BadRequest("Not found");
            }
            return Ok(audios);
        }
    }
}