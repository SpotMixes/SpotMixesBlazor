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
        
        [HttpGet("GetVerifiedUserByUserId/{userId}")]
        public async Task<IActionResult> GetContractsByContractedId(string userId)
        {
            var user = await _verifiedUserService.GetVerifiedUserByUserId(userId);
            
            if (user == null)
            {
                return BadRequest("No contracts found");
            }

            return Ok(user);
        }
    }
}