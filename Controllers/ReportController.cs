using BetsoCare.Core.DTOS;
using BetsoCare.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/report")]
    [Authorize] // 🔥 كل endpoints محتاجة login
    public class ReportController : ControllerBase
    {
        private readonly ReportService _service;

        public ReportController(ReportService service)
        {
            _service = service;
        }

        // =============================
        // 🟢 CREATE BITE
        // =============================
        [HttpPost("bite")]
        public async Task<IActionResult> CreateBite(BiteReportDto dto)
        {
            await _service.CreateBite(dto);
            return Ok("Bite Report Submitted");
        }

        // =============================
        // 🟢 CREATE DANGEROUS
        // =============================
        [HttpPost("dangerous")]
        public async Task<IActionResult> CreateDangerous(DangerousAnimalDto dto)
        {
            await _service.CreateDangerous(dto);
            return Ok("Dangerous Animal Report Submitted");
        }

        // =============================
        // 🟢 CREATE COMPLAINT
        // =============================
        [HttpPost("complaint")]
        public async Task<IActionResult> CreateComplaint(ComplaintDto dto)
        {
            await _service.CreateComplaint(dto);
            return Ok("Complaint Submitted");
        }

        // =============================
        // 🟢 GET MY REPORTS (Tracking)
        // =============================
        [HttpGet("my-reports")]
        public async Task<IActionResult> GetMyReports()
        {
            var reports = await _service.GetMyReports();
            return Ok(reports);
        }

        // =============================
        // 🟢 MAP (Approved / Done only)
        //// =============================
        //[AllowAnonymous] // الخريطة ممكن تبقى عامة
        //[HttpGet("map")]
        //public async Task<IActionResult> GetMapReports()
        //{
        //    var reports = await _service.GetMapReports();
        //    return Ok(reports);
        //}
    }
}