namespace AppointmentService.Model
{
    public class AppointmentModel
    {
        public int DoctorId {get;set;}
        public int PatientId { get;set;}
        public int AppointmentId { get;set;}
        public string CreatedDate { get;set;}
        //public Response response { get;set;}
    }

    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public int Experience { get; set; }
        public string Gender { get; set; }
        public int AppointmentCreatedBy { get; set; }
        public string Department { get; set; }
    }

    public class BookAppointmentModel
    {
        public int DoctorId { get; set; }
        //public int PatientId { get; set; }
        //public int AppointmentId { get; set; }
        public string CreatedDate { get; set; }
    }

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
