using BetsoCare.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsoCare.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
        public class ClinicsController : ControllerBase
        {
            private readonly IClinicRepository _repo;

            public ClinicsController(IClinicRepository repo)
            {
                _repo = repo;
            }

            [HttpGet]
            public async Task<IActionResult> GetClinics()
            {
                var clinics = await _repo.GetAllAsync();
                return Ok(clinics);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetClinic(int id)
            {
                var clinic = await _repo.GetByIdAsync(id);

                if (clinic == null)
                    return NotFound("Clinic not found");

                return Ok(clinic);
            }

        [HttpGet("map")]
        public async Task<IActionResult> GetClinicsForMap()
        {
            var clinics = await _repo.GetAllAsync();

            var result = clinics.Select(c => new
            {
                c.Id,
                c.Name,
                c.Latitude,
                c.Longitude
            });

            return Ok(result);
        }
    }
    }