using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
        private readonly ContractService _contractService;

        public ContractController(ContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] Contract contract)
        {
            await _contractService.CreateContract(contract);
            return Created("Created", true);
        }
        
        [HttpGet("GetContractsByContractedId/{contractedId}")]
        public async Task<IActionResult> GetContractsByContractedId(string contractedId)
        {
            var contracts = await _contractService.GetContractsByContractedId(contractedId);
            
            if (contracts == null)
            {
                return BadRequest("No contracts found");
            }

            return Ok(contracts);
        }
    }
}