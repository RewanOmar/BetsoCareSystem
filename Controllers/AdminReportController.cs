using BetsoCare.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [ApiController]
    [Route("api/admin/reports")]
    //[Authorize(Roles = "Admin")] 
    public class AdminReportController : ControllerBase
    {
        private readonly ReportService _service;

        public AdminReportController(ReportService service)
        {
            _service = service;
        }

        // ✅ 1. عرض كل الريپورتات
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _service.GetAllReports();
            return Ok(reports);
        }

        [HttpPut("{id}/seen")]
        public async Task<IActionResult> MarkSeen(int id)
        {
            await _service.MarkSeen(id);
            return Ok("Report Seen");
        }

        // ✅ 2. approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveReport(id);
            return Ok("Report Approved");
        }

        // ✅ 3. reject + رسالة من الادمن
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] string reason)
        {
            await _service.RejectReport(id, reason);
            return Ok("Report Rejected");
        }

        // ✅ 4. mark as in progress
        [HttpPut("{id}/in-progress")]
        public async Task<IActionResult> InProgress(int id)
        {
            await _service.MarkInProgress(id);
            return Ok("Report In Progress");
        }

        // ✅ 5. mark as done
        [HttpPut("{id}/done")]
        public async Task<IActionResult> Done(int id)
        {
            await _service.MarkDone(id);
            return Ok("Report Done");
        }

        [HttpGet("map")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMapReports()
        {
            var reports = await _service.GetMapReports();
            return Ok(reports);
        }
    }
}