using System.Text.Json.Serialization;

namespace DoctorService.Model
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public int Experience { get; set; }
        public string Gender { get; set; }
        public int AppointmentCreatedBy { get; set; }
        public string Department { get; set; }
    }

    public class UpdateDoctorModel
    {
        [JsonIgnore]
        public int DoctorId { get; set; }
        public int Experience { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
    }


    public class DoctorModelResp
    {
        public int DoctorId { get; set; }
        public int Experience { get; set; }
        public string Gender { get; set; }
        //public int AppointmentCreatedBy { get; set; }
        public string Department { get; set; }

    }

    public class ModelResponse
    {
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

}
