using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Models.DTO;
using ClinicAppointmentReservation.Domain.Records;
using ClinicAppointmentReservation.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClinicsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllClinics(string? search, int? page)
        {
            var parameters = new ClinicSearchParameters(search, page ?? 1);
            var result = await _unitOfWork.Clinics.GetAllAsync(parameters);
            return Ok(new {
                Clinics = result.Clinics,
                TotalCount = result.TotalCount
            });
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetClinicById(int id)
        {
            var clinic = await _unitOfWork.Clinics.GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return Ok(clinic);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> CreateClinic(FormClinic clinic)
        {
            var newClinic = new Clinic
            {
                Name = clinic.Name,
                Address = clinic.Address,
                PhoneNumber = clinic.PhoneNumber
            };
            _unitOfWork.Clinics.Add(newClinic);
            await _unitOfWork.SaveChangesAsync();
            return Ok(newClinic);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> UpdateClinic(int id, FormClinic clinic)
        {
            var existingClinic = await _unitOfWork.Clinics.GetByIdAsync(id);
            if (existingClinic == null)
            {
                return NotFound();
            }
            existingClinic.Name = clinic.Name;
            existingClinic.Address = clinic.Address;
            existingClinic.PhoneNumber = clinic.PhoneNumber;
            _unitOfWork.Clinics.Update(existingClinic);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingClinic);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _unitOfWork.Clinics.GetByIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            _unitOfWork.Clinics.Delete(clinic);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
