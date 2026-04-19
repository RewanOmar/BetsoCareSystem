using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BetsoCare.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

           

            public AuthService(ApplicationDbContext context, EmailService emailService)
            {
                _context = context;
                _emailService = emailService;
            }

            // ✅ REGISTER + SEND EMAIL
            public async Task<User> RegisterAsync(RegisterDto dto)
            {
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    PasswordHash = HashPassword(dto.Password),
                    Role = "User",
                    EmailConfirmed = false,
                    EmailVerificationToken = Guid.NewGuid().ToString()
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

            // 🔥 مهم جدًا: غيري اللينك ده بـ ngrok أو domain
            var link = $"https://life-williams-invalid-cruz.trycloudflare.com/auth/verify-email?token={user.EmailVerificationToken}";

            await _emailService.SendEmailAsync(
                    user.Email,
                    "Verify your email",
                    $"<h2>Welcome {user.Name}</h2>" +
                    $"<p>Click the link below to verify your email:</p>" +
                    $"<a href='{link}'>Verify Email</a>"
                );

                return user;
            }

            private string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            // ✅ LOGIN (مش هيسمح غير لو verified)
            public async Task<User?> LoginAsync(LoginDto dto)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == dto.Email);

                if (user == null)
                    return null;

                if (string.IsNullOrEmpty(user.PasswordHash))
                    throw new Exception("This account was created with Google. Please login with Google.");

                if (!user.EmailConfirmed)
                    throw new Exception("Please verify your email first");

                bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

                if (!valid)
                    return null;

                return user;
            }

            // ✅ VERIFY EMAIL
            public async Task<User?> VerifyEmailAsync(string token)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.EmailVerificationToken == token);

                if (user == null)
                    return null;

                user.EmailConfirmed = true;
                user.EmailVerificationToken = null;

                await _context.SaveChangesAsync();

                return user;
            }

            // ✅ GOOGLE LOGIN
            public async Task<User> GetOrCreateGoogleUser(string email, string name)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user != null)
                    return user;

                user = new User
                {
                    Name = name ?? "Google User",
                    Email = email,
                    IsGoogleAccount = true,
                    EmailConfirmed = true
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }

            // ✅ GET USER
            public async Task<User> GetByIdAsync(int id)
            {
                return await _context.Users.FindAsync(id);
            }

            // ✅ UPDATE PROFILE
            public async Task<User> UpdateProfile(int userId, UpdateProfileDto dto)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                    throw new Exception("User not found");

                if (!string.IsNullOrEmpty(dto.Name))
                    user.Name = dto.Name;

                if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.Phone = dto.PhoneNumber;

            if (dto.DateOfBirth.HasValue)
                    user.DateOfBirth = dto.DateOfBirth;

                if (!string.IsNullOrEmpty(dto.Address))
                    user.Address = dto.Address;

                await _context.SaveChangesAsync();

                return user;
            }

            // ✅ CHANGE PASSWORD
            public async Task ChangePassword(int userId, ChangePasswordDto dto)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                    throw new Exception("User not found");

                if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                    throw new Exception("Current password is incorrect");

                if (BCrypt.Net.BCrypt.Verify(dto.NewPassword, user.PasswordHash))
                    throw new Exception("New password must be different");

                if (dto.NewPassword.Length < 6)
                    throw new Exception("Password must be at least 6 characters");

                user.PasswordHash = HashPassword(dto.NewPassword);

                await _context.SaveChangesAsync();
            }

            // ✅ CHANGE EMAIL (ويبعت verification تاني)
            public async Task ChangeEmail(int userId, ChangeEmailDto dto)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                    throw new Exception("User not found");

                user.Email = dto.NewEmail;
                user.EmailConfirmed = false;
                user.EmailVerificationToken = Guid.NewGuid().ToString();

                await _context.SaveChangesAsync();

            var link = $"https://life-williams-invalid-cruz.trycloudflare.com/api/auth/verify-email?token={user.EmailVerificationToken}";

            await _emailService.SendEmailAsync(
                    user.Email,
                    "Verify your new email",
                    $"Click here: <a href='{link}'>Verify Email</a>"
                );
            }

            // ✅ FORGOT PASSWORD
            public async Task ForgotPassword(ForgotPasswordDto dto)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == dto.Email);

                if (user == null)
                    return;

                var token = new PasswordResetToken
                {
                    UserId = user.Id,
                    Token = Guid.NewGuid().ToString(),
                    ExpireAt = DateTime.UtcNow.AddHours(1),
                    Used = false
                };

                _context.PasswordResetTokens.Add(token);
                await _context.SaveChangesAsync();

            var link = $"https://life-williams-invalid-cruz.trycloudflare.com/api/auth/reset-password?token={token.Token}";

            await _emailService.SendEmailAsync(
                    user.Email,
                    "Reset Password",
                    $"Click here: <a href='{link}'>Reset Password</a>"
                );
            }

            // ✅ RESET PASSWORD
            public async Task ResetPassword(ResetPasswordDto dto)
            {
                var token = await _context.PasswordResetTokens
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Token == dto.Token);

                if (token == null)
                    throw new Exception("Invalid token");

                if (token.Used)
                    throw new Exception("Token already used");

                if (token.ExpireAt < DateTime.UtcNow)
                    throw new Exception("Token expired");

                var user = token.User;

                user.PasswordHash = HashPassword(dto.NewPassword);
                token.Used = true;

                await _context.SaveChangesAsync();
            }

        public async Task UpdateProfileImage(int userId, string imageUrl)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.ImageUrl = imageUrl;

            await _context.SaveChangesAsync();
        }

        public Task ForgotPassword(ResetPasswordDto dto)
        {
            throw new NotImplementedException();
        }
        public async Task<User> UpdatePhone(int userId, string phone)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("Phone number is required");

            user.Phone = phone;

            await _context.SaveChangesAsync();

            return user;
        }

        Task IAuthService.UpdatePhone(int userId, string phone)
        {
            return UpdatePhone(userId, phone);
        }

        public Task<User> UpdatePhoneAsync(int userId, UpdatePhoneDto dto)
        {
            throw new NotImplementedException();
        }
    }
    }