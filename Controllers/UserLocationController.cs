using BetsoCare.APIS.Helpers;
using BetsoCare.Core.DTOS;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BetsoCare.Core.Enums;

[ApiController]
[Route("api/user-location")]
[Authorize]
public class UserLocationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly AppNotificationService _notificationService;

    public UserLocationController(
        ApplicationDbContext context,
        AppNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    [HttpPost("check-danger")]
    public async Task<IActionResult> CheckDanger([FromBody] UserLocationDto dto)
    {
        // 🟢 Validate input
        if (dto == null)
            return BadRequest("Location data is required");

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        // 🟡 هات كل الأماكن الخطرة
        var reports = _context.Reports
            .Where(r => r.Status == ReportStatus.Approved)
            .ToList();

        foreach (var r in reports)
        {
            // 🔴 skip لو مفيش location
            if (r.Latitude == null || r.Longitude == null)
                continue;

            var distance = GeoHelper.GetDistance(
                dto.Latitude,
                dto.Longitude,
                r.Latitude.Value,
                r.Longitude.Value
            );

            // 🟣 لو قريب
            if (distance <= 1) // 1 KM
            {
                // 🔥 منع التكرار
                var exists = _context.Notifications.Any(n =>
                    n.UserId == userId &&
                    n.Message.Contains("مكان فيه حالة خطر") &&
                    n.CreatedAt > DateTime.UtcNow.AddMinutes(-30)
                );

                if (!exists)
                {
                    await _notificationService.Create(
                        userId,
                        "⚠️ Warning\r\nYou are near a potentially dangerous area. Please be careful.!",
                        "First Aid Advice for Animal Bites:\r\nGoal: Provide immediate guidance before reaching a hospital.\r\nUser Interface:\r\nQuick tips list for each case:\r\n• Wash the wound with soap and water for 15 minutes\r\n• Disinfect the wound with antiseptic\r\n• Go immediately to a hospital for preventive vaccination"

                    );
                }

                break;
            }
        }

        return Ok("Checked successfully ✅");
    }
}