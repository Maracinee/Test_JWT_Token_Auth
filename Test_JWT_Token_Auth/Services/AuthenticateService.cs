using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test_JWT_Token_Auth.Models;

namespace Test_JWT_Token_Auth.Services
{
    //Implement interface IAuthenticateService
    public class AuthenticateService : IAuthenticateService
    {
        //AppSettings private readonly
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        //Template list of User
        private List<User> users = new List<User>()
        {
            new User{UserId = 1, FirstName = "Test1_FirstName" , LastName = "Test1_LastName", UserName = "Test1_UserName", Password = "Test1_Password"}
        };
        public User Authenticate(string userName, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            //Return null if user is not found
            if (user == null)
            {
                return null;
            }
            //Generate Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();
            //Encode Token
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            //Generate Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Generate token based on Handler
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            //Default password null
            user.Password = null;
            //Override users credentials
            return user;
        }
    }
}
