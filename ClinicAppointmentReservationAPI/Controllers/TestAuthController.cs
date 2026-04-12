using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok("Open for all");
        }

        [HttpGet("admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public IActionResult GetAdmin()
        {
            // return the current user's claims for debugging role/claim issues
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { message = "Admin only", claims });
        }
        [HttpGet("patient")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "patient")]
        public IActionResult GetPatient()
        {
            // return the current user's claims for debugging role/claim issues
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { message = "Patient only", claims });
        }
    }
}
