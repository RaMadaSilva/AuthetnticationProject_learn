using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TokenDotNetCore.Services
{
    public class GenerateTokenService
    {
        public string TokenGerator()
        {
            var hander = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Secret);

            var signing = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256 );

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signing, 
                Expires = DateTime.UtcNow.AddHours(10)
            };

            var token = hander.CreateToken(descriptor);

            return hander.WriteToken(token);

        }
    }
}