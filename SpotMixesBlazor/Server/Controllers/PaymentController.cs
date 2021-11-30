using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Shared.ModelsData;

namespace SpotMixesBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreferenceData preferenceData)
        {
            var preference = await _paymentService.GeneratePreference(preferenceData);
            
            if (preference == null)
            {
                return BadRequest("Error creating preference");
            }
            
            return Created("Create", preference);
        }
    }
}