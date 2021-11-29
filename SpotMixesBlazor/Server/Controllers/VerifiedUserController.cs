using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiedUserController : Controller
    {
        private readonly VerifiedUserService _verifiedUserService;

        public VerifiedUserController(VerifiedUserService verifiedUserService)
        {
            _verifiedUserService = verifiedUserService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateVerifiedUser([FromBody] VerifiedUser verifiedUser)
        {
            await _verifiedUserService.CreateVerifiedUser(verifiedUser);
            return Created("Created", true);
        }
    }
}