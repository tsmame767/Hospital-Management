namespace Users.Models
{
    public class Model
    {
        public int UserId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

    }
    public class RegisterModel
    {
        public string first_name { get; set; }
        public string last_name { get; set;}
        public string email { get; set; }
        public string password { get; set; }
        public string role {  get; set; }
        public string status {  get; set; }
    }

    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }

    }

    public class LoginResponse
    {
        public ResponseModel response { get; set; }
        public string Token { get; set; }
        public Model Model { get; set; }
    }
    public class ResponseModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public bool IsSucces { get; set; }
    }
}
