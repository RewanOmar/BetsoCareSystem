
using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetsoCare.APIS.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= BOOK APPOINTMENT =================

        [HttpPost]
        public async Task<IActionResult> BookAppointment(CreateAppointmentDto dto)
        {
            var clinic = await _context.Clinics.FindAsync(dto.ClinicId);

            if (clinic == null)
                return NotFound("Clinic not found");

            var exists = await _context.Appointments.AnyAsync(a =>
                a.ClinicId == dto.ClinicId &&
                a.Date == dto.Date &&
                a.Time == dto.Time);

            if (exists)
                return BadRequest("This time slot is already booked");

            var appointment = new Appointment
            {
                ClinicId = dto.ClinicId,
                CustomerName = dto.CustomerName,
                Phone = dto.Phone,
                Date = dto.Date,
                Time = dto.Time,
                Price = clinic.BookingPrice
            };

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return Ok(appointment);
        }

        [HttpGet("{clinicId}/available-times")]
        public async Task<IActionResult> GetAvailableTimes(int clinicId, DateTime date)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
                return NotFound();

            var start = TimeSpan.Parse(clinic.WorkingHours.Split('-')[0]);
            var end = TimeSpan.Parse(clinic.WorkingHours.Split('-')[1]);

            var bookedTimes = await _context.Appointments
                .Where(a => a.ClinicId == clinicId && a.Date == date)
                .Select(a => a.Time)
                .ToListAsync();

            var availableTimes = new List<TimeSpan>();

            for (var time = start; time < end; time += TimeSpan.FromHours(1))
            {
                if (!bookedTimes.Contains(time))
                    availableTimes.Add(time);
            }

            return Ok(availableTimes);
        }
        [HttpGet("user/{phone}")]
        public async Task<IActionResult> GetUserAppointments(string phone)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Clinic)
                .Where(a => a.Phone == phone)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new
                {
                    a.Id,
                    ClinicName = a.Clinic.Name,
                    a.Date,
                    a.Time,
                    a.Price,
                    a.Status,
                    a.RejectReason
                })
                .ToListAsync();

            return Ok(appointments);
        }
    }
}