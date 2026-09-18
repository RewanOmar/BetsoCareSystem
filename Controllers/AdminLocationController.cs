using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/admin/locations")]
    [Authorize(Roles = "Admin")]
    public class AdminLocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public AdminLocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Location location)
        {
            await _service.AddAsync(location);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLocationDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _service.ToggleStatusAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
