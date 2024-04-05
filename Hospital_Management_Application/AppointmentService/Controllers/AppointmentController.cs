using AppointmentService.Interface;
using AppointmentService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _service;

        public AppointmentController(IAppointment service)
        {
            _service = service;
        }

        [HttpGet("get-doctors-by-dept")]
        [Authorize(Policy = "RequireAdminAndPatient")]
        public async Task<IActionResult> GetDoctors(string DepartmentName, string token)
        {
            var result = await _service.GetAvailableDoctors(DepartmentName,token);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Book-Appointment")]
        [Authorize(Roles ="patient")]
        public async Task<IActionResult> CreateAppointment(BookAppointmentModel model)
        {
            var userClaims = User.Identity as ClaimsIdentity;
            var roleClaim = userClaims?.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim != null && roleClaim == "patient")
            {
                var PatientId = Convert.ToInt32(userClaims?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _service.BookAppointment(model,PatientId);
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            return Unauthorized();
        }

        
    }
}
