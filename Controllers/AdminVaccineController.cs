using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin/vaccine")]
[Authorize(Roles = "Admin")]
public class AdminVaccineController : ControllerBase
{
    private readonly IVaccineService _service;

    public AdminVaccineController(IVaccineService service)
    {
        _service = service;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        return Ok(await _service.GetAllUsersVaccines());
    }

    [HttpGet("stats")]
    public async Task<IActionResult> Stats()
    {
        return Ok(await _service.GetStats());
    }
}