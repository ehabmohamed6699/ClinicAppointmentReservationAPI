using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpecializationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var specializations = await _unitOfWork.Specializations.GetAllAsync();
            return Ok(specializations);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSpecializationById(int id)
        {
            var specialization = await _unitOfWork.Specializations.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> CreateSpecialization([FromBody] string name)
        {
            var newSpecialization = new Specialization
            {
                Name = name
            };
            _unitOfWork.Specializations.Create(newSpecialization);
            await _unitOfWork.SaveChangesAsync();
            return Ok(newSpecialization);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] string name)
        {
            var existingSpecialization = await _unitOfWork.Specializations.GetByIdAsync(id);
            if (existingSpecialization == null)
            {
                return NotFound();
            }
            existingSpecialization.Name = name;
            _unitOfWork.Specializations.Update(existingSpecialization);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingSpecialization);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var existingSpecialization = await _unitOfWork.Specializations.GetByIdAsync(id);
            if (existingSpecialization == null)
            {
                return NotFound();
            }
            _unitOfWork.Specializations.Delete(existingSpecialization);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}