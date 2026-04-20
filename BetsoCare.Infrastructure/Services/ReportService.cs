using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Enums;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace BetsoCare.Infrastructure.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // =============================
        // 🟢 GET USER ID
        // =============================
        private string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        // =============================
        // 🟢 CREATE REPORTS
        // =============================

        public async Task CreateBite(BiteReportDto dto)
        {
            var report = new Report
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Governorate = dto.Governorate,
                District = dto.District,
                Type = ReportType.Bite,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

                UserId = GetUserId(),
                Status = ReportStatus.Pending,

                BiteReport = new BiteReport
                {
                    AnimalType = dto.AnimalType,
                    ExposureType = dto.ExposureType,
                    Severity = dto.Severity,
                    ExposureDateTime = dto.ExposureDateTime,
                    LocationCity = dto.LocationCity,
                    BodyLocations = JsonSerializer.Serialize(dto.BodyLocations),
                    InitialActions = JsonSerializer.Serialize(dto.InitialActions),
                    OtherBodyLocation = dto.OtherBodyLocation,
                    OtherAction = dto.OtherAction
                }
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task CreateDangerous(DangerousAnimalDto dto)
        {
            var report = new Report
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Governorate = dto.Governorate,
                District = dto.District,
                Type = ReportType.DangerousAnimal,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,

                UserId = GetUserId(),
                Status = ReportStatus.Pending,

                DangerousAnimalReport = new DangerousAnimalReport
                {
                    AnimalType = dto.AnimalType,
                    ReportDate = dto.ReportDate,
                    LocationCity = dto.LocationCity,
                    SelectedSymptoms = JsonSerializer.Serialize(dto.SelectedSymptoms ?? new List<string>()),
                    OtherSymptom = dto.OtherSymptom
                }
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task CreateComplaint(ComplaintDto dto)
        {
            var report = new Report
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Governorate = dto.Governorate,
                District = dto.District,
                Type = ReportType.Complaint,

                UserId = GetUserId(),
                Status = ReportStatus.Pending,

                ComplaintReport = new ComplaintReport
                {
                    Email = dto.Email,
                    Subject = dto.Subject,
                    Message = dto.Message,
                    Urgency = dto.Urgency
                }
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 ADMIN FLOW (ONE METHOD 🔥)
        // =============================
        public async Task UpdateReport(int id, ReportStatus status, string? adminResponse)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
                throw new Exception("Report not found");

            report.Status = status;
            report.AdminResponse = adminResponse;

            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 GET REPORT BY ID (ADMIN VIEW)
        // =============================
        public async Task<object> GetReportById(int id)
        {
            var report = await _context.Reports
                .Include(r => r.BiteReport)
                .Include(r => r.DangerousAnimalReport)
                .Include(r => r.ComplaintReport)
                .FirstOrDefaultAsync(r => r.Id == id);

            object details = null;

            if (report.Type == ReportType.Bite)
                details = report.BiteReport;
            else if (report.Type == ReportType.DangerousAnimal)
                details = report.DangerousAnimalReport;
            else if (report.Type == ReportType.Complaint)
                details = report.ComplaintReport;

            return new
            {
                report.Id,
                report.Type,
                report.Status,
                report.Name,
                report.Phone,
                report.Governorate,
                report.District,
                report.AdminResponse,
                Details = details
            };
        }

        // =============================
        // 🟢 USER TRACKING
        // =============================
        public async Task<List<object>> GetMyReports()
        {
            var userId = GetUserId();

            return await _context.Reports
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Id)
                .Select(r => new
                {
                    r.Id,
                    r.Type,
                    r.Status,
                    r.AdminResponse
                })
                .ToListAsync<object>();
        }

        // =============================
        // 🟢 MAP (ONLY APPROVED)
        // =============================
        public async Task<List<object>> GetMapReports()
        {
            return await _context.Reports
                .Where(r => r.Status == ReportStatus.Approved)
                .Select(r => new
                {
                    r.Id,
                    r.Type,
                    r.Latitude,
                    r.Longitude,

                    Color = r.Type == ReportType.Bite ? "red" :
                            r.Type == ReportType.DangerousAnimal ? "orange" :
                            r.Type == ReportType.Complaint ? "blue" : "gray"
                })
                .ToListAsync<object>();
        }

        // =============================
        // 🟢 GET ALL REPORTS (ADMIN)
        // =============================
        public async Task<List<object>> GetAllReports()
        {
            return await _context.Reports
                .Include(r => r.BiteReport)
                .Include(r => r.DangerousAnimalReport)
                .Include(r => r.ComplaintReport)
                .OrderByDescending(r => r.Id)
                .Select(report => new
                {
                    report.Id,
                    report.Type,
                    report.Status,
                    report.Name,
                    report.Phone,
                    report.Governorate,
                    report.District,
                    report.AdminResponse
                })
                .ToListAsync<object>();
        }

        // =============================
        // 🟢 MARK SEEN
        // =============================
        public async Task MarkSeen(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) throw new Exception("Not found");

            report.Status = ReportStatus.Seen;
            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 APPROVE
        // =============================
        public async Task ApproveReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) throw new Exception("Not found");

            report.Status = ReportStatus.Approved;
            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 REJECT
        // =============================
        public async Task RejectReport(int id, string reason)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) throw new Exception("Not found");

            report.Status = ReportStatus.Rejected;
            report.AdminResponse = reason;

            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 IN PROGRESS
        // =============================
        public async Task MarkInProgress(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) throw new Exception("Not found");

            report.Status = ReportStatus.InProgress;
            await _context.SaveChangesAsync();
        }

        // =============================
        // 🟢 DONE
        // =============================
        public async Task MarkDone(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) throw new Exception("Not found");

            report.Status = ReportStatus.Done;
            await _context.SaveChangesAsync();
        }
    }
}