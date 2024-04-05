using Dapper;
using System.ComponentModel.DataAnnotations;
using Users.ContextDB;
using Users.Interface;
using Users.JWT;
using Users.Models;

namespace Users.Service
{
    public class UserService:IUser
    {
        private readonly DBContext dBContext;
        private readonly IConfiguration _config;

        public UserService(DBContext dBContext,IConfiguration config)
        {
            this.dBContext = dBContext;
            this._config = config;
        }

        public async Task<Model> GetAll(int UserId)
        {
            var query = "select userid,first_name,last_name,email from User_Details where userid = @userid";

            using (var connect = this.dBContext.CreateConnection())
            {
                var res = await connect.QueryFirstOrDefaultAsync<Model>(query, new { userid = UserId });

                return new Model { UserId = res.UserId, email = res.email, first_name = res.first_name, last_name = res.last_name };
            }
        }

        public async Task<int> UserRegisteration(RegisterModel Credentials)
        {
            var newstatus = "";
            
            var query = $"insert into user_details(first_name,last_name,email,[password],[role],status) values(@first_name,@last_name,@email,@password,@role,@status)";
            using(var connect = this.dBContext.CreateConnection())
            {
                if (Credentials.role == "doctor")
                {
                    newstatus = "UnActive";
                }
                var res = await connect.ExecuteAsync(query,new { first_name = Credentials.first_name, last_name=Credentials.last_name, email=Credentials.email,password= BCrypt.Net.BCrypt.HashPassword(Credentials.password),role=Credentials.role,status = newstatus });
                if (res == null)
                {
                    return 0;
                }
                return 1;
            }

        }
        public async Task<LoginResponse> UserLogin(LoginModel Credentials)
        {
            var query = $"select * from user_details where email=@email";

            using(var connect = this.dBContext.CreateConnection())
            {
                var userid = await connect.QueryFirstOrDefaultAsync<int>($"SELECT * FROM User_details WHERE email =@email",new {email=Credentials.email});
                var res = await  connect.QuerySingleOrDefaultAsync <RegisterModel>(query, new { email = Credentials.email });
                if (res != null)
                {
                    if (res != null && BCrypt.Net.BCrypt.Verify(Credentials.password, res.password))
                    {

                        TokenGenerator Token = new TokenGenerator(_config);
                        var LoginToken = Token.generateJwtToken(userid, Credentials.email, res.role);

                        return new LoginResponse { Token = LoginToken, Model = new Model { email = res.email, first_name = res.first_name, last_name = res.last_name, UserId = userid } };
                    }
                }
                
            }
            return null;
        }
    }
}
