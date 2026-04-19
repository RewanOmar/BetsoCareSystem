using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
   // [Authorize(Roles = "Admin")]
    [Route("api/dashboard/clinics")]
    [ApiController]
    public class DashboardClinicsController : ControllerBase
    {
        private readonly IClinicRepository _repo;
        private readonly IWebHostEnvironment _env;

        public DashboardClinicsController(IClinicRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        // GET dashboard clinics
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinics = await _repo.GetAllAsync();
            return Ok(clinics);
        }

        // ADD clinic
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateClinicDto dto)
        {
            string? imagePath = null;

            if (dto.Image != null)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "images", "clinics");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = "/images/clinics/" + fileName;
            }

            var clinic = new Clinic
            {
                Name = dto.Name,
                Address = dto.Address,
                Governorate = dto.Governorate,
                Phone = dto.Phone,
                FacebookPage = dto.FacebookPage,
                ImageUrl = imagePath,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                BookingPrice = dto.BookingPrice,
                WorkingDays = dto.WorkingDays,
                WorkingHours = dto.WorkingHours
            };

            var result = await _repo.AddAsync(clinic);

            return Ok(result);
        }

        // UPDATE clinic
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateClinicDto dto)
        {
            var clinic = await _repo.GetByIdAsync(id);

            if (clinic == null)
                return NotFound();

            clinic.Name = dto.Name;
            clinic.Address = dto.Address;
            clinic.Governorate = dto.Governorate;
            clinic.Phone = dto.Phone;
            clinic.FacebookPage = dto.FacebookPage;
            clinic.Latitude = dto.Latitude;
            clinic.Longitude = dto.Longitude;
            clinic.BookingPrice = dto.BookingPrice;
            clinic.WorkingDays = dto.WorkingDays;
            clinic.WorkingHours = dto.WorkingHours;

            if (dto.Image != null)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "images", "clinics");

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                clinic.ImageUrl = "/images/clinics/" + fileName;
            }

            var updated = await _repo.UpdateAsync(clinic);

            return Ok(updated);
        }

        // DELETE clinic
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clinic = await _repo.GetByIdAsync(id);

            if (clinic == null)
                return NotFound("Clinic not found");

            await _repo.DeleteAsync(id);

            return Ok("Clinic deleted successfully");
        }
    }
}