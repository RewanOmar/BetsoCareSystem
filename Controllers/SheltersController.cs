using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/shelters")]
public class SheltersController : ControllerBase
{
    private readonly IShelterRepository _repo;

    public SheltersController(IShelterRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var shelter = await _repo.GetByIdAsync(id);
        if (shelter == null) return NotFound();
        return Ok(shelter);
    }
}