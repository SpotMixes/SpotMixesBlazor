using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

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

        #region GetAudio
        [HttpGet("all/{audioPerPage:int}/{page:int}")]
        public async Task<ActionResult> GetAllAudios(int audioPerPage, int page)
        {
            var audios = await _audioService.GetAllAudios(audioPerPage, page);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("recent/{audioPerPage:int}/{page}")]
        public async Task<ActionResult> GetRecentAudios(int audioPerPage, int page)
        {
            var audios = await _audioService.GetRecentAudios(audioPerPage, page);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("mostListened/{audioPerPage:int}/{page}")]
        public async Task<ActionResult> GetMostListenedAudios(int audioPerPage, int page)
        {
            var audios = await _audioService.GetMostListenedAudios(21, page);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("GetAudioById/{audioId}")]
        public async Task<ActionResult> GetMostListenedAudios(string audioId)
        {
            var audios = await _audioService.GetAudioById(audioId);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("search/{textSearch}/{audioPerPage:int}/{page}")]
        public async Task<ActionResult> SearchAudios(int audioPerPage, int page, string textSearch)
        {
            var audios = await _audioService.SearchAudios(audioPerPage, page, textSearch);
            
            if (audios.Equals(null)) return BadRequest("Not found");
            
            return Ok(audios);
        }
        #endregion
    }
}