using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace SchoolLogin.Services
{
  public class TokenGenerator
  {
    public string Generate()
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("JWT_SECRET"));
      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        SigningCredentials = signingCredentials,
        Expires = DateTime.Now.AddDays(5)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}