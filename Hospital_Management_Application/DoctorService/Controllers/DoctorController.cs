using Azure;
using DoctorService.Interface;
using DoctorService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class DoctorController : ControllerBase
    {
        private readonly IDocService _service;

        public DoctorController(IDocService service)
        {
            _service = service;
        }

        [HttpGet("get-doctors")]
        [Authorize(Policy = "RequireAdminAndPatient")]
        public async Task<IActionResult> getdoctors(string DepartmentName)
        {
            var role = User.FindFirst(ClaimTypes.Role);
            //if (!string.Equals(role, "admin",StringComparison.OrdinalIgnoreCase))
            //{
            //    return Forbid();
            //}
            var result = await _service.GetAvailableDoctors(DepartmentName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Create-DrDetails")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateDoctorDetails(UpdateDoctorModel Model, int DoctorId)
        {
            var result = await _service.CreateDoctorDetails(Model, DoctorId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            
        }


        [HttpPut("Update-DrDetails")]
        [Authorize(Roles = "admin")]
        public async Task<ModelResponse> UpdateDoctorDetails(UpdateDoctorModel Model,int DoctorId)
        {
            var result= await _service.UpdateDoctorDetails(Model,DoctorId);
            if (result < 0)
            {
                return new ModelResponse
                {
                    IsSuccess = false,
                    Message = "No Details Updated",
                    Status = 204
                };
            }
            return new ModelResponse
            {
                IsSuccess = true,
                Message = "Doctor Details Updated",
                Status = 201
            };
        }

        //[httpget]
        //public async task<t> showappointments()
        //{

        //}
    }
}
