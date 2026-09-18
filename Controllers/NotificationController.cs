using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly VaccineReminderJob _job;

    public NotificationController(ApplicationDbContext context, VaccineReminderJob job)
    {
        _context = context;
        _job = job;
    }

    // 🔔 Get all notifications (لليوزر الحالي)
    [HttpGet]
    public IActionResult Get()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized("User not found in token");

        int userId = int.Parse(userIdClaim);

        var notifications = _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();

        return Ok(notifications);
    }

    // 🔴 عدد الـ unread
    [HttpGet("unread-count")]
    public IActionResult GetUnreadCount()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var count = _context.Notifications
            .Count(n => n.UserId == userId && n.IsRead == false);

        return Ok(count);
    }

    // ✅ Mark notification as read
    [HttpPost("mark-as-read/{id}")]
    public IActionResult MarkAsRead(int id)
    {
        var notification = _context.Notifications.FirstOrDefault(n => n.Id == id);

        if (notification == null)
            return NotFound();

        notification.IsRead = true;
        _context.SaveChanges();

        return Ok("Marked as read");
    }

   
}