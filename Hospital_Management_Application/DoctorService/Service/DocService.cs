using Dapper;
using DoctorService.Context;
using DoctorService.Interface;
using DoctorService.Model;

namespace DoctorService.Service
{
    public class DocService:IDocService
    {
        private readonly ContextDB dBContext;

        public DocService(ContextDB dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<List<DoctorModel>> GetAvailableDoctors(string DepartmentName)
        {
            using (var connection = dBContext.CreateConnection()) // Assuming CreateConnection is a method that returns IDbConnection
            {
                var query = $"SELECT * FROM Doctors_details where Department=@Department"; // Example query
                var doctors = await connection.QueryAsync<DoctorModel>(query,new {Department = DepartmentName});
                return doctors.ToList();
            }
        }

        public async Task<DoctorModel> CreateDoctorDetails(UpdateDoctorModel Model, int Id)
        {
            using (var connection = dBContext.CreateConnection()) // Assuming CreateConnection is a method that returns IDbConnection
            {
                var query = $"insert into Doctors_Details(DoctorId,Experience,Gender,Department) values (@doctorid,@experience,@gender,@department);"; // Example query
                var doctors = await connection.ExecuteAsync(query, new { DoctorId = Id, Experience = Model.Experience, Gender = Model.Gender, Department = Model.Department });
                if (doctors <= 0)
                {
                    return null;
                }
                var res = await connection.QueryFirstOrDefaultAsync<DoctorModel>("select * from Doctors_Details where DoctorId=@doctorId",new {doctorId=Id});
                return res;
            }
            return null;
        }

        public async Task<int> UpdateDoctorDetails(UpdateDoctorModel Model,int Id)
        {
            using (var connection = dBContext.CreateConnection()) // Assuming CreateConnection is a method that returns IDbConnection
            {
                var query = $"update Doctors_details set Experience=@Experience, Gender=@Gender, Department=@Department where DoctorId=@Id "; // Example query
                var doctors = await connection.ExecuteAsync(query, new { Id=Id, Experience=Model.Experience, Gender=Model.Gender,Department = Model.Department });
                return doctors;
            }
            return 0;
        }

    }
}
