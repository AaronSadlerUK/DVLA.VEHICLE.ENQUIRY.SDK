using System.Threading.Tasks;
using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLA.TEST.WEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IVehicleDetailsService _vehicleDetailsService;

        public TestController(IVehicleDetailsService vehicleDetailsService)
        {
            _vehicleDetailsService = vehicleDetailsService;
        }

        // GET api/Test/GetVehicleDetails?registration=ZZ99ABC
        // To request the vehicle details by registration
        [HttpGet("GetVehicleDetails")]
        public async Task<ActionResult<string>> GetVehicleDetails(string registration)
        {
            var vehicleDetails = await _vehicleDetailsService.GetVehicleDetails(registration);

            if (vehicleDetails != null)
            {
                return Ok(vehicleDetails);
            }

            return NotFound(registration);
        }
    }
}