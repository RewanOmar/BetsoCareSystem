using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto dto);
        Task<User?> LoginAsync(LoginDto dto);
        Task<User> GetOrCreateGoogleUser(string email, string name);
        Task<User?> VerifyEmailAsync(string token);
        Task<User> GetByIdAsync(int id);
        Task<User> UpdateProfile(int userId, UpdateProfileDto dto);
        Task ChangePassword(int v, ChangePasswordDto dto);
        Task ChangeEmail(int v, ChangeEmailDto dto);
        Task UpdateProfileImage(int userId, string imageUrl);
        Task ForgotPassword(ResetPasswordDto dto);
        Task ResetPassword(ResetPasswordDto dto);
        Task ForgotPassword(ForgotPasswordDto dto);
        Task UpdatePhone(int userId, string phoneNumber);
        Task<User> UpdatePhoneAsync(int userId, UpdatePhoneDto dto);
    }
}
