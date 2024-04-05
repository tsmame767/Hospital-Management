using Users.Models;

namespace Users.Interface
{
    public interface IUser
    {
        Task<Model> GetAll(int UserId);
        Task<int> UserRegisteration(RegisterModel Credentials);
        Task<LoginResponse> UserLogin(LoginModel Credentials); 
    }
}
