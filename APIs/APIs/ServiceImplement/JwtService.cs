using APIs.DTOs;
using APIs.Models;
using APIs.Services;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIs.ServiceImplement
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AthenticationResponseDTO CreateJwtToken(ApplicationUser user, IList<string> roles)
        {
            DateTime expiration = DateTime.UtcNow.AddDays(Convert.ToDouble
                (_configuration["Jwt:EXPIRATION_DAYS"]));

            List<Claim> claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
        new Claim(ClaimTypes.NameIdentifier, user.Email),
        new Claim(ClaimTypes.Name, user.NameOfPerson)
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            SymmetricSecurityKey securityKey = new
                SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new
                     SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new
            JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AthenticationResponseDTO()
            {
                Token = token,
                Email = user.Email,
                PersonName = user.NameOfPerson,
                Expiration = expiration
            };
        }
    }
}