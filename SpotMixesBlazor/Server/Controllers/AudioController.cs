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

        [HttpGet("quantity")]
        public async Task<IActionResult> GetQuantityAudios()
        {
            var quantity = await _audioService.GetQuantityAudios();
            if (quantity > 0)
            {
                return Ok(quantity);
            }

            return BadRequest("Not found");
        }
        
        [HttpGet("all/{page:int}")]
        public async Task<ActionResult> GetAllAudios(int page)
        {
            var audios = await _audioService.GetAllAudios(21, page);
            if (audios == null)
            {
                return BadRequest("Not found");
            }
            return Ok(audios);
        }
        
        [HttpGet("recent/{page}")]
        public async Task<ActionResult> GetRecentAudios(int page)
        {
            var audios = await _audioService.GetRecentAudios(21, page);
            if (audios == null)
            {
                return BadRequest("Not found");
            }
            return Ok(audios);
        }
        
        [HttpGet("mostListened/{page}")]
        public async Task<ActionResult> GetMostListenedAudios(int page)
        {
            var audios = await _audioService.GetMostListenedAudios(21, page);
            if (audios == null)
            {
                return BadRequest("Not found");
            }
            return Ok(audios);
        }
        
        [HttpGet("search/{textSearch}/{page}")]
        public async Task<ActionResult> SearchAudios(int page, string textSearch)
        {
            var audios = await _audioService.SearchAudios(21, page, textSearch);
            if (audios.Equals(null))
            {
                return BadRequest("Not found");
            }
            return Ok(audios);
        }
    }
}