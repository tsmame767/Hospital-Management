using AppointmentService.Context;
using AppointmentService.Interface;
using AppointmentService.Model;
using Dapper;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AppointmentService.Service
{
    public class AppointService:IAppointment
    {
        private readonly DBContext _dbContext;
        //private readonly IHttpClientFactory _client;
        private readonly HttpClient _httpClient;

        public AppointService(DBContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        public async Task<List<DoctorModel>> GetAvailableDoctors(string DepartmentName,string token)
        {
            //var response = await _httpClient.GetAsync($"https://localhost:7067/api/Doctor/get-doctors?DepartmentName={DepartmentName}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var user = JsonSerializer.Deserialize<List<DoctorModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //    return user;
            //}
            //return null;
            var client = new HttpClient();

            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var protectedResourceUri = $"https://localhost:7067/api/Doctor/get-doctors?DepartmentName={DepartmentName}";
            var response = await client.GetAsync(protectedResourceUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<List<DoctorModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                return user; // The data from the protected endpoint
            }
            return null;

        }

        public async Task<AppointmentModel> BookAppointment(BookAppointmentModel model, int PatientId) 
        {
            var query = "insert into Appointment_Details (patientid,doctorid,createdDate) values(@patientid,@doctorid,@createdDate)";

            using (var connect = _dbContext.CreateConnection())
            {
                var res = await connect.ExecuteAsync(query,new {patientid=PatientId,doctorid=model.DoctorId,createdDate=model.CreatedDate});
                var getres= await connect.QueryFirstOrDefaultAsync<AppointmentModel>("select * from Appointment_Details where patientid=@patientid and doctorid=@doctorid and createdDate=@createdDate",new { patientid =  PatientId, doctorid = model.DoctorId, createdDate = model.CreatedDate });
                return getres;
            }
            return null;
        }

        //public async Task<>
    }
}
