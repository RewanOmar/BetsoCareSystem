using BetsoCare.Core.DTOS;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetsoCare.APIS
{
   // [Authorize(Roles = "Clinic,Admin")]
    [ApiController]
    [Route("api/dashboard/appointments")]
    public class DashboardAppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardAppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= GET CLINIC APPOINTMENTS =================

        [HttpGet("clinic/{clinicId}")]
        public async Task<IActionResult> GetClinicAppointments(int clinicId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.ClinicId == clinicId)
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

            appointment.Status = "Approved";

            await _context.SaveChangesAsync();

            return Ok("Appointment approved");
        }

        // ================= REJECT =================

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectAppointment(int id, RejectAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound("Appointment not found");

            appointment.Status = "Rejected";
            appointment.RejectReason = dto.Reason;

            await _context.SaveChangesAsync();

            return Ok("Appointment rejected");
        }
    }
}