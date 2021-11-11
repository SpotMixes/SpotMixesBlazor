using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : Controller
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            await _sessionService.CreateSession(session);
            return Created("Created", true);
        }
        
    }
}