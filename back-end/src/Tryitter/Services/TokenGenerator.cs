using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SchoolLogin.Services
{
    public class TokenGenerator
    {
        public string Generate(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(student),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Expires = DateTime.Now.AddDays(1)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AddClaims(Student student) 
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim("StudentId", student.StudentId.ToString()));
            return claims;
        }
    }
}