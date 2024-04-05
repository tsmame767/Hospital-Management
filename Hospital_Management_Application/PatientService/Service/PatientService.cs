using Dapper;
using PatientService.ContextDB;
using PatientService.Interface;
using PatientService.Model;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;


namespace PatientService.Service
{
    public class PatientsService:IPatient
    {
        private readonly DBContext dBContext;
        //private readonly IHttpClientFactory _client;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientsService(DBContext dBContext,HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.dBContext = dBContext;
            this._httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PatientModel> CreatePatient(PatientModel patient, int PatientId)
        {
            using (var connect = dBContext.CreateConnection())
            {
                var query = $"insert into Patient_Details(PatientId,Insurance,Gender,Disease) values (@patientid,@insurance,@gender,@disease;"; // Example query
                var patients = await connect.ExecuteAsync(query, new { patient = PatientId, insurance = patient.Insurance, Gender = patient.Gender, disease=patient.Disease });
                if (patients <= 0)
                {
                    return null;
                }
                var res = await connect.QueryFirstOrDefaultAsync<PatientModel>("select * from Patient_Details where PatientId=@patientId", new { patientId = PatientId });
                return res;
            }
            return null;
        }

        public async Task<int> UpdatePatient(PatientModel patient, int PatientId)
        {
            using (var connect = dBContext.CreateConnection())
            {
                var query = $"Update Patient_Details set insurance=@insurance, disease=@disease where patientId=@patientid ;"; // Example query
                var patients = await connect.ExecuteAsync(query, new {patientid=PatientId,insurance = patient.Insurance, disease = patient.Disease });
                if(patients > 0)
                {
                    return patients;
                }
                
            }
            return 0;
        }

        public async Task<int> DeletePatient(int PatientId)
        {
            using (var connect = dBContext.CreateConnection())
            {
                var query = $"delete from patient_details where patientId=@patientid ;"; // Example query
                var patients = await connect.ExecuteAsync(query, new { patientid=PatientId });
                if (patients > 0)
                {
                    return patients;
                }
            }
            return 0;
        }

        //public async Task<PatientDetailsModel> GetPatientDetails(int PatientId)
        //{
        public async Task<PatientDetailsModel> GetPatientDetails(int PatientId)
        {

            var client = new HttpClient();
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string[] parts = authorizationHeader.Split(' ');


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parts[1]);

            using (var connection = dBContext.CreateConnection()) // Assuming CreateConnection is a method that returns IDbConnection
            {
                

                var query = $"SELECT * FROM Patient_details where PatientId=@patientid"; // Example query
                var PatientServiceDetails = await connection.QueryFirstOrDefaultAsync<PatientModel>(query,new {patientid=PatientId});

                var debug = "debug";

                var protectedResourceUri = $"https://localhost:7225/api/User?UserId={PatientId}";
                var response = await client.GetAsync(protectedResourceUri);
                //https://localhost:7225/api/User
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return new PatientDetailsModel { Disease = PatientServiceDetails.Disease, Insurance = PatientServiceDetails.Insurance, Gender = PatientServiceDetails.Gender, UserId = user.UserId, first_name = user.first_name, last_name = user.last_name, email = user.email }; // The data from the protected endpoint
                }
                return null;
                //var PatientDetails = await
                //return doctors;
            }
        }







    }
}
