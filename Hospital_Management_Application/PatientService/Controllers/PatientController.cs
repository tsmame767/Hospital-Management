using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.Interface;
using PatientService.Model;
using System.Reflection.Metadata.Ecma335;

namespace PatientService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IPatient PatientService;
        public PatientController(IPatient _Patient)
        {
            this.PatientService = _Patient;
        }

        [HttpPost("Create-Patient")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreatePatientDetails(PatientModel Model, int PatientId)
        {
            var res = await PatientService.CreatePatient(Model, PatientId);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("Update-Patient")]
        [Authorize(Roles = "admin")]
        public async Task<ModelResponse> UpdatePatientDetails(PatientModel Model, int PatientId)
        {
            var res = await PatientService.UpdatePatient(Model, PatientId);
            if (res != null)
            {
                return new ModelResponse { IsSuccess = true, Message = "Updated Patient Details",Status=201 };
            }
            return new ModelResponse { IsSuccess = false, Message = "Not Updated Patient Details", Status = 204 };
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<ModelResponse> DeletePatientDetails( int PatientId)
        {
            var res = await PatientService.DeletePatient(PatientId);
            if (res != null)
            {
                return new ModelResponse { IsSuccess = true, Message = "Deleted Patient Details", Status = 201 };
            }
            return new ModelResponse { IsSuccess = false, Message = "Not Deleted Patient Details", Status = 204 };
        }
        //[HttpGet]
        //public PatientResponse BookAppointments(int DoctorId, string DeptName)
        //{

        //}

        [HttpGet("Get-Patient-Details")]
        [Authorize(Policy = "RequireAdminAndDoctor")]
        public async Task<IActionResult> GetPatientDetails(int PatientId)
        {
            var res = await PatientService.GetPatientDetails(PatientId);
            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


    }
}
