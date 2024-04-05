using System.Text.Json.Serialization;

namespace PatientService.Model
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public bool Insurance { get; set; }
        public string Gender { get; set; }
        public string Disease { get; set; }


    }

    public class PatientResponse 
    {
        public bool IsSuccess {  get; set; }
        public string Message {  get; set; }
        public int Status { get; set; }
    }

    public class PatientDetailsModel
    {
        public int  UserId {  get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public bool Insurance { get; set; }
        public string Gender { get; set; }
        public string Disease { get; set; }
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

    }
    public class ModelResponse
    {
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }

}
