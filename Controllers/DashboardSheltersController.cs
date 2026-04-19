using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/dashboard/shelters")]
public class DashboardSheltersController : ControllerBase
{
    private readonly IShelterRepository _repo;

    public DashboardSheltersController(IShelterRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShelterDto dto)
    {
        var shelter = new Shelter
        {
            Name = dto.Name,
            Governorate = dto.Governorate,
            Address = dto.Address,
            AnimalType = dto.AnimalType,
            Capacity = dto.Capacity,
            Phone = dto.Phone,
            WorkingHours = dto.WorkingHours,
            Notes = dto.Notes,
            Lat = dto.Lat,
            Lng = dto.Lng
        };

        await _repo.AddAsync(shelter);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ShelterDto dto)
    {
        var shelter = await _repo.GetByIdAsync(id);
        if (shelter == null) return NotFound();

        shelter.Name = dto.Name ?? shelter.Name;
        shelter.Address = dto.Address ?? shelter.Address;
        shelter.Phone = dto.Phone ?? shelter.Phone;
        shelter.AnimalType = dto.AnimalType ?? shelter.AnimalType;
        shelter.Capacity = dto.Capacity ?? shelter.Capacity;
        shelter.WorkingHours = dto.WorkingHours ?? shelter.WorkingHours;
        shelter.Notes = dto.Notes ?? shelter.Notes;
        shelter.Lat = dto.Lat ?? shelter.Lat;
        shelter.Lng = dto.Lng ?? shelter.Lng;

        await _repo.UpdateAsync(shelter);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return Ok();
    }
    
    
}