using PatientService.Model;

namespace PatientService.Interface
{
    public interface IPatient
    {
        Task<PatientModel> CreatePatient(PatientModel patient, int PatientId);
        Task<int> UpdatePatient(PatientModel patient, int PatientId);
        Task<int> DeletePatient(int PatientId);
        Task<PatientDetailsModel> GetPatientDetails(int PatientId);
        //Task<PatientModel> GetPatientDetails(int PatientId);
    }
}
