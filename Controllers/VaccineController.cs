using BetsoCare.Core.DTOS;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/vaccine")]
    [Authorize]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _service;

        public VaccineController(IVaccineService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVaccineDto dto)
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _service.CreateAsync(userId, dto);

            return Ok(new { message = "Created successfully" });
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete(CompleteVaccineDto dto)
        {
            await _service.CompleteAsync(dto?.Id);

            return Ok(new { message = "Marked as completed" });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var data = await _service.GetUserVaccines(userId);

            return Ok(data);
        }
    }
}
