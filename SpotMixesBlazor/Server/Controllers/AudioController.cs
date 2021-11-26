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

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAudio(string id)
        {
            var deleteResult = await _audioService.DeleteAudio(id);

            if (deleteResult) return Ok("200");

            return BadRequest("400");
        }
        
        [HttpGet("countAudios")]
        public async Task<IActionResult> CountAudios()
        {
            var numberOfAudios = await _audioService.CountAudios();
            return Ok(numberOfAudios);
        }
        
        [HttpGet("all/{audioPerPage:int}/{skip:int}")]
        public async Task<ActionResult> GetAllAudios(int audioPerPage, int skip)
        {
            var audios = await _audioService.GetAllAudios(audioPerPage, skip);
            
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
            var audios = await _audioService.GetMostListenedAudios(audioPerPage, page);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("getAudioById/{audioId}")]
        public async Task<ActionResult> GetMostListenedAudios(string audioId)
        {
            var audios = await _audioService.GetAudioById(audioId);
            
            if (audios == null) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("getAudioByGenre/{textGenre}/{audioPerPage:int}/{page}")]
        public async Task<ActionResult> GetAudioByGenre(int audioPerPage, int page, string textGenre)
        {
            var audios = await _audioService.GetAudioByGenre(audioPerPage, page, textGenre);
            
            if (audios.Equals(null)) return BadRequest("Not found");
            
            return Ok(audios);
        }
        
        [HttpGet("searchAudioByTitle/{textSearch}/{audioPerPage:int}/{page}")]
        public async Task<ActionResult> SearchAudios(int audioPerPage, int page, string textSearch)
        {
            var audios = await _audioService.SearchAudioByTitle(audioPerPage, page, textSearch);
            
            if (audios.Equals(null)) return BadRequest("Not found");
            
            return Ok(audios);
        }
    }
}