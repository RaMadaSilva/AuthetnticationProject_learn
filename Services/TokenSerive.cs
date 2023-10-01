using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TokenDotNetCore.Models;

namespace TokenDotNetCore.Services
{
    public class TokenSerive
    {
        public string Create(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.Secret);

            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credential,
                Expires = DateTime.Now.AddHours(9),
                Subject = GenerateClamins(user)

            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);

        }

        private static ClaimsIdentity GenerateClamins(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Id.ToString()));

            foreach(var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role)); 

            return ci;
        }
    }
}