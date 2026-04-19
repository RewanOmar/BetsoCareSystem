using BetsoCare.Core.DTOS;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BetsoCare.APIS.Controllers
{
    //[Authorize(Roles = "Clinic,Admin")]
    [ApiController]
    [Route("api/clinic/dashboard")]
    public class ClinicSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClinicSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("{clinicId}/settings")]
        public async Task<IActionResult> UpdateClinicSettings(int clinicId, JsonElement dto)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
                return NotFound("Clinic not found");

            if (dto.TryGetProperty("bookingPrice", out var bookingPrice))
                clinic.BookingPrice = bookingPrice.GetDecimal();

            if (dto.TryGetProperty("workingDays", out var workingDays))
                clinic.WorkingDays = workingDays.GetString();

            if (dto.TryGetProperty("workingHours", out var workingHours))
                clinic.WorkingHours = workingHours.GetString();

            await _context.SaveChangesAsync();

            return Ok("Clinic settings updated");
        }
    }
}