using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Users.JWT
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration config)
        {
            this._configuration = config;
        }
        public string generateJwtToken(int UserId, string Email,string role)
        {

            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException(nameof(Email), "Email cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException(nameof(role), "Role cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(UserId.ToString()))
            {
                throw new ArgumentNullException(nameof(UserId), "Role cannot be null or empty.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
         new Claim(ClaimTypes.Email,Email),
         new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),
         new Claim(ClaimTypes.Role,role)
         };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audiance"],
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(120),

              //notBefore:
              signingCredentials: credentials);

            string Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return Data;
        }
    }
}
