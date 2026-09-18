using BetsoCare.Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    // 🔥 Create normal vaccine
    [HttpPost]
    public async Task<IActionResult> Create(CreateVaccineDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _service.CreateAsync(userId, dto);

        return Ok(new { message = "Vaccine created" });
    }

    // 📥 Get user vaccines
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(await _service.GetUserVaccines(userId));
    }

    // ✅ Take dose
    [HttpPost("take")]
    public async Task<IActionResult> Take(TakeDoseDto dto)
    {
        await _service.CompleteAsync(dto.Id);

        return Ok(new { message = "Dose taken" });
    }

    // 🔥 CUSTOM (المهم)
    [HttpPost("custom")]
    public async Task<IActionResult> CreateCustom(CreateCustomVaccineDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _service.CreateCustomAsync(userId, dto);

        return Ok(new { message = "Custom vaccine created" });
    }

    // ✏️ Update dose
    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateDoseDto dto)
    {
        await _service.UpdateDose(dto);

        return Ok(new { message = "Updated" });
    }

    // ❌ Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);

        return Ok(new { message = "Deleted" });
    }
}