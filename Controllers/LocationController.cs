using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/user/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAllAsync();

            var result = data.Select(l => new
            {
                l.Id,
                l.Name,
                Type = l.Type.ToString(),
                l.Governorate,
                l.Address,
                l.Phone,
                l.ServiceType,
                Status = l.IsActive ? "true" : "false"
            });

            return Ok(result);
        }
    }
}
