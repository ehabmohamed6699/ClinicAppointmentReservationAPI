using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Models.DTO;
using ClinicAppointmentReservation.Domain.Records;
using ClinicAppointmentReservation.Infrastructure.Data.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DoctorMapper _mapper;
        private readonly UserManager<User> _userManager;
        public DoctorsController(IUnitOfWork unitOfWork, DoctorMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DoctorSearchParameters parameters)
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync(parameters);
            return Ok(new
            {
                Doctors = doctors.doctors,
                TotalCount = doctors.TotalCount
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDetailsAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            DoctorDTO doctorDTO = _mapper.MapToDoctorDTO(doctor);
            return Ok(doctorDTO);
        }
        [HttpPost("become-a-doctor")]
        [Authorize]
        public async Task<IActionResult> BecomeADoctor(FormDoctorApplication model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            var doctor = await _unitOfWork.Doctors.GetByUserIdAsync(user.Id);
            if (doctor != null)
            {
                return BadRequest("You are already a doctor.");
            }
            var newDoctor = new Doctor
            {
                Bio = model.Bio,
                SpecializationId = model.SpecializationId,
                UserId = user.Id
            };
            _unitOfWork.Doctors.Add(newDoctor);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("approve-doctor/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> ApproveDoctor(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(doctor.UserId);
            if(user == null)
            {
                return NotFound();
            }
            doctor.IsApproved = true;
            await _userManager.AddToRoleAsync(user, "doctor");
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
