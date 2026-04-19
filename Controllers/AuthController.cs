using BetsoCare.Core.DTOS;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(IAuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // ================= REGISTER =================

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                await _authService.RegisterAsync(dto);

                return Ok(new
                {
                    message = "User registered successfully. Please check your email to verify your account"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= LOGIN =================

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var user = await _authService.LoginAsync(dto);

                if (user == null)
                    return Unauthorized("Invalid email or password");

                if (!user.EmailConfirmed)
                    return Unauthorized("Please verify your email first");

                var token = _tokenService.CreateToken(user);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= VERIFY EMAIL =================

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var user = await _authService.VerifyEmailAsync(token);

            if (user == null)
                return BadRequest("Invalid token");

            return Ok("Email verified successfully");
        }

        // ================= GOOGLE LOGIN =================

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/api/auth/google-response"
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync("Cookies");

            if (!result.Succeeded)
                return BadRequest("Google authentication failed");

            var claims = result.Principal.Claims;

            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (email == null)
                return BadRequest("Email not found");

            var user = await _authService.GetOrCreateGoogleUser(email, name);

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                token,
                user
            });
        }

        // ================= FORGOT PASSWORD =================

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _authService.ForgotPassword(dto);
            return Ok("If email exists, reset link sent");
        }

        // ================= RESET PASSWORD =================

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            await _authService.ResetPassword(dto);
            return Ok("Password reset successfully");
        }

        [Authorize]
        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // ✅ استخراج userId من التوكن
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User not authenticated");

            var userId = int.Parse(userIdClaim.Value);

            // ✅ إنشاء اسم فريد للصورة
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // ✅ تحديد المسار
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            // ✅ حفظ الصورة
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // ✅ إنشاء URL
            var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";

            // ✅ حفظ في الداتا بيز
            await _authService.UpdateProfileImage(userId, imageUrl);

            return Ok(new
            {
                message = "Image uploaded successfully",
                imageUrl = imageUrl
            });
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var user = await _authService.GetByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user); 
        }
        [Authorize]
        [HttpPut("update-phone")]
        public async Task<IActionResult> UpdatePhone(UpdatePhoneDto dto)
        {
            var userIdClaim = User.FindFirst("id");

            if (userIdClaim == null)
                return Unauthorized("User ID not found in token");

            var userId = int.Parse(userIdClaim.Value);

            var user = await _authService.UpdatePhoneAsync(userId, dto);

            return Ok(user);
        }
    }
}