using DoctorService.Model;

namespace DoctorService.Interface
{
    public interface IDocService
    {
        Task<List<DoctorModel>> GetAvailableDoctors(string DepartmentName);
        Task<int> UpdateDoctorDetails(UpdateDoctorModel Model, int Id);
        Task<DoctorModel> CreateDoctorDetails(UpdateDoctorModel Model, int Id);
    }
}
