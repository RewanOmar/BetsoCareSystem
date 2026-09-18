using BetsoCare.Core.DTOS;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetsoCare.APIS
{
    [ApiController]
    [Route("api/dashboard/appointments")]

    // ✅ Admin + Doctor
    [Authorize(Roles = "Admin,Doctor")]
    public class DashboardAppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppNotificationService _notificationService;

        public DashboardAppointmentsController(
            ApplicationDbContext context,
            AppNotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // ================= GET MY CLINIC APPOINTMENTS =================

        [HttpGet("my-clinic")]
        public async Task<IActionResult> GetMyClinicAppointments()
        {
            var userId = int.Parse(
     User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
 );

            var doctor = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (doctor == null)
                return Unauthorized();

            var appointments = await _context.Appointments
                .Where(a => a.ClinicId == doctor.ClinicId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return Ok(appointments);
        }

        // ================= APPROVE =================

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound("Appointment not found");

            // ✅ current logged doctor/admin
            var userId = int.Parse(
     User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
 );

            var doctor = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (doctor == null)
                return Unauthorized();

            // ✅ لو Doctor لازم يكون نفس العيادة
            if (doctor.Role == "Doctor" &&
                appointment.ClinicId != doctor.ClinicId)
            {
                return Forbid();
            }

            appointment.Status = "Approved";

            await _context.SaveChangesAsync();

            // 🔥 Notification
            await _notificationService.Create(
                int.Parse(appointment.UserId),
                "✅ Booking Confirmed",
                "Great news! Your booking request has been accepted."
            );

            return Ok("Appointment approved");
        }

        // ================= REJECT =================

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectAppointment(
            int id,
            RejectAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound("Appointment not found");

            // ✅ current logged doctor/admin
            var userId = int.Parse(
     User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
 );

            var doctor = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (doctor == null)
                return Unauthorized();

            // ✅ لو Doctor لازم يكون نفس العيادة
            if (doctor.Role == "Doctor" &&
                appointment.ClinicId != doctor.ClinicId)
            {
                return Forbid();
            }

            appointment.Status = "Rejected";

            appointment.RejectReason = dto.Reason;

            await _context.SaveChangesAsync();

            // 🔥 Notification
            await _notificationService.Create(
                int.Parse(appointment.UserId),
                "❌ Booking Rejected",
                $"We regret to inform you that your booking request has been declined. Reason: {dto.Reason}"
            );

            return Ok("Appointment rejected");
        }
    }
}