using BetsoCare.Core.Entities;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RatingController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ 1. اليوزر يعمل تقييم
    [Authorize]
    [HttpPost]
    public IActionResult Rate(int value)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized("User not found");

        var userId = int.Parse(userIdClaim.Value);

        if (value < 1 || value > 5)
            return BadRequest("Invalid rating");

        var lastRating = _context.Ratings
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefault();

        if (lastRating != null)
        {
            var hoursPassed = (DateTime.UtcNow - lastRating.CreatedAt).TotalHours;

            if (hoursPassed < 24)
            {
                return BadRequest("You can rate again after 24 hours");
            }
        }

        var rating = new Rating
        {
            Value = value,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Ratings.Add(rating);
        _context.SaveChanges();

        return Ok("Rating submitted successfully");
    }
    // ✅ 2. الأدمن يشوف كل التقييمات
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public IActionResult GetAllRatings()
    {
        var ratings = _context.Ratings
            .Select(r => new
            {
                r.Id,
                r.Value,
                r.UserId,
                r.CreatedAt
            })
            .ToList();

        return Ok(ratings);
    }

    // ✅ 3. متوسط التقييم (للداشبورد)
    [Authorize(Roles = "Admin")]
    [HttpGet("average")]
    public IActionResult GetAverage()
    {
        if (!_context.Ratings.Any())
            return Ok(0);

        var avg = _context.Ratings.Average(r => r.Value);

        return Ok(avg);
    }

    // ✅ 4. عدد التقييمات
    [Authorize(Roles = "Admin")]
    [HttpGet("count")]
    public IActionResult GetCount()
    {
        var count = _context.Ratings.Count();
        return Ok(count);
    }
}