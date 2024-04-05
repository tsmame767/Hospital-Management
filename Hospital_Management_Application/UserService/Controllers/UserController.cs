using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.Interface;
using Users.Models;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUser service;

        public UserController(IUser service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ResponseModel> UserRegisteration(RegisterModel Credentials)
        {
            var res = await service.UserRegisteration(Credentials);
            if (res == 0)
            {
                return new ResponseModel { IsSucces = false, status = 404, message = "Not Registered" };
            }
            return new ResponseModel { IsSucces = true, status = 201, message = $"User Redistered as {Credentials.role}" };

        }

        [HttpPost("User Login")]
        public async Task<LoginResponse> UserLogin(LoginModel Credentials)
        {
            var res = await service.UserLogin(Credentials);
            if (res == null)
            {
                return new LoginResponse { response = new ResponseModel { IsSucces = false, message = "User Not Logged In", status = 204 }, Token = res.Token, Model = new Model { email = res.Model.email, first_name = res.Model.email, last_name = res.Model.last_name, UserId = res.Model.UserId } };
            }
            return new LoginResponse { response = new ResponseModel { IsSucces = true, message = "User Logged In", status = 200 }, Token = res.Token, Model = new Model { email = res.Model.email, first_name = res.Model.email, last_name = res.Model.last_name, UserId = res.Model.UserId } };

        }

        [HttpGet]
        public async Task<Model> GetAll(int UserId)
        {
            try
            {
                var list = await service.GetAll(UserId); // Assuming you have a method GetAllAsync in your service
                if (list == null)
                {
                    return null;
                }

                return list;
            }
            catch
            {
                // It's good practice to handle potential errors
                return new Model { };
            }
        }


    }
}
