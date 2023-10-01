using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TokenDotNetCore.ViewModel;

namespace TokenDotNetCore.Services
{
    public class CreateTokenService
    {
        private readonly IConfiguration _configuration;

        public CreateTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserToken CreateToken()
        {
            var handler = new JwtSecurityTokenHandler();

            var simetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(simetricKey, SecurityAlgorithms.HmacSha256),

                Expires = DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:Expire"]))

            };


            var token = handler.CreateToken(descriptor);

            return new UserToken
            {
                Token = handler.WriteToken(token),
                Created = true,
                Mensage = "Token Gerado com sucesso"

            }; 
        }
    }
}
