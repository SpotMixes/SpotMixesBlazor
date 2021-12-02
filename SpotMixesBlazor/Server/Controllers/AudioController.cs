using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;
using SpotMixesBlazor.Shared.ModelsData;

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
        
        [HttpPut]
        public async Task<IActionResult> UpdateAudio([FromBody] Audio audio)
        {
            var resultUpdate = await _audioService.UpdateAudio(audio);
            
            if (resultUpdate) return Ok("200");

            return BadRequest("400");
        }
        
        [HttpPut("updateNumberOfReproductions")]
        public async Task<IActionResult> UpdateNumberOfReproductions([FromBody] AudioNumReproduction audioNumReproduction)
        {
            var audio = await _audioService.GetAudioByIdToUpdate(audioNumReproduction.Id);
            audio.NumReproduction++;
            
            var resultUpdate = await _audioService.UpdateNumberOfReproductions(audio.Id, audio.NumReproduction);
            
            if (resultUpdate) 
                return Ok(audio.NumReproduction);

            return BadRequest("No se pudo actualizar el numero de reproducciones");
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
        
        [HttpGet("getAudioByIdToUpdate/{audioId}")]
        public async Task<ActionResult> SearchAudios(string audioId)
        {
            var audio = await _audioService.GetAudioByIdToUpdate(audioId);

            if (audio != null) { 
                return Ok(audio);
            }

            return BadRequest("Not found");
        }
        
        [HttpGet("GetAllAudioByUserId/{userId}")]
        public async Task<ActionResult> GetAllAudioByUserId(string userId)
        {
            var audios = await _audioService.GetAllAudioByUserId(userId);
            
            return audios.Count > 0 ? Ok(audios) : BadRequest("Not found");
        }
        
        [HttpGet("GetMostListenedAudioByUserId/{userId}")]
        public async Task<ActionResult> GetMostListenedAudioByUserId(string userId)
        {
            var audios = await _audioService.GetMostListenedAudioByUserId(userId);
            
            return audios.Count > 0 ? Ok(audios) : BadRequest("Not found");
        }
        
        [HttpGet("GetRecentAudioByUserId/{userId}")]
        public async Task<ActionResult> GetRecentAudioByUserId(string userId)
        {
            var audios = await _audioService.GetRecentAudioByUserId(userId);
            
            return audios.Count > 0 ? Ok(audios) : BadRequest("Not found");
        }
    }
}