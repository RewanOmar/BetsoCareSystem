using BetsoCare.Core.DTOS;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;

    public UserController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _authService.GetByIdAsync(int.Parse(userId));

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Name,
            user.Email,
            user.Phone,
            user.DateOfBirth,
            user.Address
        });
    }
    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _authService.UpdateProfile(int.Parse(userId), dto);

        return Ok(new
        {
            message = "Profile updated successfully",
            user.Name,
            user.Phone,
            user.DateOfBirth,
            user.Address,
            image = user.ImageUrl
        });
    }
    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _authService.ChangePassword(int.Parse(userId), dto);

        return Ok("Password changed successfully");
    }
    [Authorize]
    [HttpPut("change-email")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        await _authService.ChangeEmail(int.Parse(userId), dto);

        return Ok(new
        {
            message = "Email updated successfully. Please verify your new email."
        });
    }

    [Authorize]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine("wwwroot/images", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var imageUrl = $"/images/{fileName}";

        await _authService.UpdateProfileImage(userId, imageUrl);

        return Ok(new
        {
            message = "Uploaded successfully",
            imageUrl
        });

    }
    
    
}