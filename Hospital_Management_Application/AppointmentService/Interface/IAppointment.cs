using AppointmentService.Model;

namespace AppointmentService.Interface
{
    public interface IAppointment
    {
        Task<AppointmentModel> BookAppointment(BookAppointmentModel model,int PatientId);
        Task<List<DoctorModel>> GetAvailableDoctors(string DepartmentName,string token);
    }
}
