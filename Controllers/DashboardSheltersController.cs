using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard/shelters")]
[Authorize(Roles = "Admin")]
public class DashboardSheltersController : ControllerBase
{
    private readonly IShelterRepository _repo;
    private readonly ApplicationDbContext _context;
    private readonly AppNotificationService _notificationService;

    public DashboardSheltersController(
        IShelterRepository repo,
        ApplicationDbContext context,
        AppNotificationService notificationService)
    {
        _repo = repo;
        _context = context;
        _notificationService = notificationService;
    }

    // ================= ADD =================
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

        // 🔔 Notification لكل المستخدمين
        var userIds = _context.Users.Select(u => u.Id).ToList();

        await _notificationService.CreateForAll(
            userIds,
            "🏠 New Shelter Available",
            $"A new shelter \"{shelter.Name}\" has been added. Check it out for more details."
        );

        return Ok(shelter);
    }

    // ================= UPDATE =================
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

        // 🔔 Notification
        var userIds = _context.Users.Select(u => u.Id).ToList();

        await _notificationService.CreateForAll(
            userIds,
            "✏️ Shelter Updated",
            $"Shelter \"{shelter.Name}\" has been updated. Check the latest information."
        );

        return Ok(shelter);
    }

    // ================= DELETE =================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return Ok("Shelter deleted successfully");
    }
}